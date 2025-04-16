using FileManager.Blocks;

namespace FileManager.Managers.Memories
{
    internal class DataMemoryManager
    {
        public event Func<DataPage, ValueTask> SaveDataPage;
        private SectorMemoryManager _sector;
        private DataMemoryManager(SectorMemoryManager sectorManager)
        {
            _sector = sectorManager;
        }
        public static DataMemoryManager Create(SectorMemoryManager sectorManager)
        {
            var result = new DataMemoryManager(sectorManager);
            return result;
        }
        public static DataMemoryManager Open(SectorMemoryManager sectorManager)
        {
            var result = new DataMemoryManager(sectorManager);
            return result;
        }




    }

}
