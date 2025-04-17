using BenchmarkDotNet.Attributes;
using ZeroFormatter;

namespace Parser.Benchmark.Benchmarks
{
    [ZeroFormattable]
    // [MemoryPackable]
    //Benchmkarkda MemoryPack ishlatgani yaxshi ekan 50 ns ga serializatsiya qilib berdi
    public class User // Changed 'class' to 'partial class' to fix MEMPACK001
    {
        [Index(0)]
        //  [FieldOrder(0)]
        public virtual long Id { get; set; }
        // [FieldOrder(1)]
        [Index(1)]
        public virtual string? Name { get; set; }
        [Index(2)]
        //    [FieldOrder(2)]
        public virtual string? Description { get; set; }
        public static User CreateUser()
        {
            return new User { Id = 1, Name = "sdcsdcsd", Description = "refervwewedcwe" };
        }
    }
    [MemoryDiagnoser]
    public class SerializeBenchmark
    {
        //[Benchmark]
        //public void BinarySerializer()
        //{
        //    var stream = new MemoryStream();
        //    var serializer = new BinarySerializer();
        //    var derivedClass = User.CreateUser();
        //    serializer.Serialize(stream, derivedClass);
        //    int legth = (int)stream.Length;
        //    var data = new byte[legth];
        //    stream.Write(data, 0, legth);
        //}

        [Benchmark]
        public void ZeroSerializer()
        {
            var memory = ZeroFormatterSerializer.Serialize(User.CreateUser());
        }

        //[Benchmark]
        //public void MemorySerializer()
        //{
        //    var bin = MemoryPackSerializer.Serialize(User.CreateUser());
        //}
    }
}
