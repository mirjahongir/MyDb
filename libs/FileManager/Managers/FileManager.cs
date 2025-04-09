using Core.Models;
using FileManager.Models;

namespace FileManager.Managers
{

    public class FileManager : IDisposable
    {
        #region Constructor
        DiskManager _disk;
        MemoryManager _memory;
        Block HeaderBlock;
        private FileManager()
        {

        }
        public static (FileManager?, Error?) Open(string path)
        {
            if (!File.Exists(path))
            {
                return (null, new Error() { Message = "File not found" });
            }
            var diskManager = new DiskManager(path);
            var (block, err) = diskManager.ReadBlock(0);
            if (err != null)
            {
            }
            MemoryManager memoryManager = MemoryManager.Create();
            var result = new FileManager() { HeaderBlock = block, _disk = diskManager, _memory = memoryManager };
            return (result, null);
        }
        public static (FileManager?, Error?) Create(string path, int blockCount = 800)
        {
            if (File.Exists(path))
            {
                return (null, new Error() { Message = "File already exists" });
            }
            var result = new FileManager() { HeaderBlock = GernerateHeaderBlock() };
            var (disc, err) = DiskManager.Create(path, result.HeaderBlock, blockCount);
            if (err != null)
            {
                return (null, err);
            }
            result._disk = disc;
            //Inser Blockni Qushishim kerak
            result._memory = MemoryManager.Create();
            return (result, null);

        }
        private static Block GernerateHeaderBlock()
        {
            //BUG: Default Blokni yaratish kerak
            Block block = new Block() { };
            return block;
        }
        public void Dispose()
        {
            _disk?.Dispose();
            _memory?.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
        
    }

}
