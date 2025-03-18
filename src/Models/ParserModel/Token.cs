using DbEnums.Enums.Parser;

namespace Models.ParserModel
{
    public sealed class Token
    {
        public string? Value { get; set; }
        public CmdType CmdType { get; private set; } = CmdType.NONE;
        public TokenType TokenType { get; private set; } = TokenType.None;
        public KeyType KeyType { get; private set; } = KeyType.None;
        public void SetTokenType(TokenType tokenType)
        {
            TokenType = tokenType;
        }
        public void SetTokenType(CmdType cmd)
        {
            CmdType = cmd;
            TokenType = TokenType.Command;
        }
        public void SetTokenType(KeyType keyType)
        {
            KeyType = keyType;
            TokenType = TokenType.KeyWords;
        }
        public Token? Left { get; set; }
        public Token? Right { get; set; }

    }
}
