namespace Parser.Models
{
    public class LeftToken
    {
        public string[]? Value { get; set; }
        public static LeftToken Create(string[] value)
        {
            return new LeftToken { Value = value };
        }
    }
}
