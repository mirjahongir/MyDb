using System.Buffers.Binary;


namespace Core.Extensions
{
    public static class ByteConverterExtension
    {
        #region
        public static uint ToUint32(this Span<byte> data)
        {
            return BinaryPrimitives.ReadUInt32LittleEndian(data);
        }
        public static ushort ToUint16(this Span<byte> data)
        {
            return BinaryPrimitives.ReadUInt16LittleEndian(data);
        }
        #endregion
        #region 
        public static void ToBytes(this uint a, ref Span<byte> data)
        {
            BinaryPrimitives.WriteUInt32LittleEndian(data, a);
        }
        public static void ToBytes(this ushort a, ref Span<byte> data)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(data, a);
        }
        public static void ToBytes(this long a, ref Span<byte> data)
        {
            BinaryPrimitives.WriteInt64LittleEndian(data, a);
        }
        public static void ToBytes(this ulong a, ref Span<byte> data)
        {
            BinaryPrimitives.WriteUInt64LittleEndian(data, a);
        }
        #endregion

    }
}
