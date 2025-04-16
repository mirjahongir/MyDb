using System.Buffers;

using Core.Extensions;
using Core.Models;

using FileManager.Config;
using FileManager.Extensions;

namespace FileManager.Blocks
{

    //8192-12= 8180
    public sealed class FileInfoPage : BasePage //12 byte
    {
        public static ushort FileInfoFullPropertySize = 30;// ulardan 12 byte BasePage ga tegishli
        public static ushort FileInfoPropertySize = 18; // 4+4+8+2=
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
        //public static uint GetFileName(string path)
        //{
        //    return 0;
        //}
        public static FileInfoPage Create(string path)
        {
            FileInfoPage result = new()
            {
                PageId = 0,
                FileName = path.GetFileName(),// GetFileName(path),
                SectionCount = 0,
                Sections = [],
                FileSize = 0,
                FullDataCount = 0,
                Hash = 0,
                PageType = Enums.PageType.FileInfo,
                UsedCount = 0,
            };
            return result;
        }
        public static FileInfoPage Deserialize(Memory<byte> memory)
        {
            var span = memory.Span;
            return FileInfoPageExtension.Deserialize(ref span);
        }
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

            var properySlice = span.Slice((BasePage.BasePageSize - 1), FileInfoPage.FileInfoFullPropertySize);
            page.SerializeFields(ref properySlice);


            var itemSlice = span[(FileInfoPage.FileInfoFullPropertySize - 1)..];
            page.SerializeFileInfoData(ref itemSlice);
            page.SetHash(ref itemSlice);
            return owner;
        }

        public static (bool, Error?) SerializeFields(this FileInfoPage page, ref Span<byte> slice)
        {
            //  var slice = buffer.Slice(0, BasePage.BasePageSize);
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
            ushort a = 0;
            if (page.Sections != null)
                foreach (var section in page.Sections)
                {
                    var dataSlice = buffer.Slice(a, 7);
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
            //0,11 BasePage
            var (fileInfo, error) = BasePageExtension.DeserializeBasePage<FileInfoPage>(ref data);
            var slice = data.Slice(BasePage.BasePageSize);

            var filePropertySlice = data.Slice((BasePage.BasePageSize - 1), FileInfoPage.FileInfoPropertySize);
            DeserializeProperty(fileInfo, ref filePropertySlice);

            var item = data.Slice(FileInfoPage.FileInfoFullPropertySize - 1);
            DeserializeSection(fileInfo, ref item);
            return fileInfo;
        }
        public static (bool, Error?) DeserializeProperty(this FileInfoPage page, ref Span<byte> data)
        {
            //0,1,2,3
            page.FileName.ToBytes(ref data);
            //4,5,6,7
            var fileSizeSlice = data.Slice(4, 4);
            page.FileSize.ToBytes(ref fileSizeSlice);
            //8,9,10,11,12,13,14,15
            var fileCountSlice = data.Slice(8, 8);
            page.FullDataCount.ToBytes(ref fileCountSlice);
            //16,17
            var sectionCountSlice = data.Slice(16, 2);
            page.SectionCount.ToBytes(ref sectionCountSlice);
            return (true, null);
        }
        public static (bool, Error?) DeserializeSection(this FileInfoPage infoPage, ref Span<byte> data)
        {
            //0,1,2,3,4,5,6
            //7,8,9,10,12,13
            //14,15,16,17,18,19,20
            List<FileInfoItem> sectionData = [];
            for (var i = 0; i < infoPage.SectionCount; i++)
            {
                sectionData.Add(GenerateItem(data.Slice(i * 7, 7)));
            }
            infoPage.Sections = sectionData;
            return (true, null);
        }
        public static FileInfoItem GenerateItem(Span<byte> slice)
        {
            FileInfoItem item = new()
            {
                PageId = slice.Slice(0, 4).ToUint32()
            };
            return item;
        }
        #endregion


    }
}
