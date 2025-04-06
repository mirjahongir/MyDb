using System.Diagnostics;
using System.Text;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
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
            BenchmarkRunner.Run<GetKeyword>();
        }
    }
    [MemoryDiagnoser]

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
