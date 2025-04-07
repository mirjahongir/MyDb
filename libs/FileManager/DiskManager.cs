using System;

using FileManager;
using FileManager.Models;

namespace FileManagers
{
    public sealed class DiskManager : IDisposable
    {
        FileStream _stream;
        FileStreamOptions option;
        Block _headerBlock;
        public DiskManager(string path)
        {
            option = new FileStreamOptions()
            {
                Access = FileAccess.ReadWrite,
                BufferSize = FileConfig.BlockSize,
                Mode = FileMode.OpenOrCreate,
                Options = FileOptions.RandomAccess,
                PreallocationSize = 10 * 1024 * 1024,
                Share = FileShare.None,
            };
            _stream = new FileStream(path, option);
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
        public static DiskManager Create(string path, Block block)
        {
            //BUG: method not implement
            throw new NotImplementedException();
        }

        #region Block
        public void SaveBlock(Block block)
        {
            var pageId = block.Header.BlockNumber;
            Span<byte> data = stackalloc byte[FileConfig.BlockSize];
            Serialize(ref data, ref block);
            SetByteData(pageId,ref data);
        }
        public void SetByteData(UInt32 pageId, ref Span<byte> data)
        {
            _stream.Seek(GetPostion(pageId), SeekOrigin.Begin);
            _stream.Write(data);
        }
        #endregion


        public void Serialize(ref Span<byte> data, ref Block block)
        {
            block.Header.ToSpan(ref data);
            block.Data.Span.CopyTo(data.Slice(10));
        }

        public Block Deserialize(ref byte[] bytes)
        {
            return null;
        }
        public Block ReadBlock(uint blockId)
        {
            _stream.Seek(GetPostion(blockId), SeekOrigin.Begin);
            var buffer = new byte[FileConfig.BlockSize];
            var readCount = _stream.Read(buffer, 0, FileConfig.BlockSize);
            return Deserialize(ref buffer);
        }


        public UInt32 GetPostion(uint blockId)
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
