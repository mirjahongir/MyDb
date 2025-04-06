
namespace Parser.Enums
{
    public static class CommandState
    {
        private static Dictionary<string, CommandType> _cmdType;
        internal static Dictionary<string, CommandType> CmdType
        {
            get
            {
                if (_cmdType is null)
                {
                    _cmdType = new Dictionary<string, CommandType>(StringComparer.OrdinalIgnoreCase) {
                            { "select",CommandType.SELECT},
                            { "alter", CommandType.ALTER},
                            { "insert", CommandType.INSERT},
                            { "delete", CommandType.DELETE},
                            {"update", CommandType.UPDATE},
                        };
                }
                return _cmdType;
            }
        }

        public static (CommandType, int) GetCommand(ref string str)
        {
            ReadOnlySpan<char> span = str.AsSpan();
            int spaceIndex = span.IndexOf(' ');
            if (spaceIndex == -1) return (CommandType.NONE, 0);

            ReadOnlySpan<char> cmdSpan = span.Slice(0, spaceIndex).Trim();

            // `cmdSpan` ni `Dictionary` ga mos keladigan string formatida olish
            if (CmdType.TryGetValue(cmdSpan.ToString(), out CommandType type))
                return (type, spaceIndex);

            return (CommandType.NONE, spaceIndex);
        }
    }
    public enum CommandType : ushort
    {
        NONE,
        CREATE,
        ALTER,
        INSERT,
        UPDATE,
        SELECT,
        DELETE,
    }
}
