using Parser.Enums;

using Superpower;
using Superpower.Model;
using Superpower.Parsers;

namespace Parser.Parsers
{
    public class DeleteStatement
    {
        public required string Table { get; set; }
        public WhereStatement WhereStatement { get; set; }
    }
    static class DeleteParser
    {
        public static TokenListParser<SqlToken, DeleteStatement> Delete =
            from delete in Token.EqualTo(SqlToken.Delete)
            from fromTable in Token.EqualTo(SqlToken.From)
            from table in SqlTokenizer.Identifier
            from whereClause in WhereParser.Where.OptionalOrDefault()
            select new DeleteStatement() { Table = table, WhereStatement = whereClause };
    }
    public static class DeleteTest
    {
        static string DeleteSql = "DELETE FROM Talabalar WHERE Id = 1;";
        public static DeleteStatement DeleteMethod()
        {
            var tokenizer = SqlTokenizer.Instance;
            var tokens = tokenizer.Tokenize(DeleteSql);
            var result = DeleteParser.Delete.Parse(tokens);
            return result;
        }
    }
}
