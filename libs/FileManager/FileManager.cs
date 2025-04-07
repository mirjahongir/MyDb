using FileManager;

namespace FileManagers
{
    public class FileManager : IDisposable
    {
        public readonly string FilePath;
        
        DiskManager _disk;
        MemoryCache _memory;
        public FileManager(string filePath)
        {
            //BUG: 
            FilePath = Path.Combine(FileConfig.FilePath, filePath);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
   
}
