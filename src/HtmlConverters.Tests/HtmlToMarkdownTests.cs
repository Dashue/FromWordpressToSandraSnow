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
        public void Should_trim_text_inside_inline_element()
        {
            Assert.Equal("**String**", _converter.Convert("<strong> String </strong>"));
        }

        [Fact]
        public void Should_convert_text_with_strong_and_em()
        {
            string html = "This has <strong>blocked and <em>italicized</em></strong> texts.";
            Assert.Equal("This has **blocked and _italicized_** texts.", _converter.Convert(html));
        }

        //        it("should convert elements with child elements surrounded by whitespace", function() {
        //            var html = "<div>\n\t<h2>\n\t\t<a href='http://finance.yahoo.com'>Yahoo! Finance</a>\n\t</h2>\n</div>";
        //            md = markdown(html);
        //            expect(md).toEqual("## [Yahoo! Finance][0]\n\n[0]: http://finance.yahoo.com");

        //            html = "<span>\n\t<b>Hello</b>\n\t</span>";
        //            md = markdown(html);
        //            expect(md).toEqual(" **Hello** ");
        //        });


        //        it("should convert image wrapped in anchor to markdown that can be rendered using showdown - inline style parsing", function() {
        //            var md = markdown("<a href=\"/exec/j/4/?pid=62838&lno=1&afsrc=1\"><img alt=\"Example Image\" title=\"Free example image\" src=\"/img/62838.jpg\"></a>", {"inlineStyle": true});
        //            var expected = "[![Example Image](/img/62838.jpg \"Free example image\")](/exec/j/4/?pid=62838&lno=1&afsrc=1)";
        //            expect(md).toEqual(expected);
        //        });

        //        it("should convert image wrapped in anchor to markdown that can be rendered using showdown - reference style parsing", function() {
        //            var md = markdown("<a href='/exec/j/4/?pid=62838&lno=1&afsrc=1'><img alt='Example Image' title='Free example image' src='/img/62838.jpg'></a>", {"inlineStyle": false});
        //            var expected = "[![Example Image](/img/62838.jpg \"Free example image\")](/exec/j/4/?pid=62838&lno=1&afsrc=1)";

        //            var html = "<a href='/exec/j/4/?pid=62838&lno=1&afsrc=1'>\n\t<img alt='Example Image' title='Free example image' src='/img/62838.jpg'>\n\t</a>";
        //            md = markdown(html, {"inlineStyle": false});
        //            expect(md).toEqual(expected);

        //        });

        //        it("should output only text of empty links", function() {
        //            var md = markdown("<a href=''>Empty Link Text</a>", {"inlineStyle": true});
        //            var expected = "Empty Link Text";
        //            expect(md).toEqual(expected);
        //        });

        //        //tags that have no parsing rules e.g. form elements 'head', 'style', script', 'link' 'option', 'noscript', 'noframes', 'input', 'button', 'select', 'textarea', and 'label'
        //        it("should not convert any elements that have no parsing rules. ", function() {
        //            var html = "<head><link rel='openid.delegate' href='http://jeresig.livejournal.com/'/>";
        //            html +=	"<script src='http://ejohn.org/files/retweet.js'></script></head>";

        //            var md = markdown(html);
        //            expect(md).toEqual("");
        //        });

        //        //tables
        //        it("should be able to convert tables", function() {
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

        //        it("should be able to convert tables with lists", function() {
        //            var html = "<table border=\"1\">";
        //            html += "<tr><td width=\"50%\"><ul><li>List Item 1</li><li>List Item 2</li></ul></td>";
        //            html += "<td><ul><li>List Item 3</li><li>List Item 4</li></ul></td></tr>";
        //            html += "</table>";

        //            var md = markdown(html);
        //            var expected = "* List Item 1\n* List Item 2\n\n* List Item 3\n* List Item 4\n\n";

        //            expect(md).toEqual(expected);
        //        });

        //        it("should collape whitespace to single space for text nodes", function() {
        //            var md = markdown("<div>     a     b     c\n     d    </div>");
        //            expect(md).toEqual(" a b c d \n\n");

        //            md = markdown("<div></div><div>     a     b     c\n     d    </div>");
        //            expect(md).toEqual(" a b c d \n\n");

        //            md = markdown("<div>1</div><div>     a     b     c\n     d    </div>");
        //            expect(md).toEqual("1\n\na b c d \n\n");

        //            md = markdown("<h1>     a     b     c\n     d </h1>");
        //            expect(md).toEqual("# a b c d \n\n");
        //        });

        //        it("should trim anchor title and text", function() {
        //            var md = markdown("<a href=\"http://www.example.com\" title=\"   Example   \">   Visit Example    </a>", {"inlineStyle": true});
        //            expect(md).toEqual("[Visit Example](http://www.example.com \"Example\")");

        //            md = markdown("<a href=\"http://www.example.com\" title=\"   Example   \">   Visit Example    </a>", {"inlineStyle": false});
        //            expect(md).toEqual("[Visit Example][0]\n\n[0]: http://www.example.com");

        //            var html ="<a href='/blog/line-length-readability#comments'>\n";
        //            html += "<span itemprop='interactionCount'>32</span>\n";
        //            html += "comments\n</a>";

        //            md = markdown(html);
        //            expect(md).toEqual("[32 comments][0]\n\n[0]: /blog/line-length-readability#comments");
        //        });

        //        it("should trim image alt and title", function() {
        //            var html = "<img alt=\"  Example Image   \" title=\"   Free example image   \" src=\"/img/62838.jpg\">";

        //            var md = markdown(html, {"inlineStyle": true});
        //            var expected = "![Example Image](/img/62838.jpg \"Free example image\")\n\n";
        //            expect(md).toEqual(expected);

        //            md = markdown(html);
        //            expected = "![Example Image][0]\n\n[0]: /img/62838.jpg";
        //            expect(md).toEqual(expected);
        //        });

        //        it("should be able to convert image followed by link to markdown that can be renderd using showdown", function() {
        //            var html = "<p>\n";
        //            html += "	<img alt='Feed' class='icon' src='http://mementodb.com/images/logo.png'/>\n";
        //            html += "	<a href='http://mementodb.com'>Memento</a>\n";
        //            html += "</p>";

        //            var md = markdown(html);
        //            var expected = "![Feed][0]\n\n[Memento][1]\n\n";
        //            expected += "[0]: http://mementodb.com/images/logo.png\n";
        //            expected += "[1]: http://mementodb.com";

        //            expect(md).toEqual(expected);
        //        });

        //        it("should be able to convert list items with linked images as only linked images", function() {
        //            var html = "before list";
        //                html += "<ul>\n";
        //                html += "	<li><div class='curve-down'><a href='/ipad/#video'><img src='http://images.apple.com/home/images/promo_video_ipad_launch.png' alt='Watch the new iPad video' width='237' height='155' /><span class='play'></span></a></div></li>";
        //                html += "	<li><div class='curve-down'><a href='/iphone/videos/#tv-ads-datenight'><img src='http://images.apple.com/home/images/promo_video_iphone4s_ad.png' alt='Watch the new iPhone TV Ad' width='237' height='155' /><span class='play'></span></a></div></li>";
        //                html += "</ul>\n";
        //            var md = markdown(html);
        //            var expected = "before list\n\n";
        //            expected += "[![Watch the new iPad video](http://images.apple.com/home/images/promo_video_ipad_launch.png)](/ipad/#video)\n\n";
        //            expected += "[![Watch the new iPhone TV Ad](http://images.apple.com/home/images/promo_video_iphone4s_ad.png)](/iphone/videos/#tv-ads-datenight)\n\n";
        //            expect(md).toEqual(expected);
        //        });

        //        it("should be able to convert title", function() {
        //            var html = "<hgroup>\n";
        //            html += "\t<h1><a href='http://www.google.com'>Nathen Harvey</a></h1>\n";
        //            html += "\t<h2>a blog</h2>\n";
        //            html += "</hgroup>";
        //            var md = markdown(html);

        //            var expected = "# [Nathen Harvey][0]\n\n## a blog\n\n\n\n[0]: http://www.google.com";
        //            expect(md).toEqual(expected);
        //        });

        //        it("should be able to convert paragrphs in blocquotes", function() {
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

        //        it("should be able to convert pre block", function() {
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

        //        it("should be able to convert pre block with html tags", function() {
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

        //        it("should be able to convert <pre><code>...</code></pre> blocks", function() {
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
        //    it("parser function should be able to echo input html", function() {
        //        var html = "<div><span id=\"test-id\"> Hmm <br/> Hello markdown converter </span><!-- this is comment --></div>";
        //        var result ="";

        //        markdownHTMLParser(html, {
        //            start: function(tag, attrs, unary) {
        //                result+="<"+tag.toLowerCase();

        //                for ( var i = 0; i < attrs.length; i++ ) {
        //                    result += " " + attrs[i].name + '="' + attrs[i].value + '"';
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

        //TODO add test for block function
        //TODO test bookmarklet links
        //TODO add test for xss protection
        //TODO test parsing of iframe/frame element
        //TODO add tests to verify hidden nodes are not parsed
        //TODO add more unit tests based on official markdown syntax
        //TODO improve formatting of pre/code tags 
    }
}