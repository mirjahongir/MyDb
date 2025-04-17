
namespace Core.Enums
{
    public enum BsonType : byte
    {
        MinValue = 0,

        Null,

        #region Number
        #region 8 bit 1 byte
        Byte, // shuni Boolean qilsa buladi
        #endregion

        #region 16 bit 2 byte
        Int16,
        UInt16,
        #endregion

        #region 32 bit 4 byte
        Int32,
        UInt32,
        #endregion

        #region 64 bit 8 byte
        Int64,
        Uint64,
        Double,
        #endregion

        #region 128 bit 16 byte
        Decimal,
        #endregion
        #endregion

        String,

        Document,
        Array,

        Binary,
        ObjectId,
        Guid,

        DateTime,

       
    }
}
