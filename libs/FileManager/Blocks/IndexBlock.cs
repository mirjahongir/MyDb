using System.Buffers;

namespace FileManager.Blocks
{
    public sealed class IndexBlock : BaseBlock
    {
        public IMemoryOwner<byte> Data { get; set; }
    }

}
