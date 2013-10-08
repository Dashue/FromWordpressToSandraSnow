

using Xunit;

namespace HtmlConverters.Tests
{
    public class HtmlToMarkdownTests
    {
        private HtmlToMarkdownConverter _converter;

        public HtmlToMarkdownTests()
        {
            _converter = new HtmlToMarkdownConverter();
        }

        [Fact]
        public void should_be_able_to_convert_h1()
        {
            Assert.Equal("# H1\n\n", _converter.Convert("<h1>H1</h1>"));
        }

        [Fact]
        public void should_be_able_to_convert_h2()
        {
            Assert.Equal("## H2\n\n", _converter.Convert("<h2>H2</h2>"));
        }

        [Fact]
        public void should_be_able_to_convert_h3()
        {
            Assert.Equal("### H3\n\n", _converter.Convert("<h3>H3</h3>"));
        }

        [Fact]
        public void should_be_able_to_convert_h4()
        {
            Assert.Equal("#### H4\n\n", _converter.Convert("<h4>H4</h4>"));
        }

        [Fact]
        public void should_be_able_to_convert_h5()
        {
            Assert.Equal("##### H5\n\n", _converter.Convert("<h5>H5</h5>"));
        }

        [Fact]
        public void should_be_able_to_convert_h6()
        {
            var html = "<h6>H6</h6>";
            var expected = "###### H6\n\n";

            Assert.Equal(expected, _converter.Convert(html));
        }

        [Fact]
        public void Should_convert_strong()
        {
            var html = "<strong>Bold</strong>";
            var expected = "**Bold**";

            Assert.Equal(expected, _converter.Convert(html));
        }

        [Fact]
        public void Should_convert_b()
        {
            var html = "<b>Bold</b>";
            var expected = "**Bold**";

            Assert.Equal(expected, _converter.Convert(html));
        }

        [Fact]
        public void Should_convert_em()
        {
            var html = "<em>Italic</em>";
            var expected = "_Italic_";

            Assert.Equal(expected, _converter.Convert(html));
        }

        [Fact]
        public void Should_convert_i()
        {
            var html = "<i>Italic</i>";
            var expected = "_Italic_";

            Assert.Equal(expected, _converter.Convert(html));
        }

        [Fact]
        public void Should_convert_title()
        {
            var html = "<title>This is document Title</title>";
            var expected = "# This is document Title\n\n";

            Assert.Equal(expected, _converter.Convert(html));
        }

        [Fact]
        public void Should_trim_text_inside_elements()
        {
            var html = "<strong> String </strong>";
            var expected = "**String**";

            Assert.Equal(expected, _converter.Convert(html));
        }

        [Fact]
        public void Should_convert_strong_inside_text()
        {
            var html = "This has a <strong>block</strong> word";
            var expected = "This has a **block** word";

            Assert.Equal(expected, _converter.Convert(html));
        }

        [Fact]
        public void Should_convert_hr()
        {
            var html = "<hr />";
            var expected = "- - -\n\n";

            Assert.Equal(expected, _converter.Convert(html));
        }

        [Fact]
        public void Should_convert_br()
        {
            var html = "<br/>";
            var expected = "  \n";

            Assert.Equal(expected, _converter.Convert(html));
        }

        [Fact]
        public void Should_convert_strong_and_em_inside_text()
        {
            var html = "This has <strong>blocked and <em>italicized</em></strong> texts.";
            var expected = "This has **blocked and _italicized_** texts.";

            Assert.Equal(expected, _converter.Convert(html));
        }

        [Fact]
        public void Should_convert_hr_inside_text()
        {
            var html = "this is text before hr<hr/>this is text after hr";
            var expected = "this is text before hr\n\n- - -\n\nthis is text after hr";

            Assert.Equal(expected, _converter.Convert(html));
        }

        [Fact]
        public void Should_convert_br_inside_text()
        {
            var html = "this is text before break<br/>this is text after break";
            var expected = "this is text before break  \nthis is text after break";

            Assert.Equal(expected, _converter.Convert(html));
        }

