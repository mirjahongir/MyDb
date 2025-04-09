using Core.Models;

using FileManager.Configs;
using FileManager.Errors;
using FileManager.Models;

namespace FileManager.Managers
{

    public class BlockFileManager : IDisposable
    {
        #region Constructor
        DiskManager _disk;
        MemoryManager _memory;
        HeaderBlock HeaderBlock;
        private BlockFileManager()
        {

        }
        public static (BlockFileManager?, Error?) Open(string path)
        {
            if (!File.Exists(path))
            {
                return (null, Err.FileNotFound);
            }
            var diskManager = new DiskManager(path);
            var (block, err) = diskManager.ReadHeadBlock(0);
            if (err != null)
            {
            }
            MemoryManager memoryManager = MemoryManager.Create();
            var result = new BlockFileManager() { HeaderBlock = block, _disk = diskManager, _memory = memoryManager };
            return (result, null);
        }
        public static (BlockFileManager?, Error?) Create(string path, int blockCount = 800)
        {
            if (File.Exists(path))
            {
                return (null, Err.FileExist);
            }
            var result = new BlockFileManager() {
                HeaderBlock = HeaderBlock.CreateDefaultHeader()
            };
            blockCount= FileConfig.BlockSize * blockCount;
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
        public void Dispose()
        {
            _disk?.Dispose();
            _memory?.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion

    }

}
