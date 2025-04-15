using FileManager.Blocks;

namespace FileManager.Managers.Memories
{
    internal class FileInfoManager
    {
        public event Func<FileInfoPage, ValueTask> SaveFileInfo;
        public FileInfoPage FileInfo;
        public FileInfoManager(FileInfoPage fileInfoPage)
        {
            FileInfo = fileInfoPage;
        }
        public FileInfoManager Create()
        {
            return null;
        }
    }

}
