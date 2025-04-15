
using System.Buffers;

using Core.Models;

namespace FileManager.Blocks
{
    public sealed class DataPage : BasePage
    {

    }
    public static class DataPageExtension
    {
        public static (IMemoryOwner<byte>, Error) Serialize(this DataPage page)
        {

        }
        public static (DataPage, Error) Deserialize(this Memory<byte> memory)
        {
        }
    }
}
}
