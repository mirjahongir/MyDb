using System.Buffers;


namespace FileManagers.Test
{
    internal class Program
    {
        public static void CreateFile()
        {
            FileStream stream = new("test.db", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            stream.SetLength(8192 * 2);
            var random = new Random();
            var data = new byte[8192 * 2];
            random.NextBytes(data);
            stream.Write(data);
        }
        static void ReadFile()
        {
            FileStream stream = new("test.db", FileMode.Open, FileAccess.Read);
            Span<byte> span = stackalloc byte[8192];
            stream.Read(span);
            for (int i = 0; i < span.Length; i++)
            {
                Console.WriteLine(span[i]);
            }
        }
        // IMemoryOwner test qildim
        public static IMemoryOwner<byte> GenerateMemory()
        {
            var owner = MemoryPool<byte>.Shared.Rent(8192);
            var random = new Random();
            var memory = owner.Memory;
            random.NextBytes(memory.Span);
            return owner;
        }
        static void Main(string[] args)
        {
            Console.WriteLine(GC.GetTotalMemory(false));
            var memory = GenerateMemory();
            var span = memory.Memory.Span;
            //foreach (var i in span)
            //{
            //    Console.WriteLine(i);
            //}
            memory.Dispose();
            Console.WriteLine("Some change");
            Console.WriteLine("Some change 2");
            Task.Run(async () =>
            {
                for (var i = 0; i < 1000; i++)
                {

                    Console.WriteLine(GC.GetTotalMemory(true));
                    GC.Collect();
                    await Task.Delay(1000);
                }

            });

            Console.Read();
            return;


            //CreateFile();
            //return;

            ReadFile();

            return;
            ushort a = 6;
            byte[] bytes = new byte[2];

            BitConverter.TryWriteBytes(bytes.AsSpan(), a);
            Console.WriteLine("{0} {1}", bytes[0], bytes[1]);
            a = 55;
            Console.WriteLine("{0} {1}", bytes[0], bytes[1]);

            return;
            Console.WriteLine("Hello, World!");
            var (block, err) = BlockFileManager.Create("test.db", 10);


        }
    }
}
