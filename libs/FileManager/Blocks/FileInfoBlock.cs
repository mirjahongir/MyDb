
using System.Buffers;

using Core.Models;

using FileManager.Configs;

namespace FileManager.Blocks
{
    // Fayilni boshida saqlanadigan ma`lumotlar
    // Fayilni ochish uchun kerak bo`ladi
    public class FileInfoBlock : BaseBlock // 20 bayt
    {
        // 4 byte
        public uint FileName;
        // 2 byte
        public ulong FileSize;
        //8 byte
        public ulong BlockCount;
        // 8 byte
        public ulong CreatedDate;
        // 8 byte
        public ulong ModifiedDate;
        // 8 byte
        public ulong AccessedDate;
        // 8 byte
        public Enums.Status Status;
        //8192-128= 8064 byte
        public Memory<byte> Data;
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

            return (rentedMemory, null);
        }
        static (FileInfoBlock?, Error?) DeserializeFileInfo(ref Span<byte> data)
        {
            var fileInfo = new FileInfoBlock();
            fileInfo.DeserializeBasePage(ref data);
            var slice = data.Slice(20, FileConfig.BlockSize);
            fileInfo.FileName = BitConverter.ToUInt32(slice.Slice(0, 4));
            fileInfo.FileSize = BitConverter.ToUInt64(slice.Slice(4, 8));
            fileInfo.BlockCount = BitConverter.ToUInt64(slice.Slice(12, 8));
            fileInfo.CreatedDate = BitConverter.ToUInt64(slice.Slice(20, 8));
            fileInfo.ModifiedDate = BitConverter.ToUInt64(slice.Slice(28, 8));
            fileInfo.AccessedDate = BitConverter.ToUInt64(slice.Slice(36, 8));
            fileInfo.Status = (Enums.Status)slice[44];
            fileInfo.Data = slice.Slice(45, (FileConfig.BlockDataSize - 20)).ToArray();
            return (fileInfo, null);
        }
    }

}
