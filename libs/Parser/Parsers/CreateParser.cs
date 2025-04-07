using Superpower.Parsers;
using Superpower;
using Parser.Enums;

namespace Parser.Parsers
{
    public class Create
    {
        public virtual CreateType CreateType { get; }
    }
    public class CreateDatabase : Create
    {
        public override CreateType CreateType { get => CreateType.Database; }
        public string Database { get; set; }
    }
    public class CreateTable : Create
    {
        public override CreateType CreateType => CreateType.Table;
        public required string Table { get; set; }
        public required TableProperty[] Properties { get; set; }
    }
    public class TableProperty
    {
        public string? Column { get; set; }
        public string? Type { get; set; }
    }
    public class CreateSchema : Create
    {
        public override CreateType CreateType => CreateType.Schema;
        public required string SchemaName { get;  set; }
    }

    public static class CreateParser
    {
        static TokenListParser<SqlToken, TableProperty> ColumnParser =
            from name in Token.EqualTo(SqlToken.Identifier)
            from dataType in Token.EqualTo(SqlToken.DataType)
            select new TableProperty() { Column = name.ToStringValue(), Type = dataType.ToStringValue() };


        //  "CREATE TABLE Talabalar (\r\n    Id INT,\r\n    Ism VARCHAR(50),\r\n    TugilganYil INT\r\n);";
        static TokenListParser<SqlToken, Create> CreateTable =
          from create in Token.EqualTo(SqlToken.Create)
          from tableToken in Token.EqualTo(SqlToken.Table)
          from table in SqlTokenizer.Identifier
          from lparen in Token.EqualTo(SqlToken.LParen)
          from columns in ColumnParser.ManyDelimitedBy(Token.EqualTo(SqlToken.Comma))
          from rparen in Token.EqualTo(SqlToken.RParen)
          select (Create)new CreateTable() { Table = table, Properties = columns };

        static TokenListParser<SqlToken, Create> CreateDatabase =
              from create in Token.EqualTo(SqlToken.Create)
              from database in Token.EqualTo(SqlToken.Database)
              from dbName in SqlTokenizer.Identifier
              select (Create)new CreateDatabase() { Database = dbName };

        static TokenListParser<SqlToken, Create> CreateShcema =
            from create in Token.EqualTo(SqlToken.Create)
            from schema in Token.EqualTo(SqlToken.Schema)
            from schemaName in SqlTokenizer.Identifier
            select (Create)new CreateSchema() { SchemaName = schemaName };

        public static TokenListParser<SqlToken, Create> Create =
            CreateTable.Try().Or(CreateDatabase).Try().Or(CreateShcema);

    }

    public class CreateTest
    {
        static string createTableString = "CREATE TABLE Talabalar (\r\n    Id INT,\r\n    Ism VARCHAR(50),\r\n    TugilganYil INT\r\n);";
        public static Create CreateTable()
        {
            var tokenizer = SqlTokenizer.Instance;
            var tokens = tokenizer.Tokenize(createTableString);
            var result = CreateParser.Create.Parse(tokens);
            return result;
            
        }
        static string createDatabaseString = "CREATE DATABASE test;";
        public static Create CreateDatabase()
        {
            var tokenizer = SqlTokenizer.Instance;
            var tokens = tokenizer.Tokenize(createDatabaseString);
            var result = CreateParser.Create.Parse(tokens);
            return result;
            if (result is CreateDatabase createDatabase)
            {
                Console.WriteLine($"Database: {createDatabase.Database}");
            }
        }

        static string createSchemaString = "CREATE SCHEMA test;";
        public static Create CreateSchema()
        {
            var tokenizer = SqlTokenizer.Instance;
            var tokens = tokenizer.Tokenize(createSchemaString);
            var result = CreateParser.Create.Parse(tokens);
           
            return result;
        }

    }
}
