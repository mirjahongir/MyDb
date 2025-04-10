namespace FileManager.Blocks
{
    public sealed class TOASTBlock : BaseBlock
    {
        public Memory<byte> Data { get; set; } // 2^13 - 10 = 8192 - 10 = 8182
    }
}
