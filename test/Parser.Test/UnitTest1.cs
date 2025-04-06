using System.Security.Cryptography.X509Certificates;

using Parser.Utils;

namespace Parser.Test
{
    public class UnitTest1
    {
        string str = "select a.i, a.item from tableName i";
        [Fact]
        public void CreateExpression()
        {
            var expression = TokenParser.CreateExpression(str);
        }
        [Fact]
        public void GetFirstRightToken()
        {
            var expression = TokenParser.CreateExpression(str);
            TokenParser.GetRighthToken(expression);
        }

    }
}
