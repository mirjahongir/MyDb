using System.Buffers;

namespace FileManager.Blocks
{
    public sealed class TOASTBlock : BaseBlock
    {
        public IMemoryOwner<byte> Data { get; set; } // 2^13 - 10 = 8192 - 10 = 8182
    }
}
