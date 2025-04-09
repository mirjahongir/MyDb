using FileManager.Managers;

namespace FileManagers.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var (block, err) = BlockFileManager.Create("test.db", 10);
         
         
        }
    }
}
