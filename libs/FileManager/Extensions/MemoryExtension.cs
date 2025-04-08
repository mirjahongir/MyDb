
using System.Buffers.Binary;

using FileManager.Models;

namespace FileManager.Extensions
{
    public static class BlockExtension
    {
        public static DataHeader GetHeaderById(int id, ref Memory<byte> data)
        {
            var oldSpan = data.Span.Slice(id * FileConfig.HeaderDataSize, 4);
            return new DataHeader
            {
                // StartPosition va Lengthni span'dan o'qish
                LastPosition = BinaryPrimitives.ReadUInt16LittleEndian(oldSpan.Slice(0, 2)), // 2 baytni StartPosition sifatida o'qish
                Length = BinaryPrimitives.ReadUInt16LittleEndian(oldSpan.Slice(2, 2)) // 2 baytni Length sifatida o'qish
            };
        }

        //Blockni index buicha ma`lumotini olish
        public static Memory<byte> GetData(int index, ref Memory<byte> data)
        {
            var header = GetHeaderById(index, ref data);

            return data.Slice(header.LastPosition - header.Length, header.Length);
        }

        //Blockdagi bush hotirani hisoblab beradi
        public static ushort FreeSpace(int count, ref Memory<byte> data)
        {
            var header = GetHeaderById(count, ref data);
            return FreeSpace(count, ref header, ref data);
        }
        //Blockdagi bush hotirani hisoblab beradi
        public static ushort FreeSpace(int count, ref DataHeader header, ref Memory<byte> data)
        {
            var position = header.LastPosition - header.Length;
            return (ushort)(position - count * FileConfig.HeaderDataSize);

        }

        public static DataHeader GenerateNewDataHeader(DataHeader oldHeader, ref Memory<byte> newData)
        {
            var newHeader = new DataHeader
            {
                LastPosition = (ushort)(oldHeader.LastPosition - newData.Length),
                Length = (ushort)newData.Length
            };
            return newHeader;
        }
        public static void SetDataHeader(ref int count, ref DataHeader newHeader, ref Memory<byte> data)
        {
            var start = count * FileConfig.HeaderDataSize;
            var slice = data.Span.Slice(start, FileConfig.HeaderDataSize);
            slice[0] = (byte)(newHeader.LastPosition & 0xFF);
            slice[1] = (byte)((newHeader.LastPosition >> 8) & 0XFF);
            slice[2] = (byte)(newHeader.Length & 0xFF);
            slice[3] = (byte)((newHeader.Length >> 8) & 0XFF);
            count++;
        }
        public static void AddData(ref int count, ref Memory<byte> data, ref Memory<byte> newData)
        {
            var oldHeader = GetHeaderById(count, ref data);
            var freeSpace = FreeSpace(count, ref oldHeader, ref data);
            if (freeSpace - 4 < newData.Length)
            {
                // Error qaytish kerak
                //BUG: result Error
            }
            var header = GenerateNewDataHeader(oldHeader, ref newData);
            newData.CopyTo(data.Slice(header.LastPosition - header.Length, newData.Length));
            data.Span.Slice(header.LastPosition - header.Length, header.Length).CopyTo(newData.Span);
            SetDataHeader(ref count, ref header, ref data);
        }
    }
    //65535
    public static class MemoryExtension
    {
        public static uint Pow(this int length)
        {
            uint a = 8;
            while (true)
            {
                a = 8 * 2;
                if (length <= a) return a;
            }
        }
        public static ushort GetFastHashCode(ref Memory<byte> memory)
        {
            var pageData = memory.Span;
            ushort checksum = 0;
            int checksumOffset = 8; // pd_checksum offset (PageHeaderData ichida odatda 8-chi baytdan boshlanadi)

            for (int i = 0; i < pageData.Length; i += 2)
            {
                if (i == checksumOffset)
                    continue;

                ushort value = BitConverter.ToUInt16(pageData.Slice(i, 2));
                checksum ^= value;
                checksum = (ushort)((checksum << 1) | (checksum >> 15)); // rotate left
            }

            checksum ^= (ushort)(64579 & 0xFFFF);
            checksum ^= (ushort)((64579 >> 16) & 0xFFFF);

            return checksum;
        }
    }
}
