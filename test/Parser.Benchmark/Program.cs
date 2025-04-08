using System.Text;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Parser.Benchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<HashItem>();
            return;
            byte[] byteArray1 = new byte[] { 1, 2, 3, 4, 5 };
            byte[] byteArray2 = new byte[] { 1, 2, 3, 4, 5 };

            Memory<byte> memory1 = new Memory<byte>(byteArray1);
            Memory<byte> memory2 = new Memory<byte>(byteArray2);

            Console.WriteLine(memory1.GetHashCode()); // Xotira manzili asosida hash kodi
            Console.WriteLine(memory2.GetHashCode()); // Xotira manzili asosida hash kodi
            return;
            //string str = "some text";
            //Stopwatch sw = Stopwatch.StartNew();
            //for (int i = 0; i < 1_000_000; i++)
            //{
            //    var span = str.AsSpan(); // Span hosil qilish
            //}
            //sw.Stop();
            //Console.WriteLine($"AsSpan() ishlash vaqti: {sw.Elapsed.TotalMilliseconds} ms");
            BenchmarkRunner.Run<BitConvertTest>();
        }
    }
    [MemoryDiagnoser]
    public class HashItem
    {
        Memory<byte> memory;
        public HashItem()
        {
            memory = new Memory<byte>(new byte[8192]);
            // Tasodifiy baytlarni to'ldirish
            Random random = new();
            random.NextBytes(memory.Span);

        }
        [Benchmark]
        public int GetFastHashCode()
        {
            int hash = 0;
            var span = memory.Span;
            foreach (byte b in span)
            {
                hash ^= b;  // XOR operation for mixing bits
                hash = (int)((hash * 16777619) & 0xFFFFFFFF);  // Xorshift algorithm
            }

            return hash;
        }
        [Benchmark]
        public  ushort CalculateChecksum()
        {
            var pageData = memory.Span;
            ushort checksum = 0;
            int checksumOffset = 8; // pd_checksum offset (PageHeaderData ichida odatda 8-chi baytdan boshlanadi)

            for (int i = 0; i < pageData.Length; i += 2)
            {
                if (i == checksumOffset)
                    continue;

                ushort value = BitConverter.ToUInt16(pageData.Slice(i, 2));
                checksum ^= value;
                checksum = (ushort)((checksum << 1) | (checksum >> 15)); // rotate left
            }

            checksum ^= (ushort)(64579 & 0xFFFF);
            checksum ^= (ushort)((64579 >> 16) & 0xFFFF);

            return checksum;
        }
    }
    [MemoryDiagnoser]
    public class BitConvertTest
    {
        [Benchmark]
        public void CreateByte()
        {
            ushort a = 99;
            byte[] bytes = BitConverter.GetBytes(a);
        }
        [Benchmark]
        public void CreateOwnByte()
        {
            byte[] bytes = new byte[2];
            ushort a = 99;
            BitConverter.TryWriteBytes(bytes.AsSpan(), a);
        }
        [Benchmark]
        public void ConvertByBit()
        {
            ushort a = 99;
            byte[] bytes =
            [
                (byte)(a & 0xFF), // LSB
                (byte)((a >> 8) & 0xFF), // MSB
            ];
        }

    }

    [MemoryDiagnoser]
    public class FileManagerBenchmark
    {
        Memory<byte> _memory;
        public void Setup()
        {
            // 8000 ta tasodifiy byte yaratish
            byte[] randomBytes = new byte[8000];
            // Random obyekti yaratish
            Random random = new Random();

            // 8000 ta tasodifiy baytni to‘ldirish
            random.NextBytes(randomBytes);

            // Memory<byte> ga o‘tkazish
            _memory = new Memory<byte>(randomBytes);
        }
        [Benchmark]
        public void SpanBenchmark()
        {
            //Block block = new Block()
            //{
            //    Header = new BlockHeader() { BlockNumber = 1, BlockType = FileManager.Enums.BlockType.Create, Count = 5, DataSize = 5 },
            //    Data = _memory
            //};
            Span<byte> span = stackalloc byte[8192];
            //block.Header.ToSpan(ref span);
            //block.Data.Span.CopyTo(span.Slice(10));


        }
    }


    public class GetKeyword
    {
        public string SqlText = "Selectsedcsd a.id,a.name, a.createDate where a.id=5";
        [GlobalSetup]
        public void Setup()
        {

        }
        [Benchmark]
        public void SpanBenchmark()
        {

            for (int i = 0; i < 1_000_000; i++)
            {
                var span = SqlText.AsSpan(); // Span hosil qilish
            }


        }
        // [Benchmark]
        public void GetFirstSpace()
        {
            var span = SqlText.AsSpan();
            StringBuilder stringBuilder = new StringBuilder();
            int spaceIndex = 0;
            for (var i = 0; i < span.Length; i++)
            {
                if (span[i] == ' ')
                {
                    spaceIndex = i;
                    stringBuilder.Append(span[i]);
                    break;
                }
            }
            var result = stringBuilder.ToString();

        }
        //  [Benchmark]
        public void GetFirstSpaceByIndex()
        {
            var span = SqlText.AsSpan();
            var spaceIndex = span.IndexOf(' ');
            var text = span.Slice(0, spaceIndex);
        }
    }
}
