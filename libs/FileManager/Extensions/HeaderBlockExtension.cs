using Core.Models;
using FileManager.Models;

namespace FileManager.Extensions
{
    public static class HeaderBlockExtension
    {
        public static (bool, Error?) Serialize(this HeaderBlock block, ref Span<byte> data)
        {
            return (true, null);
        }
        public static (HeaderBlock, Error?) Deserialize(ref Span<byte> data)
        {
            var block = new HeaderBlock();
            block.PageId = data[0];
            block.BlockType = (Enums.BlockType)data[1];
            block.Count = data[2];
            block.Hash = data[3];
            block.PageCount = data[4];
            return (block, null);
        }
    }
}
