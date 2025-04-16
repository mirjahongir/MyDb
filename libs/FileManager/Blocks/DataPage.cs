using System.Buffers;
using Core.Extensions;
using Core.Models;
using FileManager.Config;
using FileManager.Enums;

namespace FileManager.Blocks
{
    public sealed class DataPage : BasePage //12 byte
    {
        #region Static Fields
        public static ushort HeaderSize = 5;
        public static ushort DataStartPosition = 12;//=13
        public static ushort DataSize = 8180;
        #endregion
        //8192-12=8180;
        public Memory<byte> Data { get; set; }

        public Span<byte> GetData(ushort i)
        {
            var memory = Data.Span;
            var headerPosition = i * DataPage.HeaderSize;
            var headerMemory = memory.Slice(headerPosition, DataPage.HeaderSize);
            var header = ItemHeader.GenerateHeader(headerMemory);
            return GetData(header);
        }
        public Span<byte> GetData(ItemHeader header)
        {
            var startPosition = header.StartPosition - header.Length;
            return Data.Span.Slice(startPosition, header.Length);
        }
        public void AddData(byte data)
        {

        }
        public void AddData(ref Span<byte> newData)
        {
            //
            var span = Data.Span;
            if (UsedCount == 0)
            {
                UsedCount++;
                var headers = ItemHeader.GenerateNewHeader(ref newData);
                AddData(headers, ref newData);
                return;
            }
            var header = GetItemHeader(UsedCount);
            var newHeader = ItemHeader.GenerateNewHeader(header, ref newData);
            AddData(newHeader, ref newData);
        }

        public void AddData(ItemHeader header, ref Span<byte> newData)
        {
        }
        public ItemHeader GetItemHeader(ushort id)
        {

            var startPosition = HeaderSize * (id - 1);
            var span = Data.Span;
            var slice = span.Slice(startPosition, HeaderSize);
            return ItemHeader.GenerateHeader(slice);

        }
    }
    //5 byte
    public struct ItemHeader
    {
        public ushort Length;
        public ushort StartPosition;
        public DataStatus DataStatus;
        public static ItemHeader GenerateNewHeader(ItemHeader header, ref Span<byte> data)
        {
            return new ItemHeader()
            {
                Length = (ushort)data.Length,
                StartPosition = (ushort)(DataPage.DataSize - (ushort)1),
                DataStatus = DataStatus.Active
            };
        }
        public static ItemHeader GenerateNewHeader(ref Span<byte> data)
        {
            return new ItemHeader()
            {
                Length = (ushort)data.Length,
                StartPosition = (ushort)(DataPage.DataSize - 1), // Explicit cast added to fix CS0266
                DataStatus = DataStatus.Active
            };
        }
        public static ItemHeader GenerateHeader(Span<byte> data)
        {
            var result = new ItemHeader
            {
                //0,1
                Length = data.Slice(0, 2).ToUint16(),
                //2,3
                StartPosition = data.Slice(2, 2).ToUint16(),
                DataStatus = (DataStatus)data[4]
            };
            return result;
        }
    }
    public static class DataPageExtension
    {
        public static (IMemoryOwner<byte>?, Error?) Serialize(this DataPage page)
        {
            var owner = MemoryPool<byte>.Shared.Rent(FileConfig.PageSize);
            var memory = owner.Memory;
            var span = memory.Span;
            var (isSuccess, error) = page.SerializeBasePage(ref span);
            if (error != null)
            {
                return (null, error);
            }
            var data = page.Data.Span;
            page.SetHash(ref data);
            data.CopyTo(span[DataPage.DataStartPosition..]);
            return (owner, null);
        }

        public static (DataPage?, Error?) Deserialize(this Memory<byte> memory)
        {
            var span = memory.Span;
            var (dataPage, err) = BasePageExtension.DeserializeBasePage<DataPage>(ref span);
            if (err != null)
            {
                return (null, err);
            }
            var dataMemory = memory.Slice(DataPage.DataStartPosition);
            dataPage.Data = dataMemory;
            return (dataPage, null);
        }
    }
}
