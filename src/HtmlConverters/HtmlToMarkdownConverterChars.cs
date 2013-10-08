
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
                    text = Regex.Replace(text, @"^\s+/g", "");
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
                throw new NotImplementedException();
                //nodeStack.Push(blockquoteStack.join(""));
            }

            nodeStack.Push(text);
        }
    }
}
