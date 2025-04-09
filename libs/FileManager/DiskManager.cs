using Core.Models;

using FileManager;
using FileManager.Extensions;
using FileManager.Models;

namespace FileManagers
{
    public sealed class DiskManager : IDisposable
    {
        FileStream _stream;
        FileStreamOptions option;
        public static string _path { get; private set; }
        private DiskManager(string path, Block block, int filePath)
        {

        }
        public DiskManager(string path)
        {
            _path = path;
            option = new FileStreamOptions()
            {
                Access = FileAccess.ReadWrite,
                BufferSize = FileConfig.BlockSize,
                Mode = FileMode.OpenOrCreate,
                Options = FileOptions.RandomAccess,
                PreallocationSize = 8192 * 1024,
                Share = FileShare.None,
            };
            _stream = new FileStream(_path, option);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public long FileLength
        {
            get
            {
                if (_stream != null)
                    return _stream.Length;
                return 0;
            }
        }
        public static (DiskManager, Error?) Create(string path, Block block)
        {
            DiskManager result = new(path, block, 0);
            return (result, null);
        }

        #region Block
        public (bool, Error?) SaveBlock(Block block)
        {
            return (false, null);
        }
        public (bool, Error?) SetByteData(UInt32 pageId, ref Span<byte> data)
        {
            _stream.Seek(GetPostion(pageId), SeekOrigin.Begin);
            _stream.Write(data);
            return (true, null);
        }
        #endregion

        public (Block, Error) ReadBlock(uint blockId)
        {

            _stream.Seek(GetPostion(blockId), SeekOrigin.Begin);
            Span<byte> buffer = stackalloc byte[FileConfig.BlockSize];
            var readCount = _stream.Read(buffer);
            return BlockExtension.GenerateBlock(ref buffer);

        }

        UInt32 GetPostion(uint blockId)
        {
            var position = FileConfig.BlockSize * blockId;
            if (FileLength > position + FileConfig.BlockSize)
            {
                //Bug: Error Qaytarish kerak
            }
            return position;
        }
    }
}
