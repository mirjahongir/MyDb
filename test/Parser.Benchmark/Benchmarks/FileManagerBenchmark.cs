using BenchmarkDotNet.Attributes;

namespace Parser.Benchmark.Benchmarks
{
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
}
