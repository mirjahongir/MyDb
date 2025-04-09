using Core.Models;
using FileManager.Configs;
using FileManager.Extensions;
using FileManager.Models;

namespace FileManager.Managers
{
    public sealed class DiskManager : IDisposable
    {
        #region Constructor
        FileStream _stream;
        FileStreamOptions option;
        public static string _path { get; private set; }
        private DiskManager(string path, Block block, int fileSize)
        {
            _path = path;
            //Bu yerda File path buicha tekshirish kerak
            CreateOption(FileMode.Create);
            _stream = new FileStream(_path, option);
            _stream.SetLength(fileSize);
        }
        public DiskManager(string path)
        {
            _path = path;
            CreateOption();
            _stream = new FileStream(_path, option);
        }
        public static (DiskManager, Error?) Create(string path, Block block)
        {
            if (File.Exists(path))
            {
                //Error qaytish kerak
                return (null, new Error() { Message = "File already exists" });
            }
            DiskManager result = new(path, block, 0);
            return (result, null);
        }
        #endregion


        public (bool, Error?) SaveBlock(Block block)
        {
            return (false, null);
        }
        public (bool, Error?) SetByteData(uint pageId, ref Span<byte> data)
        {
            _stream.Seek(GetPostion(pageId), SeekOrigin.Begin);
            _stream.Write(data);
            return (true, null);
        }

        public (Block, Error) ReadBlock(uint blockId)
        {

            _stream.Seek(GetPostion(blockId), SeekOrigin.Begin);
            Span<byte> buffer = stackalloc byte[FileConfig.BlockSize];
            var readCount = _stream.Read(buffer);
            return BlockExtension.GenerateBlock(ref buffer);

        }

        #region PriveteMethods
        uint GetPostion(uint blockId)
        {
            var position = FileConfig.BlockSize * blockId;
            if (FileLength > position + FileConfig.BlockSize)
            {
                //Bug: Error Qaytarish kerak
            }
            return position;
        }
        void CreateOption(FileMode mode = FileMode.Open)
        {
            option = new FileStreamOptions()
            {
                Access = FileAccess.ReadWrite,
                BufferSize = FileConfig.BlockSize,
                Mode = mode,//FileMode.OpenOrCreate,
                Options = FileOptions.RandomAccess,
                PreallocationSize = 8192 * 1024,
                Share = FileShare.None,
            };
            return;
        }
        public void Dispose()
        {
            _stream.Flush();
            _stream.Dispose();
            GC.SuppressFinalize(this);
        }
        long FileLength
        {
            get
            {
                if (_stream != null)
                    return _stream.Length;
                return 0;
            }
        }
        #endregion
    }
}
