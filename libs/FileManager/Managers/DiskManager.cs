using FileManager.Config;

namespace FileManager.Managers
{
    public class DiskManager : IDisposable
    {
        #region Disk Manager
        FileStream _stream;
        DiskManager(string path, FileMode mode = FileMode.Open)
        {
            var option = CreateOption(mode);
            _stream = new FileStream(path, option);
        }
        public DiskManager Create(string path)
        {
            var result = new DiskManager(path, FileMode.Create);

            return result;
        }
        public DiskManager Open(string path)
        {
            var result = new DiskManager(path, FileMode.Open);
            return result;
        }
        #endregion
        public void SaveBlock(uint pageId, Memory<byte> data)
        {
            _stream.Seek(pageId * FileConfig.PageSize, SeekOrigin.Begin);
            _stream.Write(data.Span);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageCount">Berilgan sonni 8192 ga kupaytiradi</param>
        public void SetFileSize(uint pageCount)
        {
            var size = FileLength + pageCount * FileConfig.PageSize;
            _stream.SetLength(size);
        }
        long FileLength
        {
            get
            {
                if (_stream != null) return _stream.Length;
                return 0;
            }
        }
        public void Dispose()
        {
            _stream.Flush();
            _stream.Dispose();
            GC.SuppressFinalize(this);
        }
        FileStreamOptions CreateOption(FileMode mode = FileMode.Open)
        {
            return new FileStreamOptions()
            {
                Access = FileAccess.ReadWrite,
                BufferSize = FileConfig.PageSize,
                Mode = mode,//FileMode.OpenOrCreate,
                Options = FileOptions.RandomAccess,
                PreallocationSize = 8192 * 1024,
                Share = FileShare.None,
            };

        }
    }

}
