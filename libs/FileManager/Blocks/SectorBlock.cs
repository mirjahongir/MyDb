using System.Buffers;

using Core.Models;

namespace FileManager.Blocks
{
    // Har sektor 4.5 MB buladi
    public sealed class SectorBlock : BaseBlock //8 
    {
        //1 byte
        // Har bir sector Indexga yoki  Data block ma`lumoti 
        public SectorType SectorType { get; set; } // 1 byte
        // 3 byte qolayapdi
        //8180 byte 
        // Har bir 14 bytedan  block buladi
        //8176 byte buladi
        //584 ta block ma`lumoti buladi
        /// <summary>
        //4.5 MB buicha bulinadi
        /// </summary>
        public required IMemoryOwner<byte> Data { get; set; }
    }
    //14 byte
    public class SerctorItem
    {
        // Block Raqami
        public uint BlockId { get; set; }
        // Nechta Ma`lumot bor
        public uint Count { get; set; }

    }
    public enum SectorType : byte
    {

    }
    public static class SectorBlockExtension
    {
        static ushort SectorDataSize = 8176;

        public static (bool, Error) SerializeSectorBlock(this SectorBlock block, ref Span<byte> span)
        {
            // 4 byte
            BitConverter.GetBytes(block.PageId).CopyTo(span.Slice(0, 4));
            // 1 byte
            span[8] = (byte)block.BlockType;
            // 2 byte
            BitConverter.GetBytes(block.Hash).CopyTo(span.Slice(9, 2));
            // 1 byte
            span[11] = (byte)block.SectorType;
            // 8176 byte
            block.Data.Span.CopyTo(span.Slice(12, SectorDataSize));
            return (true, null);
        }
        public static (SectorBlock, Error) DeserilzeBlock(ref Span<byte> span)
        {
            var block = new SectorBlock();
            // 4 byte
            block.PageId = BitConverter.ToUInt32(span.Slice(0, 4));
            // 1 byte
            block.BlockType = (BlockType)span[8];
            // 2 byte
            block.Hash = BitConverter.ToUInt16(span.Slice(9, 2));
            // 1 byte
            block.SectorType = (SectorType)span[11];
            // 8176 byte
            block.Data = span.Slice(12, SectorDataSize).ToArray();
            return (block, null);
        }
        public static (bool, Error) SaveNewSector(this SectorBlock sector, FileInfoBlock fileInfo)
        {
            fileInfo
        }
    }

}
//4294967295
