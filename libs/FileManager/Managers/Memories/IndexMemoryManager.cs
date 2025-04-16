using FileManager.Blocks;

namespace FileManager.Managers.Memories
{
    internal class IndexMemoryManager
    {
        public event Func<IndexPage, ValueTask> SaveIndex;
        readonly SectorMemoryManager _sector;
        public IndexMemoryManager(SectorMemoryManager sector)
        {
            _sector = sector;
        }

        public static IndexMemoryManager Create(SectorMemoryManager sector)
        {
            var result = new IndexMemoryManager(sector);
            //
            return result;
        }
        public static IndexMemoryManager Open(DiskManager disk, SectorMemoryManager sector)
        {
            var result = new IndexMemoryManager(sector);
            //
            return result;
        }

    }

}
