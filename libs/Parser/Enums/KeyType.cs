
namespace Parser.Enums
{
    public static class KeyTypeState
    {
        static Dictionary<string, KeyType> _kT;
        static Dictionary<string, KeyType> KeyTypes
        {
            get
            {
                if (_kT is null)
                {
                    _kT = new Dictionary<string, KeyType>(StringComparer.OrdinalIgnoreCase) {
                        { "where", KeyType.WHERE},
                        { "from", KeyType.FROM},
                        {"database", KeyType.DATABASE },
                        { "table", KeyType.TABLE},
                        {"set",KeyType.SET},

                    };
                }
                return _kT;
            }
        }
        public static bool ChecKeyType(ref string keytype, out KeyType keyType)
        {
            if (KeyTypes.TryGetValue(keytype, out keyType))
            {
                return true;
            }
            return false;
        }
    }
    public enum KeyType
    {
        NONE = 0,

        FROM = 1,
        WHERE = 2,
        DATABASE = 3,
        TABLE = 4,
        SET = 5,

    }
}
