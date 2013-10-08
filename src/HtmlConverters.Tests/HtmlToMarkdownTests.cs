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

        [Fact]
        public void Should_convert_elements_with_child_elements_surrounded_by_whitespace()
        {
            Assert.Equal("**Hello**", _converter.Convert("<span>\n\t<b>Hello</b>\n\t</span>"));

            var html = "<div>\n\t<h2>\n\t\t<a href='http://finance.yahoo.com'>Yahoo! Finance</a>\n\t</h2>\n</div>";
            var expected = "## [Yahoo! Finance][0]\n\n[0]: http://finance.yahoo.com";

            Assert.Equal(expected, _converter.Convert(html));
        }

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


        //    });
        //}

        //TODO add test for block function
        //TODO test bookmarklet links
        //TODO add test for xss protection
        //TODO test parsing of iframe/frame element
        //TODO add tests to verify hidden nodes are not parsed
        //TODO add more unit tests based on official markdown syntax
        //TODO improve formatting of pre/code tags 
    }
}