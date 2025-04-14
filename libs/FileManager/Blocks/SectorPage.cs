using System.Buffers;
using System.Runtime.CompilerServices;

using Core.Extensions;
using Core.Models;

using FileManager.Config;
using FileManager.Enums;

namespace FileManager.Blocks
{
    //Block Size= 8192
    //12 byte BaseBlock
    //584*14=8 176 byte Sections
    //
    public sealed class SectorPage : BasePage
    {
        public static ushort FullPropertySize => 16;
        public static ushort ProperySize => 4;
        //2 Byte
        public ushort SectionCount { get; set; }
        //2 byte qoldi

        //14 bytdan 584 ta Page tug`risida ma`lumot yig`adi
        //4 784 128 byte == 4,5625MB  ma`lumotni bita section oladi 
        public required List<SectionItem> Sections { get; set; }
    }
    //14 byte
    public struct SectionItem
    {
        public static ushort SectorSize => 14;
        //4 byte
        public uint BlockId { get; set; }
        //1 Byte
        public PageType PageType { get; set; }
    }
    public static class SectorPageExtension
    {
        #region Serialize
        public static (IMemoryOwner<byte>, Error?) Serialize(this SectorPage page)
        {
            var owner = MemoryPool<byte>.Shared.Rent(FileConfig.PageSize);
            var memory = owner.Memory;
            var span = memory.Span;
            page.SerializeBasePage(ref span);

            var propertySpan = span.Slice(BasePage.BasePageSize, SectorPage.ProperySize);
            SerializeProperty(page, ref propertySpan);

            var sectionSlice = span[SectorPage.FullPropertySize..];
            SerializeSectorData(page, ref sectionSlice);
            return (owner, null);
        }
       
        public static (bool, Error?) SerializeProperty(SectorPage page, ref Span<byte> data)
        {
            page.SectionCount.ToBytes(ref data);
            return (true, null);
        }
        public static (bool, Error?) SerializeSectorData(SectorPage page, ref Span<byte> data)
        {
            for (var i = 0; i < page.SectionCount; i++)
            {
                var slice = data.Slice(i * SectionItem.SectorSize, SectionItem.SectorSize);
                page.Sections[i].BlockId.ToBytes(ref slice);
            }
            return (true, null);
        }
        #endregion

        #region Deserialize
        public static (SectorPage?, Error?) Deserialize(ref Span<byte> data)
        {

            var baseSlice = data.Slice(0, BasePage.BasePageSize);
            var (page, error) = BasePageExtension.DeserializeBasePage<SectorPage>(ref baseSlice);
            if (error != null)
            {
                return (null, error);
            }
            var propertySize = data.Slice(BasePage.BasePageSize - 1, SectorPage.ProperySize);
            DeserializeProperty(page, propertySize);
            var dataSlice = data[(SectorPage.FullPropertySize - 1)..];
            DeserializeSectorItem(page, ref dataSlice);
            return (page, null);
        }
        
        public static (bool, Error?) DeserializeProperty(this SectorPage page, Span<byte> data)
        {
            page.SectionCount = data.ToUint16();
            return (true, null);
        }
        public static (bool, Error?) DeserializeSectorItem(this SectorPage page, ref Span<byte> data)
        {
            page.Sections = [];
            for (var i = 0; i < page.SectionCount; i++)
            {
                var slice = data.Slice(i * SectionItem.SectorSize, SectionItem.SectorSize);
                page.Sections.Add(Generate(slice));
            }
            return (true, null);
        }
        public static SectionItem Generate(Span<byte> data)
        {
            SectionItem result = new SectionItem();
            result.BlockId = data.ToUint32();
            return result;
        }
        #endregion


    }

}
