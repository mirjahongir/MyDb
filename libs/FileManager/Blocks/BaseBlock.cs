using Core.Models;

using FileManager.Enums;

namespace FileManager.Blocks
{
    //20 byte
    public class BaseBlock
    {
        // 8 byte
        public ulong PageId;
        // 1 byte
        public BlockType BlockType;
        //2 byte
        public ushort Hash;
        //8 byte
        public ulong NextPageId { get; set; }

    }
    public static class BaseBlockExtensions
    {
        // 20 Hashni tug`irlash kerak
        public static ValueTuple<ulong, BlockType, ushort, ulong> DeserializeBasePage<T>(this T model, ref Span<byte> span)
            where T : BaseBlock // Removed the redundant 'class' constraint
        {
            model.PageId = BitConverter.ToUInt64(span.Slice(0, 8));
            model.BlockType = (BlockType)span[8];
            model.Hash = BitConverter.ToUInt16(span.Slice(9, 2));
            model.NextPageId = BitConverter.ToUInt64(span.Slice(11, 8));
            return (model.PageId, model.BlockType, model.Hash, model.NextPageId);
        }
        // 20 Hashni tug`irlash kerak
        public static (bool, Error) SerializeBasePage<T>(this T block, ref Span<byte> span)
            where T : BaseBlock // Removed the redundant 'class' constraint
        {

            // Fix: Use `CopyTo` method to copy the byte array into the span
            BitConverter.GetBytes(block.PageId).CopyTo(span.Slice(0, 8));
            span[8] = (byte)block.BlockType;
            BitConverter.GetBytes(block.Hash).CopyTo(span.Slice(9, 2));
            BitConverter.GetBytes(block.NextPageId).CopyTo(span.Slice(11, 8));
            return (true, null); // Placeholder return to ensure method compiles
        }
    }
}