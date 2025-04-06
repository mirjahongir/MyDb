
namespace Parser.Utils
{
    //public static class TokenParser
    //{
    //    public static char GetFirstCharacter(ref string text, ref int cursor)
    //    {
    //        k:
    //        //BUG: bu yerda length tekshirish kerak
    //        if (text[cursor] == ' ')
    //        {
    //            cursor++;
    //            goto k;
    //        }
    //        return text[cursor];
    //    }
    //    public static string GetNextToken(ref string text, ref int cursor)
    //    {
    //        var character = GetFirstCharacter(ref text, ref cursor);
    //        if (character == '"')
    //        {

    //        }

    //    }
    //    public static KeyType GetNextKeyType(ref string text, ref int cursor)
    //    {
    //        var span = text.AsSpan(cursor);

    //        var index = 0;
    //        k:
    //        if (span[index] == ' ')
    //        {
    //            index++;
    //            goto k;
    //        }
    //        index += span.IndexOf(' ');


    //    }



    //    /// <summary>
    //    /// Generating CommandExpression 
    //    /// </summary>
    //    /// <param name="str"></param>
    //    /// <returns></returns>
    //    public static CommandExperssion CreateExpression(string str)
    //    {
    //        var (cmd, index) = CommandState.GetCommand(ref str);
    //        if (cmd == CommandType.NONE)
    //        {
    //            // Return Error
    //        }
    //        return new CommandExperssion(str, cmd, index + 1);
    //    }
    //    //BUG: Shuni tezda tug`irlashim kerak Hato yozilgan ya`niy Probellar urtasida 

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <returns></returns>
    //    public static RightToken GetRighthToken(CommandExperssion expression)
    //    {
    //        var span = expression.Text.AsSpan(expression.Cursor);
    //        if (span[0] == '"' || span[0] == '\'')
    //        {
    //            //Agar symbol tipi buladigan bulsa 
    //        }
    //        var spanIndex = span.IndexOf(' ');

    //        var value = new string(span[..spanIndex]);

    //        if (KeyTypeState.ChecKeyType(ref value, out KeyType keyType))
    //        {
    //            expression.Cursor += spanIndex;
    //            return RightToken.Create(value, keyType, expression);
    //        }

    //        spanIndex += 1;
    //        expression.Cursor += spanIndex;
    //        StringBuilder stringBuilder = new(value);

    //        while (true)
    //        {
    //            span = span.Slice(spanIndex);
    //            spanIndex = span.IndexOf(' ');
    //            value = new string(span[..spanIndex]);
    //            spanIndex += 1;
    //            expression.Cursor += spanIndex;
    //            if (KeyTypeState.ChecKeyType(ref value, out KeyType kt))
    //            {
    //                return RightToken.Create(stringBuilder.ToString(), kt, expression);
    //            }

    //            stringBuilder.Append(value);

    //        }


    //    }

    //}
}
