using Core.Models;
using FileManager.Configs;
using FileManager.Enums;
using FileManager.Models;

namespace FileManager.Extensions
{
    public static class BlockExtension
    {
        //Blockni index buicha ma`lumotini olish
        public static (Memory<byte>, Error?) GetData(int index, ref Memory<byte> data)
        {
            var (header, err) = DHeaderExtension.GetHeaderById(index, ref data);
            return (data.Slice(header.LastPosition - header.Length, header.Length), null);
        }

        #region Free Space
        //Blockdagi bush hotirani hisoblab beradi
        public static (ushort, Error?) FreeSpace(int count, ref Memory<byte> data)
        {
            var (header, err) = DHeaderExtension.GetHeaderById(count, ref data);
            return FreeSpace(count, ref header, ref data);
        }
        //Blockdagi bush hotirani hisoblab beradi
        public static (ushort, Error?) FreeSpace(int count, ref DataHeader header, ref Memory<byte> data)
        {
            var position = header.LastPosition - header.Length;
            return ((ushort)(position - (count * FileConfig.HeaderDataSize)), null);
        }
        #endregion

        public static (bool, Error?) SetDataHeader(ref int count, ref DataHeader newHeader, ref Memory<byte> data)
        {
            var start = count * FileConfig.HeaderDataSize;
            var slice = data.Span.Slice(start, FileConfig.HeaderDataSize);
            slice[0] = (byte)(newHeader.LastPosition & 0xFF);
            slice[1] = (byte)((newHeader.LastPosition >> 8) & 0XFF);
            slice[2] = (byte)(newHeader.Length & 0xFF);
            slice[3] = (byte)((newHeader.Length >> 8) & 0XFF);
            count++;
            return (true, null);
        }
        
        public static (DataHeader, Error?) AddData(ref int count, ref Memory<byte> data, ref Memory<byte> newData)
        {
            var (oldHeader, err) = DHeaderExtension.GetHeaderById(count, ref data);
            if (err != null)
            {
                //BUG: result Error
                return (oldHeader, err);
            }
            var (freeSpace, error) = FreeSpace(count, ref oldHeader, ref data);
            if (error != null)
            {
                //BUG: result Error
                return (oldHeader, error);
            }
            if (freeSpace - 4 < newData.Length)
            {
                // Error qaytish kerak
                //BUG: result Error
            }
            (var header, err) = DHeaderExtension.GenerateNewDataHeader(oldHeader, ref newData);
            if (err != null)
            {
                //BUG: result Error
                return (header, err);
            }
            newData.CopyTo(data.Slice(header.LastPosition - header.Length, newData.Length));
            data.Span.Slice(header.LastPosition - header.Length, header.Length).CopyTo(newData.Span);
            SetDataHeader(ref count, ref header, ref data);
            return (header, null);
        }
        
        public static (Block, Error?) GenerateBlock(ref Span<byte> data)
        {
            var block = new Block
            {

            };
            return (block, null);
        }

        #region Serialize and Deserialize
        public static (bool, Error?) Serialize(this Block block, ref Span<byte> data)
        {
            if (data.Length != FileConfig.BlockSize)
            {
                //BUG:
                //Error qaytish kerak
                return (false, new Error());
            }
            // 4 byte PageId
            data[0] = (byte)(block.PageId & 0xFF);
            data[1] = (byte)((block.PageId >> 8) & 0xFF);
            data[2] = (byte)((block.PageId >> 16) & 0xFF);
            data[3] = (byte)((block.PageId >> 24) & 0xFF);
            // 1 byte BlockType
            data[4] = (byte)block.BlockType;
            // 2 byte Count
            data[5] = (byte)(block.Count & 0xFF);
            data[6] = (byte)((block.Count >> 8) & 0xFF);
            // 2 byte Hash
            data[7] = (byte)(block.Hash & 0xFF);
            data[8] = (byte)((block.Hash >> 8) & 0xFF); //Umumiy 9 bayt

            var slice = data.Slice(9, 8182);

            block.Data.Span.CopyTo(slice);

            return (true, null);
        }
        public static (bool, Error?) Serialize(this Memory<Block> blocks, ref Span<byte> data)
        {
            int a = 0;
            foreach (var i in blocks.Span)
            {
                var slice = data.Slice(a, FileConfig.BlockSize);
                var (isSuccess, err) = Serialize(i, ref slice);
            }
            return (true, null);
        }
        public static (Block, Error) Deserialize(ref Span<byte> data)
        {
            Block block = new()
            {
                PageId = (uint)(data[0] | (data[1] << 8) | (data[2] << 16) | (data[3] << 24)),
                BlockType = (BlockType)data[4],
                Count = (ushort)(data[5] | (data[6] << 8)),
                Hash = (ushort)(data[7] | (data[8] << 8)),
                Data = new Memory<byte>(data.Slice(9, 8182).ToArray()) // Fix: Convert Span<byte> to Memory<byte> using ToArray()
            };
            return (block, null);
        }
        public static (Block[], Error?) DeserializeBlocks(ref Span<byte> data)
        {
            List<Block> blocks = [];
            int a = 0;
            while (a * FileConfig.BlockSize < data.Length)
            {
                var slice = data.Slice(a * FileConfig.BlockSize, FileConfig.BlockSize);
                var (block, err) = Deserialize(ref slice);

                blocks.Add(block);
                a++;
            }
            return (blocks.ToArray(), null);
        }
        #endregion
    }
}