        [Fact]
        public void Should_convert_p()
        {
            var html = "<p>This is a paragraph. This is the second sentence.</p>";
            var expected = "This is a paragraph. This is the second sentence.\n\n";

            Assert.Equal(expected, _converter.Convert(html));
        }

        [Fact]
        public void Should_convert_p_inside_text()
        {
            var html = "this is text before paragraph<p>This is a paragraph</p>this is text after paragraph";
            var expected = "this is text before paragraph\n\nThis is a paragraph\n\nthis is text after paragraph";

            Assert.Equal(expected, _converter.Convert(html));
        }

        [Fact]
        public void Should_convert_blockquote()
        {
            var html = "<blockquote>This is blockquoted</blockquote>";
            var expected = "> This is blockquoted";

            Assert.Equal(expected, _converter.Convert(html));
        }
        [Fact]
        public void Should_convert_blockquote_inside_blockquote()
        {
            var html = "<blockquote>This is blockquoted<blockquote>This is nested blockquoted</blockquote></blockquote>";
            var expected = "> This is blockquoted\n\n> > This is nested blockquoted";

            Assert.Equal(expected, _converter.Convert(html));
        }


        //[Fact]
        //public void Should_convert_span()
        //{
        //    var html = "<span>this is span element</span>";
        //    var expected = " this is span element ";

        //    Assert.Equal(expected, _converter.Convert(html));
        //}

        //[Fact]
        //public void Should_convert_span_inside_text_without_spaces()
        //{
        //    var html = "before<span>this is span element</span>after";
        //    var expected = "before this is span element after";

        //    Assert.Equal(expected, _converter.Convert(html));
        //}

        //[Fact]
        //public void Should_convert_span_inside_text_with_spaces()
        //{
        //    var html = "before <span>this is span element</span> after";
        //    var expected = "before this is span element after";

        //    Assert.Equal(expected, _converter.Convert(html));
        //}

        //enable when wordwrap ie enabled
        //	var html = "<p>This is a paragraph. Followed by a blockquote.</p><blockquote><p>This is a blockquote which will be truncated at 75 characters width. It will be somewhere around here.</p></blockquote>";
        //	html += "<p>Some list for you:</p><ul><li>item a</li><li>item b</li></ul><p>So which one do you choose?</p>";
        //	[Fact] public void should be able to convert a block of html", function() {
        //		var md = markdown(html);
        //		var md_str = "This is a paragraph\. Followed by a blockquote\.\n\n\> \nThis is a blockquote which will be truncated at 75 characters width\. It \nwill be somewhere around here\.\n\nSome list for you:\n\n\* item a\n\* item b\n\nSo which one do you choose\?\n\n";
        //		expect(md).toEqual(md_str);
        //	});

        //[Fact] public void should be able to convert unordered list", function() {
        //    var md = markdown("<ul><li>item a</li><li>item b</li></ul>");
        //    expect(md).toMatch(/\* item a\n\* item b\n/);
        //});

        //        [Fact] public void should be able to convert ordered list", function() {
        //            var md = markdown("<ol><li>item 1</li><li>item 2</li></ol>");
        //            expect(md).toMatch(/1\. item 1\n1\. item 2\n/);
        //        });

        //        [Fact] public void should be able to convert nested lists", function() {
        //            var md = markdown("<ul><li>item a<ul><li>item aa</li><li>item bb</li></ul></li><li>item b</li></ul>");
        //            expect(md).toMatch(/\* item a\n  \* item aa\n  \* item bb\n\* item b\n/);
        //        });

        //        [Fact] public void should not convert empty list items", function() {
        //            var md = markdown("<ol><li>item 1</li><li/></ol>");
        //            expect(md).toMatch(/1\. item 1\n/);

        //            md = markdown("<ul><li>item 1</li><li></li></ol>");
        //            expect(md).toMatch(/\* item 1\n/);
        //        });


