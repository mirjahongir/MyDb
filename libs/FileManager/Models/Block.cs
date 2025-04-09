using FileManager.Enums;

namespace FileManager.Models
{
    // 2^13=8192
    public class Block
    {
        // 4 byte
        public uint PageId;
        // 1 byte
        public BlockType BlockType;
        // 2 byte
        public ushort Count;
        //2 byte
        public ushort Hash;
        // not Add Disk
        public bool CanDelete
        {
            get
            {
                //BUG:
                return true;
            }
        }
        // 8192-10=8182
        public Memory<byte> Data { get; set; } // 2^13 - 10 = 8192 - 10 = 8182
    }

}
