using Parser.Enums;

namespace Parser.Models
{
    public class RightToken
    {
        public KeyType KeyType { get; set; }
        public LeftToken? Left { get; set; }
        public RightToken? Right { get; set; }
        public int Cursor { get; set; }

        public static RightToken Create(string[] v, KeyType keyType, CommandExperssion expression)
        {
            RightToken result = new()
            {
                Left = LeftToken.Create(v),
                KeyType = keyType,
            };
            expression.Right = result;
            return result;
        }
    }
}
