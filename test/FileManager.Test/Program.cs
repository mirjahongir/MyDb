using System.Diagnostics;
using System.Runtime.InteropServices;

using FileManager.Enums;
using FileManager.Models;

using FileManagers;

namespace FileManagers.Test
{
    internal class Program
    {
        public static void CreateBlockTest(ref Memory<byte> data)
        {

            //Block block = new Block()
            //{
            //     Header = new BlockHeader() { BlockNumber = 1, BlockType = BlockType.Create, Count = 5, DataSize = 5 },
            //    Data = data
            //};
            //Span<byte> span = stackalloc byte[8192];
            //block.Header.ToSpan(ref span);
            //block.Data.Span.CopyTo(span.Slice(10));

        }
        static void Main(string[] args)
        {
            // 8000 ta tasodifiy byte yaratish
            byte[] randomBytes = new byte[8000];
            // Random obyekti yaratish
            Random random = new Random();

            // 8000 ta tasodifiy baytni to‘ldirish
            random.NextBytes(randomBytes);

            // Memory<byte> ga o‘tkazish
            Memory<byte> memory = new Memory<byte>(randomBytes);
            CreateBlockTest(ref memory);
        }
    }
}
