using Parser.Enums;

using Superpower;
using Superpower.Parsers;

namespace Parser.Parsers
{
    public class SelectStatement
    {
        public string[]? Columns { get; set; }
        public FromStatement? FromStatement { get; set; }
        public WhereStatement? WhereStatement { get; set; }
    }
    public class SelectParser
    {
        public static TokenListParser<SqlToken, SelectStatement> Selects =
            from selects in Token.EqualTo(SqlToken.Select)
            from columns in SqlTokenizer.ColumnList.Or(Token.EqualTo(SqlToken.Star).Value(new[] { "*" }))
            from froms in FromParser.Froms
            from whereState in WhereParser.Where
                 .OptionalOrDefault()
            select new SelectStatement()
            {
                Columns = columns,
                FromStatement = froms,
                WhereStatement = whereState
            };
    }
    public class SelectTest
    {
        static string SelectQueryString = "SELECT * FROM Talabalar WHERE Id = 1 ;";
        public static SelectStatement SelectMethod()
        {
            var tokenizer = SqlTokenizer.Instance;
            var tokens = tokenizer.Tokenize(SelectQueryString);
            var result = SelectParser.Selects.Parse(tokens);
            return result;

        }

    }
}
