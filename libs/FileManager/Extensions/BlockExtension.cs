using Core.Models;

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
    }
}
