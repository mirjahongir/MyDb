using BenchmarkDotNet.Attributes;

namespace Parser.Benchmark.Benchmarks
{
    [MemoryDiagnoser]
    public class HashItemBenchmark
    {
        Memory<byte> memory;
        public HashItemBenchmark()
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
                hash = (int)(hash * 16777619 & 0xFFFFFFFF);  // Xorshift algorithm
            }

            return hash;
        }
        [Benchmark]
        public ushort CalculateChecksum()
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
                checksum = (ushort)(checksum << 1 | checksum >> 15); // rotate left
            }

            checksum ^= 64579 & 0xFFFF;
            checksum ^= 64579 >> 16 & 0xFFFF;

            return checksum;
        }
    }
}
