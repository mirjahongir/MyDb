using System;
using System.Buffers.Binary;

using BenchmarkDotNet.Running;

using Parser.Benchmark.Benchmarks;

namespace Parser.Benchmark
{
    internal class Program
    {
        static void BitConvertBenchmarks()
        {
            BenchmarkRunner.Run<BitConvertTest>();
            return;
        }
        static void FileManagerBenchmarks()
        {
            BenchmarkRunner.Run<FileManagerBenchmark>();
        }
        static void HashItemBenchmarkMethod()
        {

            BenchmarkRunner.Run<HashItemBenchmark>();
        }
        static void BinaryPrimitivesTestMigration()
        {
            Span<byte> span = stackalloc byte[] { 0x40, 0xE2, 0x01, 0x00 };
            uint a = BinaryPrimitives.ReadUInt32LittleEndian(span);
            Console.WriteLine(a);
            span[0] = 85;
            Console.WriteLine(a);
        }
        static void Main(string[] args)
        {
            BinaryPrimitivesTestMigration();
            // BitConvertBenchmarks();
            // FileManagerBenchmarks();
            //HashItemBenchmarkMethod();
        }
    }



}
