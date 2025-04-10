using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using Core.Models;

using FileManager.Blocks;
using FileManager.Configs;
using FileManager.Errors;
using FileManager.Extensions;

namespace FileManager.Managers
{
    public sealed class DiskManager : IDisposable
    {
        #region Constructor
        FileStream _stream;
        FileStreamOptions option;
        public static string _path { get; private set; }
        private DiskManager(string path, BaseBlock block, int fileSize)
        {
            _path = path;
            //Bu yerda File path buicha tekshirish kerak
            CreateOption(FileMode.Create);
            _stream = new FileStream(_path, option);
            _stream.SetLength(fileSize);
            SaveBlock(block);
        }
        public DiskManager(string path)
        {
            _path = path;
            CreateOption();
            _stream = new FileStream(_path, option);
        }
        public static (DiskManager?, Error?) Create(
            [Required][NotNull] string path,
            [Required] BaseBlock block,
            [Required] int fileSize)
        {
            if (File.Exists(path))
            {
                //Error qaytish kerak
                return (null, Err.FileExist);
            }
            DiskManager result = new(path, block, fileSize);
            return (result, null);
        }
        #endregion


        public (bool, Error?) SaveBlock(BaseBlock block)
        {
            Span<byte> bytes = stackalloc byte[FileConfig.BlockSize];
            block.Serialize(ref bytes);
            SetByteData(block.PageId, ref bytes);
            return (true, null);
        }
        public (bool, Error?) SetByteData(ulong pageId, ref Span<byte> data)
        {
            _stream.Seek(GetPostion(pageId), SeekOrigin.Begin);
            _stream.Write(data);
            return (true, null);
        }

        public (BaseBlock?, Error?) ReadBlock(uint blockId)
        {

            _stream.Seek(GetPostion(blockId), SeekOrigin.Begin);
            Span<byte> buffer = stackalloc byte[FileConfig.BlockSize];
            var readCount = _stream.Read(buffer);
            return BlockExtension.GenerateBlock(ref buffer);
        }
        public (SectorBlock?, Error?) ReadHeadBlock(uint blockId)
        {
            return (null, null);
        }

        #region PriveteMethods
        uint GetPostion(ulong blockId)
        {
            return 0;
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
