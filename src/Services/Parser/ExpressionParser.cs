
using Models.ParserModel;

namespace Services.Parser
{

    public static class TokenExpression
    {
        public static Token Parse(string text)
        {
            var span = text.AsSpan();
            int start = 0;
            var token = TokenParser.ParseToken(ref span, ref start);
            switch (token.CmdType)
            {
                case DbEnums.Enums.Parser.CmdType.CREATE:
                    CreateExpression.Create(ref token, ref span, ref start);
                    break;
                //case DbEnums.Enums.Parser.CmdType.SELECT:
                //    Select(token, ref span, ref start);
                //    break;
                //case DbEnums.Enums.Parser.CmdType.UPDATE:
                //    Update(token, ref span, ref start);
                //    break;
            }
            return token;
        }

        //public static void CreateDatabase(Token token, ref ReadOnlySpan<char> span, ref int start)
        //{
        //}

        //public static void Select(Token token, ref ReadOnlySpan<char> span, ref int start)
        //{

        //}
        //public static void Update(Token token, ref ReadOnlySpan<char> span, ref int start)
        //{
        //}
        //public static void Insert(Token token, ref ReadOnlySpan<char> span, ref int start)
        //{

        //}

    }
}
