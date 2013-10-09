


using System;

namespace HtmlConverters
{
    public partial class HtmlToMarkdownConverter
    {
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
                    if (!removeIfEmptyTag(Markdown.Tags[tag]))
                    {
                        block(true);
                    }
                    break;
                case "p":
                case "div":
                case "table":
                case "tbody":
                case "tr":
                case "td":
                    while (nodeStack.Count > 0 && nodeStack.Peek().Trim() == "")
                    {
                        nodeStack.Pop();
                    }
                    block(true);
                    break;
                case "b":
                case "strong":
                case "i":
                case "em":
                case "dfn":
                case "var":
                case "cite":
                    if (!removeIfEmptyTag(Markdown.Tags[tag]))
                    {
                        nodeStack.Push(sliceText(Markdown.Tags[tag]).Trim());
                        nodeStack.Push(Markdown.Tags[tag]);
                    }
                    break;
                case "a":
                    throw new NotImplementedException();
                    //        var text = sliceText("[");
                    //        text = text.replace(/\s+/g, " ");
                    //        text = trim(text);

                    //        if (text == "") {
                    //            nodeList.pop();
                    //            break;
                    //        }

                    //        var attrs = linkAttrStack.pop();
                    //        var url;
                    //        attrs["href"] &&  attrs["href"].value != "" ? url = attrs["href"].value : url = "";

                    //        if (url == "") {
                    //            nodeList.pop();
                    //            nodeList.push(text);
                    //            break;
                    //        }

                    //        nodeList.push(text);

                    //        if (!inlineStyle && !startsWith(peek(nodeList), "!")){
                    //            var l = links.indexOf(url);
                    //            if (l == -1) {
                    //                links.push(url);
                    //                l=links.length-1;
                    //            }
                    //            nodeList.push("][" + l + "]");
                    //        } else {
                    //            if(startsWith(peek(nodeList), "!")){
                    //                var text = nodeList.pop();
                    //                text = nodeList.pop() + text;
                    //                block();
                    //                nodeList.push(text);
                    //            }

                    //            var title = attrs["title"];
                    //            nodeList.push("](" + url + (title ? " \"" + trim(title.value).replace(/\s+/g, " ") + "\"" : "") + ")");

                    //            if(startsWith(peek(nodeList), "!")){
                    //                block(true);
                    //            }
                    //        }
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
                        var text = sliceText(li).Trim();

                        if (text.StartsWith("[!["))
                        {
                            nodeStack.Pop();
                            block(false);
                            nodeStack.Push(text);
                            block(true);
                        }
                        else
                        {
                            nodeStack.Push(text);
                            listBlock();
                        }
                    }
                    break;
                case "blockquote":
                    blockquoteStack.Pop();
                    break;
                case "pre":
                    throw new NotImplementedException();
                    //        //uncomment following experimental code to discard line numbers when syntax highlighters are used
                    //        //notes this code thorough testing before production user
                    //        /*
                    //        var p=[];
                    //        var flag = true;
                    //        var count = 0, whiteSpace = 0, line = 0;
                    //        console.log(">> " + peek(nodeList));
                    //        while(peek(nodeList).startsWith("    ") || flag == true)
                    //        {
                    //            //console.log('inside');
                    //            var text = nodeList.pop();
                    //            p.push(text);

                    //            if(flag == true && !text.startsWith("    ")) {
                    //                continue;
                    //            } else {
                    //                flag = false;
                    //            }

                    //            //var result = parseInt(text.trim());
                    //            if(!isNaN(text.trim())) {
                    //                count++;
                    //            } else if(text.trim() == ""){
                    //                whiteSpace++;
                    //            } else {
                    //                line++;
                    //            }
                    //            flag = false;
                    //        }

                    //        console.log(line);
                    //        if(line != 0)
                    //        {
                    //            while(p.length != 0) {
                    //                nodeList.push(p.pop());
                    //            }
                    //        }
                    //        */
                    //        block(true);
                    //        preStack.pop();
                    break;
                case "code":
                case "span":
                    throw new NotImplementedException();
                    //        if (preStack.length > 0) {
                    //            break;
                    //        } else if (trim(peek(nodeList)) == "") {
                    //            nodeList.pop();
                    //            nodeList.push(markdownTags[tag]);
                    //        } else {
                    //            var text = nodeList.pop();
                    //            nodeList.push(trim(text));
                    //            nodeList.push(markdownTags[tag]);
                    //        }
                    break;
                //case "table":
                //    nodeList.push("</table>");
                //    break;
                //case "thead":
                //    nodeList.push("</thead>");
                //    break;
                //case "tbody":
                //    nodeList.push("</tbody>");
                //    break;
                //case "tr":
                //    nodeList.push("</tr>");
                //    break;
                //case "td":
                //    nodeList.push("</td>");
                //    break;
                case "br":
                case "hr":
                case "img":
                    break;
            }

        }
    }
}
