using System.Buffers;
using System.Numerics;
using System.Runtime.CompilerServices;

using Core.Extensions;
using Core.Models;

using FileManager.Config;

namespace FileManager.Blocks
{

    //8192-12= 8180
    public sealed class FileInfoPage : BasePage //12 byte
    {
        public static ushort FileInfoSize = 30;// ulardan 12 byte BasePage ga tegishli
        // File Name
        //4 byte
        public uint FileName { get; set; }
        //File ning hajmi
        //4 byte
        public uint FileSize { get; set; }
        // File dagi ma`lumotlar soni
        //8 byte
        public ulong FullDataCount { get; set; }
        //File dagi Sectialar soni
        //2 byte 
        public ushort SectionCount { get; set; }



        // 8192-8162=30
        //8162=583*14
        //8176=511*16==(8*2)
        public List<FileInfoItem>? Sections { get; set; }
    }
    // 7 byte
    public struct FileInfoItem
    {
        //4 byte
        public uint PageId { get; set; }
        //3 byte bor bu yerda nima bulishi kerak hali aniq emas
    }
    public static class FileInfoPageExtension
    {
        #region Serialize
        public static IMemoryOwner<byte> Serialize(this FileInfoPage page)
        {
            var owner = MemoryPool<byte>.Shared.Rent(FileConfig.PageSize);
            var memory = owner.Memory;
            var span = memory.Span;
            page.SerializeBasePage(ref span);
            page.SerializeFileInfo(ref span);
            Span<byte> buffer = page.Data.Memory.Span;

            page.SetHash(ref buffer);
        }

        public static (bool, Error?) SerializeFileInfo(this FileInfoPage page, ref Span<byte> buffer)
        {
            var slice = buffer.Slice(0, BasePage.BasePageSize);
            //0,1,2,3
            page.FileName.ToBytes(ref slice);
            //4,5,6,7,
            var fileSizeSlice = slice.Slice(4, 4);
            page.FileSize.ToBytes(ref fileSizeSlice);
            //8,9,10,11,12,13,14,15
            var dataCount = slice.Slice(8, 8);
            page.FullDataCount.ToBytes(ref dataCount);
            // 16,17
            var sectionCount = slice.Slice(16, 2);
            page.SectionCount.ToBytes(ref sectionCount);
            return (true, null);
        }
        public static (bool, Error?) SerializeFileInfoData(this FileInfoPage page, ref Span<byte> buffer)
        {
            var slice = buffer.Slice(0, FileInfoPage.FileInfoSize);
            ushort a = 0;
            if (page.Sections != null)
                foreach (var section in page.Sections)
                {
                    var dataSlice = slice.Slice(a, 7);
                    a += 7;
                    SerializeFile(section, ref dataSlice);
                }
            return (true, null);
        }
        public static (bool, Error?) SerializeFile(FileInfoItem item, ref Span<byte> slice)
        {
            item.PageId.ToBytes(ref slice);

            return (true, null);
        }
        #endregion

        #region Deserialize
        public static FileInfoPage Deserialize(ref Span<byte> data)
        {
            var (fileInfo, error) = BasePageExtension.DeserializeBasePage<FileInfoPage>(ref data);
            var slice = data.Slice(BasePage.BasePageSize);
            DeserializeProperty(fileInfo, ref slice);
            var item = data.Slice(FileInfoPage.FileInfoSize);
            DeserializeSection(fileInfo, ref item);
        }
        public static (bool, Error?) DeserializeProperty(this FileInfoPage infoPage, ref Span<byte> data)
        {

        }
        public static (bool, Error?) DeserializeSection(this FileInfoPage infoPage, ref Span<byte> data)
        {

        }
        #endregion

    }
}
