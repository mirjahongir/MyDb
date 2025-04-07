using Parser.Enums;

using Superpower;
using Superpower.Parsers;

namespace Parser.Parsers
{
    public class FromStatement
    {
        public string TableName { get; internal set; }
    }
    static class FromParser
    {
        public static TokenListParser<SqlToken, FromStatement> Froms =
            from froms in Token.EqualTo(SqlToken.From)
            from table in SqlTokenizer.Identifier
            select new FromStatement() { TableName = table };
    }
}
