using Superpower.Parsers;
using Superpower;
using Parser.Enums;

namespace Parser.Parsers
{
    public class InsertStatement
    {
        public string? Table { get; set; }
        public string[]? Columns { get; set; }
        public string[]? Values { get; set; }
    }
    public static class InsertParser
    {

    //    public static TokenListParser<SqlToken, InsertStatement> Insertw =
    //Token.EqualTo(SqlToken.Insert).SelectMany(insert =>
    //Token.EqualTo(SqlToken.Into).SelectMany(intos =>
    //SqlTokenizer.Identifier.SelectMany(table =>
    //Token.EqualTo(SqlToken.LParen).SelectMany(lparen1 =>
    //SqlTokenizer.ColumnList.SelectMany(columns =>
    //Token.EqualTo(SqlToken.RParen).SelectMany(rparen1 =>
    //Token.EqualTo(SqlToken.Values).SelectMany(values =>
    //Token.EqualTo(SqlToken.LParen).SelectMany(lparen2 =>
    //SqlTokenizer.StringLiteral
    //    .Or(SqlTokenizer.NumberLiteral)
    //    .AtLeastOnceDelimitedBy(Token.EqualTo(SqlToken.Comma))
    //    .SelectMany(vals =>
    //Token.EqualTo(SqlToken.RParen).Select(rparen2 =>
    //    new InsertStatement
    //    {
    //        Columns = columns,
    //        Table = table,
    //        Values = vals
    //    }))))))))));

        public static TokenListParser<SqlToken, InsertStatement> Insert =
         from insert in Token.EqualTo(SqlToken.Insert)
         from intos in Token.EqualTo(SqlToken.Into)
         from table in SqlTokenizer.Identifier
         from lparen1 in Token.EqualTo(SqlToken.LParen)
         from columns in SqlTokenizer.ColumnList
         from rparent1 in Token.EqualTo(SqlToken.RParen)
         from values in Token.EqualTo(SqlToken.Values)
         from lparen2 in Token.EqualTo(SqlToken.LParen)
         from vals in SqlTokenizer.StringLiteral.Or(SqlTokenizer.NumberLiteral).AtLeastOnceDelimitedBy(Token.EqualTo(SqlToken.Comma))
         from rparent2 in Token.EqualTo(SqlToken.RParen)
         select new InsertStatement() { Columns = columns, Table = table, Values = vals };

    }
     static class InsertTest
    {
        static string input = "INSERT INTO users (name, age) VALUES ('John', 30)";
        public static InsertStatement InsertMethod()
        {

            var result = SqlTokenizer.Instance.Tokenize(input);
            var insertResult = InsertParser.Insert.Parse(result);
            return insertResult;

            //if (insertResult.HasValue)
            //{
            //    return insertResult.Value;
            //}
            //else
            //{
            //    throw new Exception("Parsing failed");
            //}
        }
    }
}
