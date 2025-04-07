using Superpower.Display;

namespace Super.Test3.Enums
{
    public enum SqlToken
    {
        [Token(Example = "SELECT")] Select,
        [Token(Example = "FROM")] From,
        [Token(Example = "WHERE")] Where,
        [Token(Example = "INSERT")] Insert,
        [Token(Example = "INTO")] Into,
        [Token(Example = "VALUES")] Values,
        [Token(Example = "UPDATE")] Update,
        [Token(Example = "SET")] Set,
        [Token(Example = "CREATE")] Create,
        [Token(Example = "TABLE")] Table,
        [Token(Example = "DATABASE")] Database,
        [Token(Example = "DELETE")] Delete,
        [Token(Example = "schema")] Schema,
        [Token(Example = "*")] Star,
        [Token(Example = "=")] Equal,
        [Token(Example = ",")] Comma,
        [Token(Example = "(")] LParen,
        [Token(Example = ")")] RParen,
        [Token(Example = "int|text|float|date")] DataType,
        [Token(Example = "identifier")] Identifier,
        [Token(Example = "number")] Number,
        [Token(Example = "string")] String,
        [Token(Example = ";")] Semicolon,

        Whitespace // biz bu tokenni e’tiborsiz qoldiramiz
    }

}
