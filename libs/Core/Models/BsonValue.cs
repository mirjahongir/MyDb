using Core.Enums;

namespace Core.Models
{
    public class BsonValue : IComparable<BsonValue>, IEquatable<BsonValue>, IFormattable//, IConvertible
    {
        #region Default Constructor
        public BsonValue()
        {
            this.Type = BsonType.Null;
            this.RawValue = null;
        }
        #region Number
        // 8-bit
        public BsonValue(byte value)
        {
            RawValue = value;
            Type = BsonType.Byte;
        }

        // 16-bit
        public BsonValue(short value)
        {
            RawValue = value;
            Type = BsonType.Int16;
        }

        public BsonValue(ushort value)
        {
            RawValue = value;
            Type = BsonType.UInt16;
        }

        // 32-bit
        public BsonValue(int value)
        {
            RawValue = value;
            Type = BsonType.Int32;
        }

        public BsonValue(uint value)
        {
            RawValue = value;
            Type = BsonType.UInt32;
        }

        // 64-bit
        public BsonValue(long value)
        {
            RawValue = value;
            Type = BsonType.Int64;
        }

        public BsonValue(ulong value)
        {
            RawValue = value;
            Type = BsonType.Uint64;
        }

        public BsonValue(double value)
        {
            RawValue = value;
            Type = BsonType.Double;
        }

        // 128-bit
        public BsonValue(decimal value)
        {
            RawValue = value;
            Type = BsonType.Decimal;
        }
        #endregion

        // String
        public BsonValue(string value)
        {
            RawValue = value;
            Type = BsonType.String;
        }

        // Optional: Boolean (as Byte if you're mapping it to 1-byte)
        public BsonValue(bool value)
        {
            RawValue = value;
            Type = BsonType.Byte; // yoki alohida BsonType.Boolean qo‘shilsa yaxshi bo‘ladi
        }
        // Universal konstruktor (optional)
        public BsonValue(object? value)
        {
            RawValue = value;
            Type = value switch
            {
                null => throw new ArgumentNullException(nameof(value)),
                byte => BsonType.Byte,
                short => BsonType.Int16,
                ushort => BsonType.UInt16,
                int => BsonType.Int32,
                uint => BsonType.UInt32,
                long => BsonType.Int64,
                ulong => BsonType.Uint64,
                double => BsonType.Double,
                decimal => BsonType.Decimal,
                string => BsonType.String,
                bool => BsonType.Byte, // yoki BsonType.Boolean bo‘lsa aniqroq
                _ => throw new NotSupportedException($"Unsupported type: {value.GetType().Name}")
            };
        }
        #endregion

        #region Property
        public BsonType Type { get; set; }
        public virtual object RawValue { get; }
        #endregion

        #region Interface
        public int CompareTo(BsonValue? other)
        {
            //BUG:
            if (other == null) throw new ArgumentNullException("other");
            if (other.Type != Type)
            {
                throw new ArgumentException("");
            }
            switch (this.Type)
            {
                case BsonType.Int32: return this.AsInt32.CompareTo(other.AsInt32);
                case BsonType.UInt32: return this.AsUInt32.CompareTo(other.AsUInt32);
                case BsonType.Int16: return this.AsInt16.CompareTo(other.AsInt16);
                case BsonType.UInt16: return this.AsInt32.CompareTo(other.AsUInt32);
                case BsonType.Int64: return this.AsInt64.CompareTo(other.AsInt64);
                case BsonType.Uint64: return this.AsUInt64.CompareTo(other.AsUInt64);
                default: throw new NotImplementedException();
            }
        }

        public bool Equals(BsonValue? other)
        {
            return this.CompareTo(other) == 0;
        }

        public TypeCode GetTypeCode()
        {
            return Type switch
            {
                BsonType.Byte => TypeCode.Byte,
                BsonType.Int16 => TypeCode.Int16,
                BsonType.UInt16 => TypeCode.UInt16,
                BsonType.Int32 => TypeCode.Int32,
                BsonType.UInt32 => TypeCode.UInt32,
                BsonType.Int64 => TypeCode.Int64,
                BsonType.Uint64 => TypeCode.UInt64,
                BsonType.Double => TypeCode.Double,
                BsonType.Decimal => TypeCode.Decimal,
                BsonType.String => TypeCode.String,

                _ => TypeCode.Object
            };
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            //BUG: 
            return "";
        }
        #endregion

        #region Convert types
        #region Number
        #region 16
        public short AsInt16 => Convert.ToInt16(this.RawValue);
        public ushort UAsInt16 => Convert.ToUInt16(this.RawValue);
        #endregion

        #region 32
        public int AsInt32 => Convert.ToInt32(this.RawValue);
        public uint AsUInt32 => Convert.ToUInt32(this.RawValue);
        #endregion

        #region 64
        public long AsInt64 => Convert.ToInt64(this.RawValue);
        public ulong AsUInt64 => Convert.ToUInt64(this.RawValue);
        #endregion
        #endregion
        #endregion

    }
}
//public byte ToByte(IFormatProvider? provider)
//{
//    throw new NotImplementedException();
//}

//public char ToChar(IFormatProvider? provider)
//{
//    throw new NotImplementedException();
//}

//public DateTime ToDateTime(IFormatProvider? provider)
//{
//    throw new NotImplementedException();
//}

//public decimal ToDecimal(IFormatProvider? provider)
//{
//    throw new NotImplementedException();
//}

//public double ToDouble(IFormatProvider? provider)
//{
//    throw new NotImplementedException();
//}

//public short ToInt16(IFormatProvider? provider)
//{
//    throw new NotImplementedException();
//}

//public int ToInt32(IFormatProvider? provider)
//{
//    throw new NotImplementedException();
//}

//public long ToInt64(IFormatProvider? provider)
//{
//    throw new NotImplementedException();
//}

//public sbyte ToSByte(IFormatProvider? provider)
//{
//    throw new NotImplementedException();
//}

//public float ToSingle(IFormatProvider? provider)
//{
//    throw new NotImplementedException();
//}

//public string ToString(string? format, IFormatProvider? formatProvider)
//{
//    throw new NotImplementedException();
//}

//public string ToString(IFormatProvider? provider)
//{
//    throw new NotImplementedException();
//}

//public object ToType(Type conversionType, IFormatProvider? provider)
//{
//    throw new NotImplementedException();
//}

//public ushort ToUInt16(IFormatProvider? provider)
//{
//    throw new NotImplementedException();
//}

//public uint ToUInt32(IFormatProvider? provider)
//{
//    throw new NotImplementedException();
//}

//public ulong ToUInt64(IFormatProvider? provider)
//{
//    throw new NotImplementedException();
//}