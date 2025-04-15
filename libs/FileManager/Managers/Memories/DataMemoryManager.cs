using FileManager.Blocks;

namespace FileManager.Managers.Memories
{
    internal class DataMemoryManager
    {
        public event Func<DataPage, ValueTask> SaveDataPage;
        private DataMemoryManager() { }
        public static DataMemoryManager Create()
        {
            return new DataMemoryManager();
        }
    }

}
