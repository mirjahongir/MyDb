using System.Text;

using DbEnums.Config;
using DbEnums.Enums.Parser;

using Models.ParserModel;

namespace Services.Parser
{
    public static class TokenParser
    {
        public static int Trim(ref ReadOnlySpan<char> span, int start)
        {
            for (int i = start; i < span.Length; i++)
            {
                if (!char.IsWhiteSpace(span[i]))
                {
                    return i;
                }
            }
            return span.Length;
        }
        public static char? GetFirstChar(ref ReadOnlySpan<char> span, ref int start)
        {
            for (var i = start; i < span.Length; i++)
            {
                switch (span[i])
                {
                    case ' ':
                        continue;
                    case '\n':
                        continue;
                    case '"':
                        start = i;
                        return '"';
                    default:
                        start = i;
                        return null;
                }

            }
            return null;
        }
        //BUG: GetKeyword should return a Token
        public static string GetKeyword(ref ReadOnlySpan<char> span, ref int start, char? key)
        {
            StringBuilder builder = new();
            for (int i = start; i < span.Length; i++)
            {
                if (key is null && !char.IsWhiteSpace(span[i]))
                {
                    builder.Append(span[i]);
                    continue;
                }
                start = i;
                return builder.ToString();
            }
            return span.Slice(start).ToString();
        }

        public static Token ParseToken(ref ReadOnlySpan<char> span, ref int start)
        {
            Token token = new();
            var key = GetFirstChar(ref span, ref start);
            token.Value = GetKeyword(ref span, ref start, key);
            var str = token.Value.ToLower();
            if (KeyConfig.CmdTypes.TryGetValue(str, out CmdType value))
            {
                token.SetTokenType(value);
            }
            if (KeyConfig.KeyWords.TryGetValue(str, out KeyType keyType))
            {
                token.SetTokenType(keyType);
            }
            if (KeyConfig.Token.TryGetValue(str, out TokenType tokenType))
            {
                token.SetTokenType(tokenType);
            }
            return token;
        }
        

    }
}
