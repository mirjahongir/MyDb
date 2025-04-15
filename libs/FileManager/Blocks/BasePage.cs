using System.Buffers;
using System.Buffers.Binary;

using Core.Extensions;
using Core.Models;

using FileManager.Enums;

namespace FileManager.Blocks
{
    //12 byte asosiy page bu uzi diskda saqlanmaydi balki nasledovat qilingan pagelar saqlanadi
    public class BasePage
    {
        public static byte BasePageSize = 12;
        //4 byte
        public uint PageId;
        //1 byte
        public PageType PageType;
        // 2 byte
        public ushort Hash;
        //2 byte
        public ushort UsedBytes;

        //nasledovat qilingan klasslar hajmini belgilaydi
        // public required IMemoryOwner<byte> Data;
        // 3 byte 
    }
    public static class BasePageExtension
    {
        public static (T, Error?) DeserializeBasePage<T>(ref Span<byte> span)
            where T : BasePage
        {
            T result = default(T);
            // 0,1,2,3
            result.PageId = span.Slice(0, 4).ToUint32();
            //BitConverter.ToUInt32(span.Slice(0, 4));
            //4
            result.PageType = (PageType)span[4];
            // 5,6
            result.Hash = span.Slice(5, 2).ToUint16();
            //7,8
            result.UsedBytes = span.Slice(7, 2).ToUint16();
            return (result, null);
        }
        public static void SetHash<T>(this T page, ref Span<byte> span)
           where T : BasePage
        {
            var hash = ToHash(ref span);
            page.Hash = hash;
        }
        public static (bool, Error) SerializeBasePage<T>(this T model, ref Span<byte> span)
            where T : BasePage
        {
            model.PageId.ToBytes(ref span);
            span[4] = (byte)model.PageType;
            var slice = span.Slice(7, 2);
            model.UsedBytes.ToBytes(ref slice);
            //TODO:
            //BUG:
            //Hasni hisoblashni qilish kerak
            return (true, null);
        }
        public static ushort ToHash(ref Span<byte> pageData)
        {
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

        //public static ValueTuple<ulong, BlockType, ushort> DeserializeBasePage<T>(this T model, ref Span<byte> span)
        //   where T : BaseBlock // Removed the redundant 'class' constraint
        //{
        //    model.PageId = BitConverter.ToUInt32(span.Slice(0, 4));
        //    model.BlockType = (BlockType)span[8];
        //    model.Hash = BitConverter.ToUInt16(span.Slice(9, 2));

        //    return (model.PageId, model.BlockType, model.Hash);
        //}
        //// 20 Hashni tug`irlash kerak
        //public static (bool, Error) SerializeBasePage<T>(this T block, ref Span<byte> span)
        //    where T : BaseBlock // Removed the redundant 'class' constraint
        //{

        //    // Fix: Use `CopyTo` method to copy the byte array into the span
        //    BitConverter.GetBytes(block.PageId).CopyTo(span.Slice(0, 8));
        //    span[8] = (byte)block.BlockType;
        //    BitConverter.GetBytes(block.Hash).CopyTo(span.Slice(9, 2));
        //    //  BitConverter.GetBytes(block.NextPageId).CopyTo(span.Slice(11, 8));
        //    return (true, null); // Placeholder return to ensure method compiles
        //}
    }
}
