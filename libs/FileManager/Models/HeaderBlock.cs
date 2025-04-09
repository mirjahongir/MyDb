namespace FileManager.Models
{
    public sealed class HeaderBlock : Block
    {
        public uint PageCount { get; set; } = 0;
        public static HeaderBlock CreateDefaultHeader()
        {
            HeaderBlock block = new()
            {
                PageId = 0,
                BlockType = Enums.BlockType. HeaderBlock,
                Count = 0,
                Hash = 0,
            };
            return block;
        }

    }

}
