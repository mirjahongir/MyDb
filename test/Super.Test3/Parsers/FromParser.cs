using Super.Test3.Enums;
using Superpower;
using Superpower.Parsers;

namespace Super.Test3.Parsers
{
    public class FromStatement
    {
        public string TableName { get; internal set; }
    }
    public static class FromParser
    {
        public static TokenListParser<SqlToken, FromStatement> Froms =
            from froms in Token.EqualTo(SqlToken.From)
            from table in SqlTokenizer.Identifier
            select new FromStatement() {TableName= table };
    }
}
