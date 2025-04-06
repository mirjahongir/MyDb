using Parser.Enums;

namespace Parser.Models
{
    public class CommandExperssion
    {
        public CommandExperssion(string text)
        {
            Text = text;
            Cursor = 0;
        }
        public CommandExperssion(string text, CommandType cmdType, int cursor)
        {
            Text = text;
            Cursor = cursor;
            CommandType = cmdType;
        }
        public LeftToken? Left { get; set; }
        public RightToken? Right { get; set; }

        public string Text { get; }
        public int Cursor { get; set; }

        internal void SetCommandType(CommandType cmdType)
        {
            CommandType = cmdType;
        }
        public CommandType CommandType { get; private set; }
    }
}