        //        [Fact] public void should be able to convert images inline style", function() {
        //            var md = markdown("<img alt=\"Example Image\" title=\"Free example image\" src=\"/img/62838.jpg\"/>", {"inlineStyle": true});
        //            var expected = "![Example Image](/img/62838.jpg \"Free example image\")\n\n";
        //            expect(md).toEqual(expected);
        //        });

        //        [Fact] public void should be able to convert images reference style", function() {
        //            var md = markdown("<img alt=\"Example Image\" title=\"Free example image\" src=\"/img/62838.jpg\"/>");
        //            var expected = "![Example Image][0]\n\n[0]: /img/62838.jpg";
        //            expect(md).toEqual(expected);

        //            //if alt is empty then title should be used
        //            md = markdown("<img title=\"Free example image title\" src=\"/img/62838.jpg\">");
        //            var expected = "![Free example image title][0]\n\n[0]: /img/62838.jpg";
        //            expect(md).toEqual(expected);

        //        });

        //        [Fact] public void should be able to convert images as block elements", function() {
        //            var md = markdown("before<img alt=\"Example Image\" title=\"Free example image\" src=\"/img/62838.jpg\"/>after");
        //            var expected = "before\n\n![Example Image][0]\n\nafter\n\n[0]: /img/62838.jpg";
        //            expect(md).toEqual(expected);

        //            var md = markdown("before<img alt=\"Example Image\" title=\"Free example image\" src=\"/img/62838.jpg\"/>after", {"inlineStyle": true});
        //            var expected = "before\n\n![Example Image](/img/62838.jpg \"Free example image\")\n\nafter";
        //            expect(md).toEqual(expected);
        //        });

        //        [Fact] public void should not convert images if url is empty", function() {
        //            var md = markdown("<img alt=\"Example Image\" title=\"Free example image\">");
        //            expect(md).toEqual("");
        //        });

        //        [Fact] public void should be able to properly convert links reference style", function() {
        //            var html = "<a href=\"http://www.example.com\" title=\"Example\">Visit Example</a>";
        //            html += "text1";
        //            html += "<a href=\"http://www.example1.com\" title=\"Example\">Visit Example1</a>";
        //            html += "text2";
        //            html += "<a href=\"http://www.example.com\" title=\"Example\">Visit Example</a>";

        //            //urls should not be duplicated in reference style
        //            var expected = "[Visit Example][0]text1[Visit Example1][1]text2[Visit Example][0]";
        //            expected += "\n\n";
        //            expected += "[0]: http://www.example.com\n";
        //            expected += "[1]: http://www.example1.com";

        //            var md = markdown(html);
        //            expect(md).toEqual(expected);
        //        });

        //        [Fact] public void should be able to convert links inline style", function() {
        //            var md = markdown("<a href=\"http://www.example.com\" title=\"Example\">Visit Example</a>", {"inlineStyle": true});
        //            expect(md).toEqual("[Visit Example](http://www.example.com \"Example\")");
        //        });

        //        [Fact] public void should not convert if link has no text to display", function() {
        //            var html = "<a href="/"/>";
        //            md = markdown(html);
        //            expect(md).toEqual("");

        //            html = "<div class="logo">\n";
        //            html += "	<a href="/"/>\n";
        //            html += "</div>";

        //            md = markdown(html);
        //            expect(md).toEqual("");
        //        });

        //        [Fact] public void should convert elements with child elements surrounded by whitespace", function() {
        //            var html = "<div>\n\t<h2>\n\t\t<a href="http://finance.yahoo.com">Yahoo! Finance</a>\n\t</h2>\n</div>";
        //            md = markdown(html);
        //            expect(md).toEqual("## [Yahoo! Finance][0]\n\n[0]: http://finance.yahoo.com");

        //            html = "<span>\n\t<b>Hello</b>\n\t</span>";
        //            md = markdown(html);
        //            expect(md).toEqual(" **Hello** ");
        //        });


