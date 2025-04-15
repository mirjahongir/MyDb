using FileManager.Blocks;
using FileManager.Managers.Memories;

namespace FileManager.Managers
{
    internal class MyFileManager : IDisposable
    {
        #region Default Manager
        DiskManager _disk;
        #region Memory
        readonly DataMemoryManager _dataMemory;
        readonly FileInfoManager _fileManager;
        readonly IndexMemoryManager _index;
        readonly SectorMemoryManager _sector;
        #endregion
        internal MyFileManager(
            DiskManager diskManager,
            SectorMemoryManager sector,
            IndexMemoryManager index,
            FileInfoManager fileInfo,
            DataMemoryManager dataMemory)
        {
            #region SetModel
            _sector = sector;
            _disk = diskManager;
            _fileManager = fileInfo;
            _index = index;
            _dataMemory = dataMemory;
            #endregion
            _sector.SaveSector += SectorSave;
            _index.SaveIndex += SaveIndex;
            _dataMemory.SaveDataPage += SaveDataPage;
            _fileManager.SaveFileInfo += SaveFileInfo;
            _disk = diskManager;
        }
        public static MyFileManager Create(string path)
        {
            return CreateFileInfoManagerExtension.Create(path);
        }
        public static MyFileManager Open(string path)
        {
            return OpenFileInfoManagerExtension.Open(path);
        }

        #endregion

        #region Event
        private async ValueTask SaveFileInfo(FileInfoPage page)
        {
            var memory = page.Serialize();
            await _disk.SaveBlockAsync(0, memory.Memory);
            memory.Dispose();
        }
        private ValueTask SaveDataPage(DataPage page)
        {
            var (memory, error) = page.Serialize();
            _disk.SaveBlock(page.PageId, memory.Memory);
            memory.Dispose();
            return ValueTask.CompletedTask;
        }
        private ValueTask SaveIndex(IndexPage page)
        {
            var memory = page.Serialize();
            _disk.SaveBlock(page.PageId, memory.Memory);
            memory.Dispose();
            return ValueTask.CompletedTask;
        }
        private async ValueTask SectorSave(SectorPage page)
        {
            var (memory, err) = page.Serialize();
            if (err != null)
            {

            }
            await _disk.SaveBlockAsync(page.PageId, memory.Memory);
            memory.Dispose();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    internal static class CreateFileInfoManagerExtension
    {
        public static MyFileManager Create(string path)
        {
            var diskManager = DiskManager.Create(path);
            var fileManager = CreateFileInfo(diskManager);
            var sectorManager = CreateSectorManager(diskManager, fileManager);
            var indexMemoryManager = CreateIndexMemoryManager(fileManager);
            var dataManager = CreateDataManager();
            return new MyFileManager(
                diskManager,
                sectorManager,
                indexMemoryManager,
                fileManager,
                dataManager
                );
        }
        #region Private
        static FileInfoManager CreateFileInfo(DiskManager disk)
        {
            var fileInfo = FileInfoPage.Create(disk.Path);
            disk.SetFileSize(583 + 2);
            FileInfoManager manager = new(fileInfo);

            var memory = fileInfo.Serialize();
            disk.SaveBlock(0, memory.Memory);
            memory.Dispose();

            return manager;
        }
        static SectorMemoryManager CreateSectorManager(
           DiskManager diskManager,
           FileInfoManager fileInfo)
        {

        }
        static IndexMemoryManager CreateIndexMemoryManager(FileInfoManager fileInfo)
        {

        }
        static DataMemoryManager CreateDataManager()
        {

        }
        #endregion
    }
    internal static class OpenFileInfoManagerExtension
    {
        public static MyFileManager Open(string path)
        {
            var diskManager = DiskManager.Open(path);
            var fileManager = OpenFileManager(diskManager);
            var sectorManager = OpenSectorManager();
            var indexManager = IndexManager();
            var dataManager = DataManager();
            return new MyFileManager(diskManager,
                sectorManager,
                indexManager,
                fileManager,
                dataManager);
        }
        static FileInfoManager OpenFileManager(DiskManager diskManager)
        {

        }
        static SectorMemoryManager OpenSectorManager()
        {

        }
        static IndexMemoryManager IndexManager()
        {

        }
        static DataMemoryManager DataManager()
        {

        }
    }

}
