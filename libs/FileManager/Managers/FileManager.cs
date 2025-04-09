namespace FileManager.Managers
{

    public class FileManager : IDisposable
    {
        DiskManager _disk;
        MemoryManager _memory;
        public FileManager(string path)
        {
            _disk = new DiskManager(path);
            _memory = new MemoryManager();
        }
       
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

}
