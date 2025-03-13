
using System.Data;

using DbEnums.Enums.Parser;

namespace DbEnums.Config
{
    public static class GlobalConfig
    {

    }
    public static class KeyConfig
    {
        public static Dictionary<string, CmdType> _cmdTypes;
        public static Dictionary<string, CmdType> CmdTypes
        {
            get
            {
                if (_cmdTypes != null) return _cmdTypes;
                _cmdTypes = new Dictionary<string, CmdType>()
                {
                    {"create", CmdType.Create}
                };
                return _cmdTypes;
            }
        }
    }

}
