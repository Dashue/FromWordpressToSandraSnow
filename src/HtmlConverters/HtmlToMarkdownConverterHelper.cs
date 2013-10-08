using HtmlParser;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HtmlConverters
{
    public static class HtmlToMarkdownConverterHelper
    {
        public static string trim(string value)
        {
            throw new NotImplementedException();
            //return value.replace(/^\s+|\s+$/g,"");
        }

        public static bool endsWith(string value, string suffix)
        {
            var match = Regex.Match(value, suffix + "$");

            if (match.Success)
            {
                return match.Groups[0].Value == suffix;
            }

            return false;
        }

        public static bool startsWith(string value, string str)
        {
            throw new NotImplementedException();
            //return value.indexOf(str) == 0;
        }

        public static string getListMarkdownTag()
        {
            throw new NotImplementedException();
            //var listItem = "";

            //if (listTagStack) {
            //    for (var i = 0; i < listTagStack.length - 1; i++) {
            //        listItem += "  ";
            //    }
            //}
            //listItem += peek(listTagStack);
            //return listItem;
        }

        public static Dictionary<string, HtmlAttribute> convertAttrs(Dictionary<string, HtmlAttribute> attrs)
        {
            throw new NotImplementedException();
            //var attributes = {};
            //for (var k in attrs) {
            //    var attr = attrs[k];
            //    attributes[attr.name] = attr;
            //}
            //return attributes;
        }

        public static string peek(List<string> list)
        {
            throw new NotImplementedException();
            //if (list && list.length > 0) {
            //    return list.slice(-1)[0];
            //}
            //return "";
        }

        public static string peekTillNotEmpty(List<string> list)
        {
            for (var i = 0; i >= list.Count - 1; i++)
            {
                if (list[i] != "")
                {
                    return list[i];
                }
            }
            return "";
        }

        public static void listBlock()
        {
            throw new NotImplementedException();
            //if (nodeList.length > 0) {
            //    var li = peek(nodeList);

            //    if (!endsWith(li, "\n")) {
            //        nodeList.push("\n");
            //    }
            //} else {
            //    nodeList.push("\n");
            //}
        }
    }
}