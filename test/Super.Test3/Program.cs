using Super.Test3.Enums;

using Superpower;
using Superpower.Display;
using Superpower.Parsers;

namespace Super.Test3
{
    public class ISqlStatement
    {

    }
  

    //public static class SqlParser
    //{
    //    static TokenListParser<SqlToken, string> Identifier =
    //        Token.EqualTo(SqlToken.Identifier).Select(t => t.ToStringValue());
    //    static TokenListParser<SqlToken, string> StringLiteral =
    //        Token.EqualTo(SqlToken.String).Select(t => t.ToStringValue());
    //    static TokenListParser<SqlToken, string[]> ColumnList =
    //        Identifier.AtLeastOnceDelimitedBy(Token.EqualTo(SqlToken.Comma));

    //    static TokenListParser<SqlToken, SelectSstatement> Selects =
    //        from selects in Token.EqualTo(SqlToken.Select)
    //        from columns in ColumnList.Or(Token.EqualTo(SqlToken.Star).Value(new[] { "*" }))
    //        from froms in Token.EqualTo(SqlToken.From)
    //        from table in Identifier
    //        from whereClause in (
    //        from whers in Token.EqualTo(SqlToken.Where)
    //        from col in Identifier
    //        from eq in Token.EqualTo(SqlToken.Equal)
    //        from val in StringLiteral
    //        select new WhereClause() { Column = col, Value = val }
    //        ).OptionalOrDefault()
    //        select new SelectSstatement()
    //        {
    //            Columns = columns,
    //            Table = table,
    //            Where = whereClause
    //        };

    
    //    public static TokenListParser<SqlToken, UpdateStatement> Update =
    //          from update in Token.EqualTo(SqlToken.Update)
    //          from table in Identifier
    //          from sets in Token.EqualTo(SqlToken.Set)
    //          from assigments in (
    //          from col in Identifier
    //          from eq in Token.EqualTo(SqlToken.Equal)
    //          from val in StringLiteral
    //          select new Assigment() { Column = col, Value = val }
    //           ).ManyDelimitedBy(Token.EqualTo(SqlToken.Comma))
    //          select new UpdateStatement() { Assigments = assigments, Table = table, };

        

    //   }

    public class Program
    {
        //static string createDatabase = "create database test";
        //static string createTable = "CREATE TABLE Talabalar (\r\n    Id INT,\r\n    Ism VARCHAR(50),\r\n    TugilganYil INT\r\n);";
        //public static void CreateTableMethod()
        //{
        //    var tokenizer = SqlTokenizer.Instance;
        //    var tokens = tokenizer.Tokenize(createTable);
        //    var result = SqlParser.CreateTable.Parse(tokens);
        //    Console.WriteLine(result);
        //}
        //static string updateSet = "UPDATE Talabalar SET Ism = \"Ali\", Yoshi = 25 WHERE Id = 1\r\n;";
        //public static void Update()
        //{
        //    var tokenizer = SqlTokenizer.Instance;
        //    var tokens = tokenizer.Tokenize(updateSet);
        //    var result = SqlParser.Update.Parse(tokens);
        //    Console.WriteLine(result);
        //}
        static void Main(string[] args)
        {

            //Update();
            //CreateTableMethod();
            //return;
            //var tokenizer = SqlTokenizer.Instance;
            //var tokens = tokenizer.Tokenize(createDatabase);
            //var result = SqlParser.CreateDatabase.Parse(tokens);
            //Console.WriteLine(result.Database);

            //var result = SqlParser.Selects.Parse(tokens);
            //Console.WriteLine(result.Table);
            //Console.WriteLine(string.Join(", ", result.Columns));
            //Console.WriteLine(result.Where.Column + " = " + result.Where.Value);
            //var result = SqlParser.Insert.Parse(tokens);
            //Console.WriteLine(result.Table);
            //Console.WriteLine(string.Join(", ", result.Columns));
            //Console.WriteLine(string.Join(", ", result.Values));
            //var result = SqlParser.Update.Parse(tokens);
            //Console.WriteLine(result.Table);
            //foreach (var item in result.Assigments)
            //{
            //    Console.WriteLine(item.Column + " = " + item.Value);
            //}
        }

    }
}
