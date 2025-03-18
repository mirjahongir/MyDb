using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Services.Parser;

namespace MyDb.Benchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //StringBenchmark benchanmark = new StringBenchmark();
            //benchanmark.StringConcatenation();
            //benchanmark.ReadOnlySpan();
            var summary = BenchmarkRunner.Run<StringBenchmark>();
        }
    }
    [MemoryDiagnoser]
    public  class StringBenchmark
    {
        
        public  string cmdText = "  create database joha;";
        //[Benchmark]
        //public void StringConcatenation()
        //{
        //    var token = TokenParser.ParseTokenString(ref cmdText, ref 0);
        //}
        //[Benchmark]
        //public void ReadOnlySpan()
        //{
        //    var span = cmdText.AsSpan();
        //    var token = TokenParser.ParseToken(ref span, 0);
        //}
    }
}
