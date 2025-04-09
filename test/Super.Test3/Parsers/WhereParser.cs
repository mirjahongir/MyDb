using Super.Test3.Enums;
using Superpower;
using Superpower.Parsers;

namespace Super.Test3.Parsers
{
    public class WhereStatement
    {
        public WhereCondition[]? Condition { get; set; }
    }
    public class WhereCondition
    {
        private WhereCondition()
        {

        }
        public WhereCondition(string column, string value)
        {
            Column = column;
            Value = value;
        }
        public string Column { get; set; }
        public string Value { get; set; }
    }
    static class WhereParser
    {
        public static TokenListParser<SqlToken, WhereStatement> Where =
            from wheres in Token.EqualTo(SqlToken.Where)
            from condition in ConditionParser!.ManyDelimitedBy(Token.EqualTo(SqlToken.Comma)) // Added null-forgiving operator (!)
            select new WhereStatement() { Condition = condition };

        static TokenListParser<SqlToken, WhereCondition> ConditionParser =
            from column in SqlTokenizer.Identifier
            from equal in Token.EqualTo(SqlToken.Equal)
            from value in SqlTokenizer.StringLiteral.Or(SqlTokenizer.NumberLiteral)
            select new WhereCondition(column, value);
    }
}
