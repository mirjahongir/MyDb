using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Core.Models;

using FileManager.Blocks;
using FileManager.Errors;

namespace FileManager.Managers.Memories
{
    internal class SectorMemoryManager
    {
        #region Sector
        // Asinxron delegate: engil va tez
        public event Func<SectorPage, ValueTask> SaveSector;
        readonly FileInfoManager _fileManager;
        readonly DiskManager _disk;
        List<SectorPage> _sectors;
        public SectorMemoryManager(FileInfoManager manager, DiskManager disk)
        {
            _fileManager = manager;
            _sectors = new List<SectorPage>();
            _disk = disk;
        }
        public static SectorMemoryManager Create(FileInfoManager fileManager, DiskManager diskManager)
        {
            return new SectorMemoryManager(fileManager, diskManager);
        }
        public static SectorMemoryManager Open(FileInfoManager fileManager, DiskManager diskManager)
        {
            return new SectorMemoryManager(fileManager, diskManager);
        }
        #endregion


        public (SectorPage?, Error?) GetSectorPage(uint pageId)
        {
            //Umumiy spisokdan qidirayapman
            var existSector = _sectors.FirstOrDefault(m => m.PageId == pageId);
            if (existSector != null) return (existSector, null);
            //agar bulmasa FileManagerdan sector bormi yuqmi 
            var sectors = _fileManager.GetSectors();
            var exist = sectors.FirstOrDefault(m => m.PageId == pageId);
            if (exist.PageId == 0)
            {
                return (null, Err.SectorNotFound);
            }
            var owner = _disk.ReadBlockById(pageId);
            (existSector, var error) = SectorPage.Deserialize(owner.Memory);
            if (existSector != null)
                _sectors.Add(existSector);

            owner.Dispose();
            return (existSector, null);
        }

        public async ValueTask<(SectorPage?, Error?)> CreateSector(uint pageId)
        {
            SectorPage newSector = new()
            {
                PageId = pageId,
                PageType = Enums.PageType.SectorPage,
                SectionCount = 0,
                Sections = [],
                UsedCount = 0,
            };
            await SaveSector.Invoke(newSector);
            _fileManager.AddSector(newSector);
            return (newSector, null);
        }
    }
}
