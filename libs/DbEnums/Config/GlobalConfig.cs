
using System.Data;

using DbEnums.Enums.Parser;

namespace DbEnums.Config
{
    public static class GlobalConfig
    {

    }
    public static class KeyConfig
    {
        #region KeyWords
        static Dictionary<string, KeyType>? _keyWords;
        public static Dictionary<string, KeyType> KeyWords
        {
            get
            {
                if (_keyWords != null) return _keyWords;
                _keyWords = new Dictionary<string, KeyType>()
                {
                    {"database", KeyType.Database},
                    {"table", KeyType.Table},
                    {"from", KeyType.From},
                    {"where", KeyType.Where}
                };
                return _keyWords;
            }
        }
        #endregion

        #region CMDTYPE
        static Dictionary<string, CmdType>? _cmdTypes;
        public static Dictionary<string, CmdType> CmdTypes
        {
            get
            {
                if (_cmdTypes != null) return _cmdTypes;
                _cmdTypes = new Dictionary<string, CmdType>()
                {
                    {"create", CmdType.CREATE}
                };
                return _cmdTypes;
            }
        }
        #endregion
        #region Token

        static Dictionary<string, TokenType>? _token;
        public static Dictionary<string, TokenType> Token
        {
            get
            {
                if (_token is not null) return _token;
                _token = new Dictionary<string, TokenType>();
                _token.Add("=", TokenType.Equal);
                _token.Add("!=", TokenType.NotEqual);
                _token.Add(">", TokenType.Greater);
                _token.Add("<", TokenType.Less);
                return _token;
            }
        }
        #endregion Token
    }

}
