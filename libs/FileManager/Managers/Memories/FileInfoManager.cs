using FileManager.Blocks;

namespace FileManager.Managers.Memories
{
    internal class FileInfoManager
    {
        public event Func<FileInfoPage, ValueTask> SaveFileInfo;

        #region Default Constructor
        private FileInfoPage _fileInfo;
        public FileInfoPage FileInfo => _fileInfo;
        private FileInfoManager(FileInfoPage fileInfoPage)
        {
            _fileInfo = fileInfoPage;
        }
        public static FileInfoManager Create(FileInfoPage fileManager)
        {
            var result = new FileInfoManager(fileManager);
            // Bu yerda Create Sector qilishim kerak va index Page yaratish kerak
            return result;
        }
        public static FileInfoManager Open(DiskManager diskManager)
        {
            var owner = diskManager.ReadBlockById(0);
            var fileInfo = FileInfoPage.Deserialize(owner.Memory);
            if (fileInfo == null)
            {
                return null;
            }
            var result = new FileInfoManager(fileInfo);
            // bu yerda Sectorlarni olaman va sectorlarni uqib chiqaman

            return result;
        }
        #endregion
        public List<FileInfoItem> GetSectors()
        {

            if (FileInfo.Sections == null)
            {
                FileInfo.Sections = [];
            }
            return FileInfo.Sections;
        }
        public void AddSector(SectorPage sector)
        {
            _fileInfo.Sections.Add(new FileInfoItem() { PageId = sector.PageId });
            SaveFileEvent();
        }
        async ValueTask SaveFileEvent()
        {
            if (SaveFileInfo == null) return;
            foreach (Func<FileInfoPage, ValueTask> handler in SaveFileInfo.GetInvocationList().Cast<Func<FileInfoPage, ValueTask>>())
            {
                await handler.Invoke(_fileInfo);
            }
        }
    }

}
