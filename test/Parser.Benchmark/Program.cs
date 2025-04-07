using System;
using System.Diagnostics;
using System.Text;
using System.Xml;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using FileManager.Models;

using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Parser.Benchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string str = "some text";
            //Stopwatch sw = Stopwatch.StartNew();
            //for (int i = 0; i < 1_000_000; i++)
            //{
            //    var span = str.AsSpan(); // Span hosil qilish
            //}
            //sw.Stop();
            //Console.WriteLine($"AsSpan() ishlash vaqti: {sw.Elapsed.TotalMilliseconds} ms");
            BenchmarkRunner.Run<FileManagerBenchmark>();
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
            Block block = new Block()
            {
                Header = new BlockHeader() { BlockNumber = 1, BlockType = FileManager.Enums.BlockType.Create, Count = 5, DataSize = 5 },
                Data = _memory
            };
            Span<byte> span = stackalloc byte[8192];
            block.Header.ToSpan(ref span);
            block.Data.Span.CopyTo(span.Slice(10));


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
