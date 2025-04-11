
using System.Buffers;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;

using Core.Models;

using FileManager.Configs;
using FileManager.Errors;

namespace FileManager.Blocks
{
    // Fayilni boshida saqlanadigan ma`lumotlar
    // Fayilni ochish uchun kerak bo`ladi
    // 584(sector saqlaydigan blocklar soni)
    // *583(secktorlar soni)*8192(block soni)
    // = 4 776 960 byte => 2.6 Gb har bir fayl ma`lumot  saqlaydi= 2 659 MB
    public class FileInfoBlock : BaseBlock // 12 bayt
    {
        public static byte PropertySize = 34; //4+8+4+8+8+1; // 4+8+4+8+8+1 = 33
        // 4 byte
        public uint FileName;
        // 8 byte
        public ulong FileSize;
        //4 byte
        public uint SectorCount;
        // 8 byte
        public ulong CreatedDate;
        // Bu kerakmi hali aniq emas
        // 8 byte
        public ulong ModifiedDate;
        // 1 byte
        public Enums.Status Status;

        //8192-12-34=8162   byte
        //8162/14=583 ta Block ma`lumot turadi
        //583*8192=4 776 960 byte
        public IMemoryOwner<byte>? Data;
    }
    //7 byte
    public struct FileInfoItem
    {
        //4 byte
        public uint SectorPageId { get; set; }
        // 3 bayte qoldi yana 3 bayte ma`lumot saqlasam buladi
    }
    public static class FileInfoBlockExtension
    {
        // Bu yerda file Filedan file Blockni olamiz
        public static (FileInfoBlock?, Error?) ReadFileInfo(this FileStream stream)
        {
            Span<byte> span = stackalloc byte[FileConfig.BlockSize];
            stream.Position = 0;
            var intA = stream.Read(span);
            return DeserializeFileInfo(ref span);
        }
        public static (bool?, Error?) SaveFile(this FileInfoBlock fileInfo, FileStream stream)
        {
            var (owner, err) = SerializeFileInfo(fileInfo);
            if (err != null)
            {
                return (false, err);
            }
            stream.Position = 0;
            stream.Write(owner.Memory.Span);
            stream.Flush();
            owner.Dispose(); // Dispose the rented memory after use
            return (true, null);
        }
        static (IMemoryOwner<byte>?, Error?) SerializeFileInfo(FileInfoBlock info)
        {
            var rentedMemory = MemoryPool<byte>.Shared.Rent(FileConfig.BlockSize);
            var blockData = rentedMemory.Memory; // Store the rented memory
            // Use a local variable for the span to avoid the CS0206 error
            var span = blockData.Span;
            info.SerializeBasePage(ref span);
            BinaryPrimitives.WriteUInt32LittleEndian(span.Slice(20, 4), info.FileName);

            //blockData.CopyTo(span.Slice(20, FileConfig.BlockSize));
            return (rentedMemory, null);
        }
        static (FileInfoBlock?, Error?) DeserializeFileInfo(ref Span<byte> data)
        {
            var fileInfo = new FileInfoBlock();
            fileInfo.DeserializeBasePage(ref data);
            var slice = data.Slice(FileConfig.BaseBlockSize, FileConfig.BlockSize - FileConfig.BaseBlockSize);

            fileInfo.FileName = BitConverter.ToUInt32(slice.Slice(0, 4));
            fileInfo.FileSize = BitConverter.ToUInt64(slice.Slice(4, 8));
            // fileInfo.BlockCount = BitConverter.ToUInt64(slice.Slice(12, 8));
            fileInfo.CreatedDate = BitConverter.ToUInt64(slice.Slice(20, 8));
            fileInfo.ModifiedDate = BitConverter.ToUInt64(slice.Slice(28, 8));
            //fileInfo.AccessedDate = BitConverter.ToUInt64(slice.Slice(36, 8));
            fileInfo.Status = (Enums.Status)slice[44];

            //  fileInfo.Data = slice.Slice().ToArray();
            return (fileInfo, null);
        }

        public static (FileInfoItem, Error) GetFileInfoItem(this FileInfoBlock fileInfo, ushort sectorId)
        {

        }
        public static (bool, Error) SaveFileItem(this FileInfoBlock fileInfoBlock, ref FileInfoItem item)
        {
            if (item.SectorId == 0)
            {
                return (false, Err.SectorIdError);
            }
            
        }
       
    }

}
