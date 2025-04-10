using FileManager.Managers;

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
            Span<byte> span= stackalloc byte[8192];
            stream.Read(span);
            for (int i = 0; i < span.Length; i++)
            {
                Console.WriteLine(span[i]);
            }
        }
        static void Main(string[] args)
        {
            //CreateFile();
            //return;

            ReadFile();

            return;
            ushort a= 6;
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
