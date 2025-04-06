
using Super.Test3.Enums;
using Superpower.Parsers;
using Superpower;

namespace Super.Test3.Parsers
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
    public class Schema : Create
    {
        public override CreateType CreateType => CreateType.Schema;
        public string SchemaName { get; set; }
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
            select (Create)new Schema() { SchemaName = schemaName };

        public static TokenListParser<SqlToken, Create> Create =
            CreateTable.Try().Or(CreateDatabase).Try().Or(CreateShcema);

    }
}
