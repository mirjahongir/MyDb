using FileManager.Blocks;
using FileManager.Managers.Memories;

namespace FileManager.Managers
{
    public class MyFileManager : IDisposable
    {
        #region Default Manager
        DiskManager _disk;
        #region Memory
        readonly DataMemoryManager _dataMemory;
        readonly FileInfoManager _fileManager;
        readonly IndexMemoryManager _index;
        readonly SectorMemoryManager _sector;
        #endregion
        public MyFileManager()
        {
            _sector.SaveSector += SectorSave;
            _index.SaveIndex += SaveIndex;
            _dataMemory.SaveDataPage += SaveDataPage;
            _fileManager.SaveFileInfo += SaveFileInfo;
            _disk= new DiskManager();
        }
        #endregion

        #region Event
        private async Task SaveFileInfo(FileInfo info)
        {
            throw new NotImplementedException();
        }
        private async Task SaveDataPage(DataPage page)
        {
            throw new NotImplementedException();
        }
        private async Task SaveIndex(IndexPage page)
        {
        }
        private async Task SectorSave(SectorPage page)
        {

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion
    }

}
