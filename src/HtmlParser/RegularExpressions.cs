using System.Text.RegularExpressions;

namespace HtmlParser
{
    public class RegularExpressions
    {
        public static Regex IsStartTag = new Regex("^<(\\w+)((?:\\s+\\w+(?:\\s*=\\s*(?:(?:\"[^\"]*\")|(?:'[^']*')|[^>\\s]+))?)*)\\s*(\\/?)>");
        public static Regex IsEndTag = new Regex("^<\\/(\\w+)[^>]*>");

        public static Regex IsAttribute = new Regex("(\\w+)(?:\\s*=\\s*(?:(?:\"((?:\\.|[^\"])*)\")|(?:'((?:\\.|[^'])*)')|([^>\\s]+)))?", RegexOptions.Multiline);
    }
}