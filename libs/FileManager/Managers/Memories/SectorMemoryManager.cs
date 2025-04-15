using FileManager.Blocks;

namespace FileManager.Managers.Memories
{
    internal class SectorMemoryManager
    {
        public SectorMemoryManager() { }
       
        // Asinxron delegate: engil va tez
        public event Func<SectorPage,ValueTask> SaveSector;
        public async ValueTask sdcsd()
        {
            var handlers = SaveSector?.GetInvocationList();
            if (handlers == null) return;
            var tasks = new Task[handlers.Length];
            for (int i = 0; i < handlers.Length; i++)
            {
                try
                {
                    // Har bir handlerni parallel ishlatish
                    tasks[i] = ((Func<Task>)handlers[i])();

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Handler exception: {ex.Message}");
                }
            }

            try
            {
                await Task.WhenAll(tasks); // Barchasini parallel kutish
            }
            catch (Exception ex)
            {
                Console.WriteLine($"One or more handlers failed: {ex.Message}");
            }
        }
    }
}
