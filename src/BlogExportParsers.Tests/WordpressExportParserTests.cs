using System;
using System.IO;
using Xunit;

namespace BlogExportParsers.Tests
{


    public class WordpressExportParserTests
    {
        [Fact]
        public void Parse()
        {
            string path = Environment.CurrentDirectory + "\\" + "WordpressExport.xml";
            var export = File.ReadAllText(path);

            var wordPressExportParser = new WordpressExportParser();

            var blogEntries = wordPressExportParser.Parse(export);

            var first = blogEntries[0];
            Assert.Equal("Blog entry title", first.Title);
            Assert.Equal("<p>Blog entry content</p>", first.Content);
            Assert.Equal("post-name", first.PostName);
            Assert.Equal("2012-04-09", first.PostDate);
            Assert.Equal(".Net", first.Categories[0]);
            Assert.Equal("Rant", first.Categories[1]);
            Assert.Equal("publish", first.Status);
        }
    }
}