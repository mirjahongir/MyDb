using System.Buffers;

namespace FileManager.Blocks
{
    /// <summary>
    /// Umumiy ByteCount=8192 -12= 8180 byte-2 Count= 8178
    /// 
    /// </summary>
    public sealed class IndexPage : BasePage
    {
        public ushort Count { get; set; }
        public required List<IndexItem> IndexData { get; set; }
    }

    // Bu hali nima bulishi haqida aniq ma`lumot yuq
    public struct IndexItem
    {

    }
    public static class IndexPageExtension
    {
        public static IMemoryOwner<byte> Serialize(this IndexPage page)
        {
            return null;
        }
        public static IndexPage Deserilaze(ref Span<byte> data)
        {
            return null;
        }
    }
}
