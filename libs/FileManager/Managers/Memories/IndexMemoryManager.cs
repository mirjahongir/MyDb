using FileManager.Blocks;

namespace FileManager.Managers.Memories
{
    internal class IndexMemoryManager
    {
        private IndexMemoryManager()
        {

        }
        public event Func<IndexPage, ValueTask> SaveIndex;
        public static IndexMemoryManager Create()
        {
            var result = new IndexMemoryManager();
            return result;
        }


    }

}
