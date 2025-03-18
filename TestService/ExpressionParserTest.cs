

using Models.ParserModel;

using Services.Parser;

namespace TestService
{
    public class TokenExpressionTest
    {
        public static string _command = "  create database йоха";
        [Fact]
        public void CreateExpression()
        {
            var token = TokenExpression.Parse(_command);
            Console.WriteLine(token);
            Console.ReadLine();
        }
    }
}
