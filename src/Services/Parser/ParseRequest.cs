
using System;
using System.Text;

using DbEnums.Config;
using DbEnums.Enums.Parser;

namespace Services.Parser
{
    public static class ParseRequest
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
        public static (char?, int) GetFirstChar(ref ReadOnlySpan<char> span, int start)
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
                        return ('"', i);
                    default:
                        return (null, i);
                }

            }
            return (null, span.Length);
        }
        //BUG: GetKeyword should return a Token
        public static string GetKeyword(ref ReadOnlySpan<char> span, int start, char? key)
        {
            StringBuilder builder = new();
            for (int i = start; i < span.Length; i++)
            {
                if (key is null && !char.IsWhiteSpace(span[i]))
                {
                    builder.Append(span[i]);
                    continue;
                }
                return builder.ToString();
            }
            return span.Slice(start).ToString();
        }
        public static Token ParseToken(ref ReadOnlySpan<char> span, int start)
        {
            Token token = new();
            var (key, newStart) = GetFirstChar(ref span, start);
            token.Value = GetKeyword(ref span, newStart, key);
            var str = token.Value.ToLower();
            if (KeyConfig.CmdTypes.ContainsKey(str))
            {
                token.TokenType = TokenType.Command;
            }
            return token;
        }
    }
    public class Token
    {
        public string? Value { get; set; }
        public TokenType TokenType { get; set; }

    }
    public class ParseExpression
    {

    }
}
