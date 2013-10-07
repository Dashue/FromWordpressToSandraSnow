using HtmlParser;
using System.Collections.Generic;
using System.Text;

namespace HtmlConverters
{
    public class HtmlToXmlConverter : BaseParser, IHtmlConverter
    {
        private StringBuilder Results = new StringBuilder();

        private readonly List<string> _excludedTags = new List<string>
            {
                "script",
                "style"
            };

        protected override void comment(string text)
        {
            Results.AppendFormat("<!--{0}-->", text);
        }

        protected override void chars(string text)
        {
            Results.Append(text);
        }

        protected override void completed(List<string> htmlStack)
        {
            throw new System.NotImplementedException();
        }

        protected override void start(string tag, Dictionary<string, HtmlAttribute> attributes, bool unary)
        {
            Results.AppendFormat("<{0}", tag);

            foreach (HtmlAttribute attribute in attributes.Values)
            {
                Results.AppendFormat(" {0}=\"{1}\"", attribute.Name, attribute.Escaped);
            }

            Results.Append(unary ? "/>" : ">");
        }

        protected override void end(string tag)
        {
            Results.AppendFormat("</{0}>", tag);

        }

        protected override List<string> ExcludedTags
        {
            get { return _excludedTags; }
        }

        public string Convert(string html)
        {
            base.Parse(html);

            return Results.ToString();
        }
    }
}