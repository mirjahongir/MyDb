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

        #region Events
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

        #region Methods
        public void AddData(byte[][] index, byte[][] data)
        {

        }

        #endregion
    }
    internal static class CreateFileInfoManagerExtension
    {
        public static MyFileManager Create(string path)
        {
            var diskManager = DiskManager.Create(path);
            var fileManager = CreateFileInfo(diskManager);
            var sectorManager = SectorMemoryManager.Create(fileManager, diskManager);//  CreateSectorManager(diskManager, fileManager);
            var indexMemoryManager = IndexMemoryManager.Create(sectorManager);//  CreateIndexMemoryManager(sectorManager);
            var dataManager = DataMemoryManager.Create(sectorManager);// CreateDataManager(sectorManager);
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
            //1 ta Sector da ketadigan Blocklar va 2 FileInfoBlock va SectorBlock
            disk.SetFileSize((uint)SectorPage.SectorItemCount + 2);
            //
            FileInfoManager manager = FileInfoManager.Create(fileInfo);
            var owner = fileInfo.Serialize();
            disk.SaveBlock(0, owner.Memory);
            owner.Dispose();
            return manager;
        }
        //static SectorMemoryManager CreateSectorManager(
        //   DiskManager diskManager,
        //   FileInfoManager fileInfo)
        //{
        //    SectorMemoryManager sector = new(fileInfo, diskManager);
        //    return sector;
        //}
        //static IndexMemoryManager CreateIndexMemoryManager(SectorMemoryManager sector)
        //{
        //    IndexMemoryManager result = IndexMemoryManager.Create(sector);
        //    return result;
        //}
        //static DataMemoryManager CreateDataManager(SectorMemoryManager sector)
        //{
        //    var result = DataMemoryManager.Create(sector);
        //    return result;
        //}
        #endregion
    }
    internal static class OpenFileInfoManagerExtension
    {
        public static MyFileManager Open(string path)
        {
            var diskManager = DiskManager.Open(path);
            var fileManager = FileInfoManager.Open(diskManager);// OpenFileManager(diskManager);
            var sectorManager = SectorMemoryManager.Open(fileManager, diskManager);
            var indexManager = IndexMemoryManager.Open(diskManager, sectorManager);
            var dataManger = DataMemoryManager.Open(sectorManager);
            return new MyFileManager(diskManager,
                sectorManager,
                indexManager,
                fileManager,
                dataManger);
        }

    }

}
