namespace FileManager.Blocks
{
    public sealed class IndexBlock : BaseBlock
    {
        public Memory<byte> Data { get; set; }
    }

}
