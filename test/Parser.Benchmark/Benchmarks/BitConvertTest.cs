using System.Buffers.Binary;

using BenchmarkDotNet.Attributes;

namespace Parser.Benchmark.Benchmarks
{
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
                (byte)(a >> 8 & 0xFF), // MSB
            ];
        }
        [Benchmark]
        public void WriteWithBinaryPrimitives()
        {
            byte[] bytes = new byte[2];
            BinaryPrimitives.WriteUInt16LittleEndian(bytes, 99);
        }

    }
}
