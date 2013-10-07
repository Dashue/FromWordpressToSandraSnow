/**
* Ported from: html2markdown by
* @author Himanshu Gilani
* @author Kates Gasis (original author)
*
*/

using HtmlParser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HtmlConverters
{
    //Todo: UL list style *, + or -
    public class HtmlToMarkdownConverter : BaseParser, IHtmlConverter
    {
        Stack<string> listTagStack = new Stack<string>();
        private Stack<Dictionary<string, HtmlAttribute>> linkAttrStack = new Stack<Dictionary<string, HtmlAttribute>>();
        Stack<string> blockquoteStack = new Stack<string>();
        Stack<bool> preStack = new Stack<bool>();
        Stack<string> codeStack = new Stack<string>();
        Stack<string> nodeStack = new Stack<string>();
        List<string> links = new List<string>();

        private Dictionary<string, string> markdownTags = new Dictionary<string, string>
            {
                {"hr", "- - -\n\n"},
                {"br", "  \n"},
                {"title", "# "},
                {"h1", "# "},
                {"h2", "## "},
                {"h3", "### "},
                {"h4", "#### "},
                {"h5", "##### "},
                {"h6", "###### "},
                {"b", "**"},
                {"strong", "**"},
                {"i", "_"},
                {"em", "_"},
                {"dfn", "_"},
                {"var", "_"},
                {"cite", "_"},
                {"span", " "},
                {"ul", "* "},
                {"ol", "1. "},
                {"dl", "- "},
                {"blockquote", "> "}
            };

        private readonly List<string> _excludedTags = new List<string>
            {
                "script", "noscript", "object", "iframe", "frame", "head", "style", "label"
            };

        private bool _inlineStyle;


        public HtmlToMarkdownConverter(bool inlineStyle = false)
        {
            _inlineStyle = inlineStyle;
        }

        public string getListMarkdownTag()
        {
            var listItem = "";
            for (var i = 0; i < listTagStack.Count - 1; i++)
            {
                listItem += "  ";
            }

            listItem += listTagStack.Peek();
            return listItem;
        }

        public string peekTillNotEmpty(Stack<string> list)
        {
            if (list != null)
            {
                var array = list.ToArray();
                foreach (string t in array)
                {
                    if (t != "")
                    {
                        return t;
                    }
                }
            }

            return "";
        }

        public bool removeIfEmptyTag(string start)
        {
            var cleaned = false;
            if (start == peekTillNotEmpty(nodeStack))
            {
                while (nodeStack.Peek() != start)
                {
                    nodeStack.Pop();
                }
                nodeStack.Pop();
                cleaned = true;
            }
            return cleaned;
        }

        public string sliceText(string start)
        {
            var text = new LinkedList<string>();

            while (nodeStack.Count > 0 && nodeStack.Peek() != start)
            {
                var t = nodeStack.Pop();
                text.AddFirst(t);
            }
            return string.Join(string.Empty, text);
        }

        public void EndBlock(bool isEndBlock = false)
        {
            if (nodeStack.Count == 0)
            {
                return;
            }

            var lastItem = nodeStack.Peek();

            if (!isEndBlock)
            {
                string block;

                if (Regex.IsMatch(lastItem, @"\s*\n\n\s*$"))
                {
                    lastItem = Regex.Replace(lastItem, @"\s*\n\n\s*$", "\n\n");
                    block = "";
                }
                else if (Regex.IsMatch(lastItem, @"\s*\n\s*$"))
                {
                    lastItem = Regex.Replace(lastItem, @"\s*\n\s*$", "\n");
                    block = "\n";
                }
                else if (Regex.IsMatch(lastItem, @"\s+$"))
                {
                    block = "\n\n";
                }
                else
                {
                    block = "\n\n";
                }

                nodeStack.Push(block);
            }
            else
            {
                switch (lastItem)
                {
                    default:
                        nodeStack.Push("\n\n");
                        break;
                }
            }
        }

        public void listBlock()
        {
            if (nodeStack.Count > 0)
            {
                var li = nodeStack.Peek();

                if (false == li.EndsWith("\n"))
                {
                    nodeStack.Push("\n");
                }
            }
            else
            {
                nodeStack.Push("\n");
            }
        }

        public string Convert(string html)
        {
            Parse(html);

            if (!_inlineStyle)
            {
                for (var i = 0; i < links.Count; i++)
                {
                    if (i == 0)
                    {
                        var lastItem = nodeStack.Pop();
                        nodeStack.Push(Regex.Replace(lastItem, @"\s+$/g", string.Empty));

                        if (false == lastItem.EndsWith("\n\n"))
                        {
                            nodeStack.Push("\n\n");
                        }

                        nodeStack.Push("[" + i + "]: " + links[i]);
                    }
                    else
                    {
                        nodeStack.Push("\n[" + i + "]: " + links[i]);
                    }
                }
            }

            var nodes = nodeStack.ToArray();
            Array.Reverse(nodes);

            var builder = new StringBuilder();
            foreach (var node in nodes)
            {
                builder.Append(node);
            }

            return builder.ToString();
        }

        protected override void comment(string text)
        {
            throw new NotImplementedException();
        }

        protected override void chars(string text)
        {
            if (preStack.Count > 0)
            {
                text = Regex.Replace(text, "\n/g", "\n    ");
            }
            else if (string.IsNullOrWhiteSpace(text))
            {
                nodeStack.Push("");
                return;
            }
            else
            {
                var prevText = peekTillNotEmpty(nodeStack);
                if (Regex.IsMatch(prevText, @"\s+$"))
                {
                    text = Regex.Replace(text, @"^\s+/g", " ");
                }
                else if (
                    false == string.IsNullOrWhiteSpace(prevText) &&
                    false == prevText.EndsWith(">") &&
                    false == prevText.EndsWith("]") &&
                    false == prevText.EndsWith(" "))
                {
                    if (false == text.StartsWith(" "))
                    {
                        text = string.Format(" {0}", text);
                    }
                }
            }

            if (blockquoteStack.Count > 0)
            {
                if (blockquoteStack.Count > 0)
                {
                    nodeStack.Push(string.Join("", blockquoteStack));
                }
            }

            nodeStack.Push(text);
        }

        protected override void completed(List<string> htmlStack)
        {
            if (nodeStack.Count == 1 && htmlStack.Count == 1)
            {
                if (false == HtmlTags.Empty.Contains(htmlStack[0]))
                {
                    removeIfEmptyTag(nodeStack.Peek());
                }
                else if (markdownTags.ContainsKey(nodeStack.Peek()))
                {
                    removeIfEmptyTag(nodeStack.Peek());
                }
            }
        }

        protected override void start(string tag, Dictionary<string, HtmlAttribute> attributes, bool unary)
        {
            tag = tag.ToLower();

            if (unary && (tag != "br" && tag != "hr" && tag != "img"))
            {
                return;
            }

            switch (tag)
            {
                case "p":
                case "div":
                case "table":
                case "tbody":
                case "tr":
                case "td":
                    EndBlock();
                    break;
            }

            switch (tag)
            {
                case "br":
                    nodeStack.Push(markdownTags[tag]);
                    break;
                case "hr":
                    EndBlock();
                    nodeStack.Push(markdownTags[tag]);
                    break;
                case "title":
                case "h1":
                case "h2":
                case "h3":
                case "h4":
                case "h5":
                case "h6":
                    EndBlock();
                    nodeStack.Push(markdownTags[tag]);
                    break;
                case "b":
                case "strong":
                case "i":
                case "em":
                case "dfn":
                case "var":
                case "cite":
                    nodeStack.Push(markdownTags[tag]);
                    break;
                case "code":
                case "span":
                    if (preStack.Count > 0)
                    {
                        break;
                    }
                    break;
                case "ul":
                case "ol":
                case "dl":
                    listTagStack.Push(markdownTags[tag]);
                    // lists are block elements
                    if (listTagStack.Count > 1)
                    {
                        listBlock();
                    }
                    else
                    {
                        EndBlock();
                    }
                    break;
                case "li":
                case "dt":
                    var li = getListMarkdownTag();
                    nodeStack.Push(li);
                    break;
                case "a":
                    //var attribs = convertAttrs(attributes);
                    linkAttrStack.Push(attributes);
                    nodeStack.Push("[");
                    break;
                case "img":
                    //var attribs = convertAttrs(attributes);
                    string alt = string.Empty;
                    string title = string.Empty;
                    string url = null;

                    if (attributes.ContainsKey("src"))
                    {
                        url = attributes["src"].Value;
                    }

                    if (string.IsNullOrWhiteSpace(url))
                    {
                        break;
                    }

                    if (attributes.ContainsKey("alt"))
                    {
                        alt = attributes["alt"].Value;

                    }

                    if (attributes.ContainsKey("title") != null)
                    {
                        title = attributes["title"].Value;

                    }

                    // if parent of image tag is nested in anchor tag use inline style
                    if (!_inlineStyle && false == peekTillNotEmpty(nodeStack).StartsWith("["))
                    {
                        var l = links.IndexOf(url);
                        if (l == -1)
                        {
                            links.Add(url);
                            l = links.Count - 1;
                        }

                        EndBlock();
                        nodeStack.Push("![");
                        if (alt != "")
                        {
                            nodeStack.Push(alt);
                        }
                        else if (title != null)
                        {
                            nodeStack.Push(title);
                        }

                        nodeStack.Push("][" + l + "]");
                        EndBlock();
                    }
                    else
                    {
                        //if image is not a link image then treat images as block elements
                        if (false == peekTillNotEmpty(nodeStack).StartsWith("["))
                        {
                            EndBlock();
                        }
                        string fixedTitle;

                        if (string.IsNullOrWhiteSpace(title))
                        {
                            fixedTitle = string.Empty;
                        }
                        else
                        {
                            fixedTitle = " \"" + title + "\"";
                        }

                        nodeStack.Push("![" + alt + "](" + url + fixedTitle + ")");

                        if (false == peekTillNotEmpty(nodeStack).StartsWith("["))
                        {
                            EndBlock(true);
                        }
                    }
                    break;
                case "blockquote":
                    //listBlock();
                    EndBlock();
                    blockquoteStack.Push(markdownTags[tag]);
                    break;
                case "pre":
                    EndBlock();
                    preStack.Push(true);
                    nodeStack.Push("    ");
                    break;
                case "table":
                    nodeStack.Push("<table>");
                    break;
                case "thead":
                    nodeStack.Push("<thead>");
                    break;
                case "tbody":
                    nodeStack.Push("<tbody>");
                    break;
                case "tr":
                    nodeStack.Push("<tr>");
                    break;
                case "td":
                    nodeStack.Push("<td>");
                    break;
            }
        }

        protected override void end(string tag)
        {
            tag = tag.ToLower();

            switch (tag)
            {
                case "title":
                case "h1":
                case "h2":
                case "h3":
                case "h4":
                case "h5":
                case "h6":
                    if (!removeIfEmptyTag(markdownTags[tag]))
                    {
                        EndBlock(true);
                    }
                    break;
                case "p":
                case "div":
                case "table":
                case "tbody":
                case "tr":
                case "td":
                    while (nodeStack.Count > 0 && string.IsNullOrWhiteSpace(nodeStack.Peek()))
                    {
                        nodeStack.Pop();
                    }
                    EndBlock(true);
                    break;
                case "b":
                case "strong":
                case "i":
                case "em":
                case "dfn":
                case "var":
                case "cite":
                    if (!removeIfEmptyTag(markdownTags[tag]))
                    {
                        nodeStack.Push(sliceText(markdownTags[tag]).Trim());
                        nodeStack.Push(markdownTags[tag]);
                    }
                    break;
                case "a":
                    var a = sliceText("[");
                    a = Regex.Replace(a, @"\s+/g", " ");
                    a = a.Trim();

                    if (string.IsNullOrWhiteSpace(a))
                    {
                        nodeStack.Pop();
                        break;
                    }

                    var attributes = linkAttrStack.Pop();

                    string url = null;

                    if (attributes.ContainsKey("href"))
                    {
                        url = attributes["href"].Value;
                    }

                    if (string.IsNullOrWhiteSpace(url))
                    {
                        nodeStack.Pop();
                        nodeStack.Push(a);
                        break;
                    }

                    nodeStack.Push(a);

                    if (!_inlineStyle && false == nodeStack.Peek().StartsWith("!"))
                    {
                        var l = links.IndexOf(url);
                        if (l == -1)
                        {
                            links.Add(url);
                            l = links.Count - 1;
                        }
                        nodeStack.Push("][" + l + "]");
                    }
                    else
                    {
                        if (nodeStack.Peek().StartsWith("!"))
                        {
                            var text = nodeStack.Pop();
                            text = nodeStack.Pop() + text;
                            EndBlock();
                            nodeStack.Push(text);
                        }

                        string title = null;

                        if (attributes.ContainsKey("title"))
                        {
                            title = attributes["title"].Value;
                        }

                        string fixedTitle;
                        if (false == string.IsNullOrWhiteSpace(title))
                        {
                            fixedTitle = " \"" + Regex.Replace(title.Trim(), @"\)s+/g", " ") + "\"";
                        }
                        else
                        {
                            fixedTitle = string.Empty;
                        }

                        nodeStack.Push("](" + url + fixedTitle + ")");

                        if (nodeStack.Peek().StartsWith("!"))
                        {
                            EndBlock(true);
                        }
                    }
                    break;
                case "ul":
                case "ol":
                case "dl":
                    listBlock();
                    listTagStack.Pop();
                    break;
                case "li":
                case "dt":
                    var li = getListMarkdownTag();
                    if (!removeIfEmptyTag(li))
                    {
                        var dt = sliceText(li).Trim();

                        if (dt.StartsWith("[!["))
                        {
                            nodeStack.Pop();
                            EndBlock();
                            nodeStack.Push(dt);
                            EndBlock(true);
                        }
                        else
                        {
                            nodeStack.Push(dt);
                            listBlock();
                        }
                    }
                    break;
                case "blockquote":
                    blockquoteStack.Pop();
                    break;
                case "pre":
                    //uncomment following experimental code to discard line numbers when syntax highlighters are used
                    //notes this code thorough testing before production user
                    /*
                    var p=[];
                    var flag = true;
                    var count = 0, whiteSpace = 0, line = 0;
                    console.log(">> " + peek(nodeStack));
                    while(peek(nodeStack).startsWith("    ") || flag == true)
                    {
                        //console.log('inside');
                        var text = nodeStack.Pop();
                        p.Push(text);

                        if(flag == true && !text.startsWith("    ")) {
                            continue;
                        } else {
                            flag = false;
                        }

                        //var result = parseInt(text.trim());
                        if(!isNaN(text.trim())) {
                            count++;
                        } else if(text.trim() == ""){
                            whiteSpace++;
                        } else {
                            line++;
                        }
                        flag = false;
                    }

                    console.log(line);
                    if(line != 0)
                    {
                        while(p.length != 0) {
                            nodeStack.Push(p.Pop());
                        }
                    }
                    */
                    EndBlock(true);
                    preStack.Pop();
                    break;
                case "code":
                case "span":
                    if (preStack.Count > 0)
                    {
                        break;
                    }
                    else if (string.IsNullOrWhiteSpace(nodeStack.Peek()))
                    {
                        nodeStack.Pop();
                    }
                    break;
                case "br":
                case "hr":
                case "img":
                    break;
            }

            switch (tag)
            {
                case "table":
                    nodeStack.Push("</table>");
                    break;
                case "thead":
                    nodeStack.Push("</thead>");
                    break;
                case "tbody":
                    nodeStack.Push("</tbody>");
                    break;
                case "tr":
                    nodeStack.Push("</tr>");
                    break;
                case "td":
                    nodeStack.Push("</td>");
                    break;
            }
        }

        protected override List<string> ExcludedTags
        {
            get { return _excludedTags; }
        }
    }
}