        //        [Fact] public void should convert image wrapped in anchor to markdown that can be rendered using showdown - inline style parsing", function() {
        //            var md = markdown("<a href=\"/exec/j/4/?pid=62838&lno=1&afsrc=1\"><img alt=\"Example Image\" title=\"Free example image\" src=\"/img/62838.jpg\"></a>", {"inlineStyle": true});
        //            var expected = "[![Example Image](/img/62838.jpg \"Free example image\")](/exec/j/4/?pid=62838&lno=1&afsrc=1)";
        //            expect(md).toEqual(expected);
        //        });

        //        [Fact] public void should convert image wrapped in anchor to markdown that can be rendered using showdown - reference style parsing", function() {
        //            var md = markdown("<a href="/exec/j/4/?pid=62838&lno=1&afsrc=1"><img alt="Example Image" title="Free example image" src="/img/62838.jpg"></a>", {"inlineStyle": false});
        //            var expected = "[![Example Image](/img/62838.jpg \"Free example image\")](/exec/j/4/?pid=62838&lno=1&afsrc=1)";

        //            var html = "<a href="/exec/j/4/?pid=62838&lno=1&afsrc=1">\n\t<img alt="Example Image" title="Free example image" src="/img/62838.jpg">\n\t</a>";
        //            md = markdown(html, {"inlineStyle": false});
        //            expect(md).toEqual(expected);

        //        });

        //        [Fact] public void should output only text of empty links", function() {
        //            var md = markdown("<a href="">Empty Link Text</a>", {"inlineStyle": true});
        //            var expected = "Empty Link Text";
        //            expect(md).toEqual(expected);
        //        });

        //        //tags that have no parsing rules e.g. form elements "head", "style", script", "link" "option", "noscript", "noframes", "input", "button", "select", "textarea", and "label"
        //        [Fact] public void should not convert any elements that have no parsing rules. ", function() {
        //            var html = "<head><link rel="openid.delegate" href="http://jeresig.livejournal.com/"/>";
        //            html +=	"<script src="http://ejohn.org/files/retweet.js"></script></head>";

        //            var md = markdown(html);
        //            expect(md).toEqual("");
        //        });

        //        //tables
        //        [Fact] public void should be able to convert tables", function() {
        //            var html = "<table border=\"1\">";
        //            html += "<tr><td>Row 1 Cell 1</td><td>Row 1 Cell 2</td></tr>";
        //            html += "<tr><td>Row 2 Cell 1</td><td>Row 2 Cell 2</td></tr>";
        //            html += "</table>";

        //            var md = markdown(html);

        //            var expected = "Row 1 Cell 1\n\n";
        //            expected += "Row 1 Cell 2\n\n";
        //            expected += "Row 2 Cell 1\n\n";
        //            expected += "Row 2 Cell 2\n\n";

        //            expect(md).toEqual(expected);
        //        });

        //        [Fact] public void should be able to convert tables with lists", function() {
        //            var html = "<table border=\"1\">";
        //            html += "<tr><td width=\"50%\"><ul><li>List Item 1</li><li>List Item 2</li></ul></td>";
        //            html += "<td><ul><li>List Item 3</li><li>List Item 4</li></ul></td></tr>";
        //            html += "</table>";

        //            var md = markdown(html);
        //            var expected = "* List Item 1\n* List Item 2\n\n* List Item 3\n* List Item 4\n\n";

        //            expect(md).toEqual(expected);
        //        });

        //        //test empty block element
        //        [Fact] public void should not convert emptyt tags", function() {
        //            var md = markdown("<div>        </div>");
        //            expect(md).toEqual("");

        //            md = markdown("<h1>        </h1>");
        //            expect(md).toEqual("");

        //            md = markdown("<b>        </b>");
        //            expect(md).toEqual("");
        //        });

        //        [Fact] public void should collape whitespace to single space for text nodes", function() {
        //            var md = markdown("<div>     a     b     c\n     d    </div>");
        //            expect(md).toEqual(" a b c d \n\n");

        //            md = markdown("<div></div><div>     a     b     c\n     d    </div>");
        //            expect(md).toEqual(" a b c d \n\n");

        //            md = markdown("<div>1</div><div>     a     b     c\n     d    </div>");
        //            expect(md).toEqual("1\n\na b c d \n\n");

