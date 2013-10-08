/*
* Ported from HTML Parser By John Resig (ejohn.org)
* Original code by Erik Arvidsson, Mozilla Public License
* http://erik.eae.net/simplehtmlparser/simplehtmlparser.js
*/

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HtmlParser
{
    public abstract class BaseParser
    {
        int _index;
        readonly HtmlStack _stack = new HtmlStack();
        private string last;
        private readonly List<string> _htmlBlocks = new List<string>();

        protected void Parse(string html)
        {
            last = html;

            while (false == string.IsNullOrWhiteSpace(html))
            {
                // Make sure we're not in a script or style element
                if (_stack.Length == 0 || (!string.IsNullOrWhiteSpace(_stack.Last()) && false == ExcludedTags.Contains(_stack.Last())))
                {
                    // Comment
                    if (html.IndexOf("<!--") == 0)
                    {
                        _index = html.IndexOf("-->");

                        if (_index >= 0)
                        {
                            var content = html.Substring(4, _index);
                            comment(content);
                            html = html.Substring(_index + 3);
                            _htmlBlocks.Add(content);
                        }

                        // end tag
                    }
                    else if (html.IndexOf("</") == 0)
                    {
                        var regexMatch = RegularExpressions.IsEndTag.Match(html);

                        if (regexMatch.Success)
                        {
                            var content = regexMatch.Groups[1].Value;

                            html = html.Substring(regexMatch.Groups[0].Length);
                            parseEndTag(content);
                            //match[0].replace( IsEndTag, parseEndTag );
                            _htmlBlocks.Add(content);
                        }

                        // start tag
                    }
                    else if (html.IndexOf("<") == 0)
                    {
                        var regexMatch = RegularExpressions.IsStartTag.Match(html);

                        if (regexMatch.Success)
                        {
                            var content = regexMatch.Groups[1].Value;

                            html = html.Substring(regexMatch.Groups[0].Length);
                            parseStartTag(content, regexMatch.Groups[2].Value, regexMatch.Groups[3].Value);
                            //match[0].replace( IsStartTag, parseStartTag );

                            _htmlBlocks.Add(content);
                        }
                    }
                    else if (false == ExcludedTags.Contains(_stack.Last()))
                    {
                        _index = html.IndexOf("<");

                        var text = _index < 0 ? html : html.Substring(0, _index);
                        html = _index < 0 ? "" : html.Substring(_index);

                        chars(text);
                        _htmlBlocks.Add(text);
                    }

                }
                else
                {
                    var regexMatch = new Regex("(.*)<\\/" + _stack.Last() + "[^>]*>").Match(html);
                    var text = regexMatch.Groups[1].Value;

                    text = Regex.Replace(text, @"<!--(.*?)-->/g", "$1");
                    text = Regex.Replace(text, @"<!\[CDATA\[(.*?)]]>/g", "$1");

                    if (false == ExcludedTags.Contains(_stack.Last()))
                    {
                        chars(text);
                    }

                    html = "";

                    parseEndTag(_stack.Last());
                }

                if (html == last)
                {
                    throw new Exception(html);
                }

                last = html;
            }

            // Clean up any remaining tags
            completedParsing();
        }

        private void completedParsing()
        {
            _stack.Length = 0;
            completed(_htmlBlocks);
        }

        private void parseStartTag(string tagName, string rest, string unaryStr)
        {
            if (HtmlTags.Block.Contains(tagName))
            {
                while (false == string.IsNullOrWhiteSpace(_stack.Last()) && HtmlTags.Inline.Contains(_stack.Last()))
                {
                    parseEndTag(_stack.Last());
                }
            }

            if (HtmlTags.SelfClosing.Contains(tagName) && _stack.Last() == tagName)
            {
                parseEndTag(tagName);
            }

            bool parsedUnary;
            bool.TryParse(unaryStr, out parsedUnary);
            var unary = HtmlTags.Empty.Contains(tagName) || parsedUnary;

            _stack.push(tagName);

            var attrs = new Dictionary<string, HtmlAttribute>();

            var regexMatch = RegularExpressions.IsAttribute.Matches(rest);

            for (int i = 0; i < regexMatch.Count; i++)
            {
                Match match = regexMatch[i];
                var value =
                  match.Groups[2].Value != string.Empty ? match.Groups[2].Value :
                            match.Groups[3].Value != string.Empty ? match.Groups[3].Value :
                            match.Groups[4].Value != string.Empty ? match.Groups[4].Value :
                            HtmlTags.FillAttrs.Contains(match.Groups[1].Value) ? match.Groups[1].Value : string.Empty;

                attrs.Add(match.Groups[1].Value, new HtmlAttribute
                {
                    Name = match.Groups[1].Value.ToLower(),
                    Value = value
                });
            }

            start(tagName, attrs, unary);
        }

        private void parseEndTag(string tagName)
        {
            int pos;
            // Find the closest opened tag of the same type
            for (pos = _stack.Length - 1; pos >= 0; pos--)
            {
                if (_stack.At(pos) == tagName)
                {
                    break;
                }
            }

            if (pos >= 0)
            {
                // Close all the open elements, up the htmlStack
                for (var i = _stack.Length - 1; i >= pos; i--)
                {
                    end(_stack.At(i));
                }

                // Remove the open elements from the htmlStack
                _stack.Length = pos;
            }
        }

        protected abstract void comment(string text);

        protected abstract void chars(string text);

        protected abstract void completed(List<string> htmlStack);

        protected abstract void start(string tag, Dictionary<string, HtmlAttribute> attributes, bool unary);

        protected abstract void end(string tag);

        // Special Elements (can contain anything)
        protected abstract List<string> ExcludedTags { get; }
    }
}