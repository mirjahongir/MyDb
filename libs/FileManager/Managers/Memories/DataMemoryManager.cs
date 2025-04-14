using FileManager.Blocks;

namespace FileManager.Managers.Memories
{
    internal class DataMemoryManager
    {
        public event Func<DataPage, Task> SaveDataPage;
        private DataMemoryManager() { }
        public static DataMemoryManager Create()
        {

        }
    }

}
