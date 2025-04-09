using Super.Test3.Enums;
using Superpower;
using Superpower.Parsers;

namespace Super.Test3.Parsers
{
    public class UpdateStatement
    {
        public required string Table { get; set; }
        public required SetAssigment[] Assigments { get; set; }
        public WhereStatement[]? WhereClause { get; internal set; }
    }
    public class SetAssigment
    {
        public string? Column { get; set; }
        public string? Value { get; set; }
    }

    public static class UpdateParser
    {
        public static TokenListParser<SqlToken, UpdateStatement> Update =
             from update in Token.EqualTo(SqlToken.Update)
             from table in SqlTokenizer.Identifier
             from set in Token.EqualTo(SqlToken.Set)
             from assignments in AssigmentParser.ManyDelimitedBy(Token.EqualTo(SqlToken.Comma))
             from whereClause in WhereParser.Where.OptionalOrDefault().ManyDelimitedBy(Token.EqualTo(SqlToken.Comma))
             select new UpdateStatement() { Table = table, Assigments = assignments, WhereClause = whereClause };



        static TokenListParser<SqlToken, SetAssigment> AssigmentParser =
            from column in SqlTokenizer.Identifier
            from equal in Token.EqualTo(SqlToken.Equal)
            from value in SqlTokenizer.StringLiteral.Or(SqlTokenizer.NumberLiteral)
            select new SetAssigment() { Column = column, Value = value };

    }

    public static class UpdateTest
    {
        static string UpdateSql = "UPDATE Talabalar SET Ism = 'Ali', TugilganYil = 2000 WHERE Id = 1;";
        public static UpdateStatement UpdateMethod()
        {
            var tokenizer = SqlTokenizer.Instance;
            var tokens = tokenizer.Tokenize(UpdateSql);
            var result = UpdateParser.Update.Parse(tokens);
            return result;
        }
    }
}
