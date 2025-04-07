using System.Text.RegularExpressions;

using Super.Test3.Enums;

using Superpower;
using Superpower.Parsers;
using Superpower.Tokenizers;

namespace Super.Test3
{
    public static class SqlTokenizer
    {
        public static readonly Tokenizer<SqlToken> Instance = new TokenizerBuilder<SqlToken>()
            .Ignore(Span.WhiteSpace)
            .Match(Span.EqualToIgnoreCase("select"), SqlToken.Select)
            .Match(Span.EqualToIgnoreCase("from"), SqlToken.From)
            .Match(Span.EqualToIgnoreCase("where"), SqlToken.Where)
            .Match(Span.EqualToIgnoreCase("insert"), SqlToken.Insert)
            .Match(Span.EqualToIgnoreCase("into"), SqlToken.Into)
            .Match(Span.EqualToIgnoreCase("values"), SqlToken.Values)
            .Match(Span.EqualToIgnoreCase("update"), SqlToken.Update)
            .Match(Span.EqualToIgnoreCase("set"), SqlToken.Set)
            .Match(Span.EqualToIgnoreCase("create"), SqlToken.Create)
            .Match(Span.EqualToIgnoreCase("table"), SqlToken.Table)
            .Match(Span.EqualToIgnoreCase("database"), SqlToken.Database)
            .Match(Span.EqualToIgnoreCase("delete"), SqlToken.Delete)
            .Match(Span.EqualToIgnoreCase("schema"), SqlToken.Schema)
            .Match(Span.Regex(@"(varchar|char)\s*\(\s*\d+\s*\)", RegexOptions.IgnoreCase), SqlToken.DataType)
            .Match(Span.Regex(@"int|text|float|date", RegexOptions.IgnoreCase), SqlToken.DataType)
            .Match(Character.EqualTo('*'), SqlToken.Star)
            .Match(Character.EqualTo('='), SqlToken.Equal)
            .Match(Character.EqualTo(','), SqlToken.Comma)
            .Match(Character.EqualTo('('), SqlToken.LParen)
            .Match(Character.EqualTo(')'), SqlToken.RParen)
            .Match(Character.EqualTo(';'), SqlToken.Semicolon)
            .Match(Span.Regex(@"'[^']*'"), SqlToken.String)
            .Match(QuotedString.CStyle, SqlToken.String)
            .Match(Numerics.Integer, SqlToken.Number)
            .Match(Superpower.Parsers.Identifier.CStyle, SqlToken.Identifier)
            .Build();

        public static TokenListParser<SqlToken, string> Identifier =
             Token.EqualTo(SqlToken.Identifier).Select(t => t.ToStringValue());
        public static TokenListParser<SqlToken, string> StringLiteral =
             Token.EqualTo(SqlToken.String).Select(t => t.ToStringValue());
        public static TokenListParser<SqlToken, string> NumberLiteral =
             Token.EqualTo(SqlToken.Number).Select(t => t.ToStringValue());
        public static TokenListParser<SqlToken, string[]> ColumnList =
             Identifier.AtLeastOnceDelimitedBy(Token.EqualTo(SqlToken.Comma));

    }
}
