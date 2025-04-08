using FileManager;

namespace FileManagers
{

    public class FileManager : IDisposable
    {

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

}
