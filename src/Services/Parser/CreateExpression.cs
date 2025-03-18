
using Models.ParserModel;

namespace Services.Parser
{
    internal static class CreateExpression
    {
        public static void Create(ref Token token, ref ReadOnlySpan<char> span, ref int start)
        {
            var rightToken = TokenParser.ParseToken(ref span, ref start);

            switch (rightToken.KeyType)
            {
                case DbEnums.Enums.Parser.KeyType.Database:
                    token.Right = rightToken;
                    CreateDatabase(rightToken, ref span, ref start);
                    break;
                case DbEnums.Enums.Parser.KeyType.Table:
                    token.Right = rightToken;
                    CreateTable(rightToken, ref span, ref start);
                    break;
                default:

                    break;
            }
            
        }
        static void CreateDatabase(Token token, ref ReadOnlySpan<char> span, ref int start)
        {
            var databaseName = TokenParser.ParseToken(ref span, ref start);
            token.Right = databaseName;
        }
        static void CreateTable(Token token, ref ReadOnlySpan<char> span, ref int start)
        {

        }
    }
}
