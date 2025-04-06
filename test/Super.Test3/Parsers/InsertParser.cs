using Super.Test3.Enums;
using Superpower.Parsers;
using Superpower;

namespace Super.Test3.Parsers
{
    public class InsertStatement
    {
        public string Table { get; set; }
        public string[] Columns { get; set; }
        public string[] Values { get; set; }
    }
    public static class InsertParser
    {
        public static TokenListParser<SqlToken, InsertStatement> Insert =
         from insert in Token.EqualTo(SqlToken.Insert)
         from intos in Token.EqualTo(SqlToken.Into)
         from table in SqlTokenizer.Identifier
         from lparen1 in Token.EqualTo(SqlToken.LParen)
         from columns in SqlTokenizer.ColumnList
         from rparent1 in Token.EqualTo(SqlToken.RParen)
         from values in Token.EqualTo(SqlToken.Values)
         from lparen2 in Token.EqualTo(SqlToken.LParen)
         from vals in SqlTokenizer.StringLiteral.AtLeastOnceDelimitedBy(Token.EqualTo(SqlToken.Comma))
         from rparent2 in Token.EqualTo(SqlToken.RParen)
         select new InsertStatement() { Columns = columns, Table = table, Values = vals };

    }
}
