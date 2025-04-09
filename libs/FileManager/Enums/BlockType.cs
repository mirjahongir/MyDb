
namespace FileManager.Enums
{
    public enum BlockType : byte
    {
        NONE = 0,
        HeaderBlock = 1,
        Data = 3,
    }
    public enum Status : byte
    {
        NONE = 0,
        Active = 1,
        Deleted = 13,
    }
}