        //            md = markdown("<h1>     a     b     c\n     d </h1>");
        //            expect(md).toEqual("# a b c d \n\n");
        //        });

        //        [Fact] public void should trim anchor title and text", function() {
        //            var md = markdown("<a href=\"http://www.example.com\" title=\"   Example   \">   Visit Example    </a>", {"inlineStyle": true});
        //            expect(md).toEqual("[Visit Example](http://www.example.com \"Example\")");

        //            md = markdown("<a href=\"http://www.example.com\" title=\"   Example   \">   Visit Example    </a>", {"inlineStyle": false});
        //            expect(md).toEqual("[Visit Example][0]\n\n[0]: http://www.example.com");

        //            var html ="<a href="/blog/line-length-readability#comments">\n";
        //            html += "<span itemprop="interactionCount">32</span>\n";
        //            html += "comments\n</a>";

        //            md = markdown(html);
        //            expect(md).toEqual("[32 comments][0]\n\n[0]: /blog/line-length-readability#comments");
        //        });

        //        [Fact] public void should trim image alt and title", function() {
        //            var html = "<img alt=\"  Example Image   \" title=\"   Free example image   \" src=\"/img/62838.jpg\">";

        //            var md = markdown(html, {"inlineStyle": true});
        //            var expected = "![Example Image](/img/62838.jpg \"Free example image\")\n\n";
        //            expect(md).toEqual(expected);

        //            md = markdown(html);
        //            expected = "![Example Image][0]\n\n[0]: /img/62838.jpg";
        //            expect(md).toEqual(expected);
        //        });

        //        [Fact] public void should be able to convert image followed by link to markdown that can be renderd using showdown", function() {
        //            var html = "<p>\n";
        //            html += "	<img alt="Feed" class="icon" src="http://mementodb.com/images/logo.png"/>\n";
        //            html += "	<a href="http://mementodb.com">Memento</a>\n";
        //            html += "</p>";

        //            var md = markdown(html);
        //            var expected = "![Feed][0]\n\n[Memento][1]\n\n";
        //            expected += "[0]: http://mementodb.com/images/logo.png\n";
        //            expected += "[1]: http://mementodb.com";

        //            expect(md).toEqual(expected);
        //        });

        //        [Fact] public void should be able to convert list items with linked images as only linked images", function() {
        //            var html = "before list";
        //                html += "<ul>\n";
        //                html += "	<li><div class="curve-down"><a href="/ipad/#video"><img src="http://images.apple.com/home/images/promo_video_ipad_launch.png" alt="Watch the new iPad video" width="237" height="155" /><span class="play"></span></a></div></li>";
        //                html += "	<li><div class="curve-down"><a href="/iphone/videos/#tv-ads-datenight"><img src="http://images.apple.com/home/images/promo_video_iphone4s_ad.png" alt="Watch the new iPhone TV Ad" width="237" height="155" /><span class="play"></span></a></div></li>";
        //                html += "</ul>\n";
        //            var md = markdown(html);
        //            var expected = "before list\n\n";
        //            expected += "[![Watch the new iPad video](http://images.apple.com/home/images/promo_video_ipad_launch.png)](/ipad/#video)\n\n";
        //            expected += "[![Watch the new iPhone TV Ad](http://images.apple.com/home/images/promo_video_iphone4s_ad.png)](/iphone/videos/#tv-ads-datenight)\n\n";
        //            expect(md).toEqual(expected);
        //        });

        //        [Fact] public void should be able to convert title", function() {
        //            var html = "<hgroup>\n";
        //            html += "\t<h1><a href="http://www.google.com">Nathen Harvey</a></h1>\n";
        //            html += "\t<h2>a blog</h2>\n";
        //            html += "</hgroup>";
        //            var md = markdown(html);

        //            var expected = "# [Nathen Harvey][0]\n\n## a blog\n\n\n\n[0]: http://www.google.com";
        //            expect(md).toEqual(expected);
        //        });

