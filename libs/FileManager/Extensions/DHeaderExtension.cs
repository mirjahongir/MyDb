
using System.Buffers.Binary;

using Core.Models;

using FileManager.Enums;
using FileManager.Models;

namespace FileManager.Extensions
{
    public static class DHeaderExtension
    {
        public static (DataHeader, Error?) GetHeaderById(int id, ref Memory<byte> data)
        {
            var oldSpan = data.Span.Slice(id * FileConfig.HeaderDataSize, FileConfig.HeaderDataSize);
            return (new DataHeader
            {
                // StartPosition va Lengthni span'dan o'qish
                LastPosition = BinaryPrimitives.ReadUInt16LittleEndian(oldSpan.Slice(0, 2)), // 2 baytni StartPosition sifatida o'qish
                Length = BinaryPrimitives.ReadUInt16LittleEndian(oldSpan.Slice(2, 2)), // 2 baytni Length sifatida o'qish
                DataStatus = (Status)oldSpan[4] // 1 baytni DataStatus sifatida o'qish
            }, null);
        }
        public static (DataHeader, Error?) GenerateNewDataHeader(DataHeader oldHeader, ref Memory<byte> newData)
        {
            var newHeader = new DataHeader
            {
                LastPosition = (ushort)(oldHeader.LastPosition - newData.Length),
                Length = (ushort)newData.Length,
                 DataStatus= Status.Active
            };
            return (newHeader, null);
        }
    }
}
