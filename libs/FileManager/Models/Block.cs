using System.Runtime.InteropServices;

using FileManager.Enums;

namespace FileManager.Models
{
    // 2^13=8192
    public class Block
    {
        //10 Byte
        public BlockHeader Header { get; set; }
        public Memory<byte> Data { get; set; } // 2^13 - 10 = 8192 - 10 = 8182
    }
    //10 byte
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BlockHeader
    {
        //4 Byte
        public uint BlockNumber { get; set; }
        //1 byte
        public BlockType BlockType { get; set; }
        // 2 Byte
        public ushort Count { get; set; }
        // 1 byte DataSize 2 darajasi
        public byte DataSize { get; set; }
        // Span<byte> ga aylantirish
        public void ToSpan(ref Span<byte> span)
        {
            //Span<byte> span = new byte[10]; // 4 + 1 + 2 + 1 = 8 byte
            BitConverter.TryWriteBytes(span.Slice(0, 4), BlockNumber);  // BlockNumber: 4 byte
            span[4] = (byte)BlockType;                                  // BlockType: 1 byte
            BitConverter.TryWriteBytes(span.Slice(5, 2), Count);         // Count: 2 byte
            span[7] = DataSize;                                          // DataSize: 1 byte
            //return span.ToArray();
        }

        // Span<byte> dan BlockHeader-ga aylantirish
        public static BlockHeader FromSpan(ref Span<byte> span)
        {
            var blockHeader = new BlockHeader
            {
                BlockNumber = BitConverter.ToUInt32(span.Slice(0, 4)),
                BlockType = (BlockType)span[4],
                Count = BitConverter.ToUInt16(span.Slice(5, 2)),
                DataSize = span[7]
            };
            return blockHeader;
        }

    }

}
