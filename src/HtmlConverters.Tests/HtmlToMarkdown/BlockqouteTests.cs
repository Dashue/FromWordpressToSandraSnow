using System;
using Xunit;

namespace HtmlConverters.Tests.HtmlToMarkdown
{
    public class BlockqouteTests
    {
        private HtmlToMarkdownConverter _converter;

        public BlockqouteTests()
        {
            _converter = new HtmlToMarkdownConverter();
        }

        [Fact]
        public void Should_convert_blockqoutes()
        {
            Assert.Equal("> This is blockquoted", _converter.Convert("<blockquote>This is blockquoted</blockquote>"));
        }

        [Fact]
        public void Should_convert_blockqoutes_nested()
        {
            string html = "<blockquote>This is blockquoted<blockquote>This is nested blockquoted</blockquote></blockquote>";
            Assert.Equal("> This is blockquoted\n\n> > This is nested blockquoted", _converter.Convert(html));
        }

        [Fact]
        public void Should_convert_blockqoutes_from_block_of_html()
        {
            throw new NotImplementedException();
            var html = "<p>This is a paragraph. Followed by a blockquote.</p><blockquote><p>This is a blockquote which will be truncated at 75 characters width. It will be somewhere around here.</p></blockquote>";
            html += "<p>Some list for you:</p><ul><li>item a</li><li>item b</li></ul><p>So which one do you choose?</p>";

            var expected =
                @"This is a paragraph\. Followed by a blockquote\.\n\n\> \nThis is a blockquote which will be truncated at 75 characters width\. It \nwill be somewhere around here\.\n\nSome list for you:\n\n\* item a\n\* item b\n\nSo which one do you choose\?\n\n";

            Assert.Equal(expected, _converter.Convert(html));
        }
    }
}