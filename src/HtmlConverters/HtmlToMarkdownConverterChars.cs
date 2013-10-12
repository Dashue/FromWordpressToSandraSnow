
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace HtmlConverters
{
    public partial class HtmlToMarkdownConverter
    {
        protected override void chars(string text)
        {
            if (preStack.Count > 0)
            {
                throw new NotImplementedException();
                //text = text.replace(/\n/g,"\n    ");
            }
            else if (text.Trim() != "")
            {
                text = Regex.Replace(text, @"\s+/g", " ");

                var prevText = HtmlToMarkdownConverterHelper.peekTillNotEmpty(nodeStack.ToList());

                if (Regex.IsMatch(prevText, @"\s+$"))
                {
                    text = text.TrimStart();
                }
            }
            else
            {
                nodeStack.Push("");
                return;
            }

            //if(blockquoteStack.length > 0 && peekTillNotEmpty(nodeList).endsWith("\n")) {
            if (blockquoteStack.Count > 0)
            {
                var array = blockquoteStack.ToArray();
                Array.Reverse(array);

                nodeStack.Push(string.Join(string.Empty, array));
            }

            nodeStack.Push(text);
        }
    }
}