        //        [Fact] public void should be able to convert paragrphs in blocquotes", function() {
        //            var html="<blockquote>\n";
        //            html+="\t<p>Lorem ipsum</p>\n";
        //            html+="\t<p>Lorem ipsum</p>\n";
        //            html+="</blockquote>";

        //            var md = markdown(html);
        //            var expected = "> Lorem ipsum\n\n> Lorem ipsum\n\n";
        //            expect(md).toEqual(expected);

        //            html = "<blockquote>\n";
        //            html+="\t<p>Lorem ipsum</p>\n";
        //            html+="</blockquote>\n";
        //            html+="<blockquote>\n";
        //            html+="\t<p>Lorem ipsum</p>\n";
        //            html+="</blockquote>"

        //            md = markdown(html);
        //            expect(md).toEqual(expected);
        //        });

        //        [Fact] public void should be able to convert pre block", function() {
        //            var html = "<pre>";
        //            html += "	void main(String[] args) {\n";
        //            html += "		System.out.println(\"Hello Markdown\");\n";
        //            html += "	}";
        //            html += "</pre>";

        //            var expected = "    " + "	void main(String[] args) {\n";
        //            expected += "    " + "		System.out.println(\"Hello Markdown\");\n";
        //            expected += "    " + "	}";
        //            expected += "\n\n";

        //            var md = markdown(html);
        //            expect(md).toEqual(expected);
        //        });

        //        [Fact] public void should be able to convert pre block with html tags", function() {
        //            var html = "<pre>\n";
        //            html += "<div a=\"b\">\n";
        //            html += "	<span>this is span inside pre block</span>\n";
        //            html += "	this is paragraph inside pre block\n";
        //            html += "</div>";
        //            html += "</pre>";

        //            var expected = "    " + "\n\n\n";
        //            expected += "    " + "	this is span inside pre block\n";
        //            expected += "    " + "	this is paragraph inside pre block\n";
        //            expected += "    " + "\n";
        //            expected += "\n";

        //            var md = markdown(html);
        //            expect(md).toEqual(expected);
        //        });

        //        [Fact] public void should be able to convert <pre><code>...</code></pre> blocks", function() {
        //            var html= "<pre><code>{% blockquote [author[, source]] [link] [source_link_title] %}";
        //            html+= "\nQuote string";
        //            html+= "\n{% endblockquote %}";
        //            html+= "\n</code></pre>";

        //            var md = markdown(html);
        //            expected="    {% blockquote [author[, source]] [link] [source_link_title] %}";
        //            expected+="\n    Quote string";
        //            expected+="\n    {% endblockquote %}";
        //            expected+="\n    ";
        //            expected+="\n\n";

        //            expect(md).toEqual(expected);
        //        });
        //    });
        //}

        //describe("markdownDOMParser", function() {
        //    [Fact] public void parser function should be able to echo input html", function() {
        //        var html = "<div><span id=\"test-id\"> Hmm <br/> Hello markdown converter </span><!-- this is comment --></div>";
        //        var result ="";

        //        markdownHTMLParser(html, {
        //            start: function(tag, attrs, unary) {
        //                result+="<"+tag.toLowerCase();

        //                for ( var i = 0; i < attrs.length; i++ ) {
        //                    result += " " + attrs[i].name + "="" + attrs[i].value + """;
        //                }

        //                result += (unary ? "/" : "") + ">";
        //            },
        //            chars: function(text) {
        //                result += text;
        //            },
        //            end: function(tag) {
        //                result+="</"+tag.toLowerCase()+">";
        //            },
        //            comment: function(text) {
        //                result += "<!--" + text + "-->";
        //            }
        //        });
        //        expect(html).toEqual(result);
        //    });
        //});

        ////TODO add test for block function
        ////TODO test bookmarklet links
        ////TODO add test for xss protection
        ////TODO test parsing of iframe/frame element
        ////TODO add tests to verify hidden nodes are not parsed
        ////TODO add more unit tests based on official markdown syntax
        ////TODO improve formatting of pre/code tags
    }
}