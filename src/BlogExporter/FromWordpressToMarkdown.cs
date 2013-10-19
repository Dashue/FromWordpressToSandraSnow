using BlogExportParsers;
using HtmlConverters;
using System;
using System.IO;

namespace BlogExporter
{
    public class FromWordpressToMarkdown
    {
        private WordpressExportParser _exportParser;
        private HtmlToMarkdownConverter _htmlToMarkdownConverter;

        public FromWordpressToMarkdown()
        {
            _exportParser = new WordpressExportParser();
            _htmlToMarkdownConverter = new HtmlToMarkdownConverter();

        }

        public void Convert(string exportPath)
        {
            string blogExport = File.ReadAllText(exportPath);

            var blogEntries = _exportParser.Parse(blogExport);

            foreach (var blogEntry in blogEntries)
            {
                string publicationStatus = null;
                switch (blogEntry.Status)
                {
                    case "draft":
                        publicationStatus = "draft";
                        break;
                    case "publish":
                        publicationStatus = "true";
                        break;
                    case "inherit":
                    case "trash":
                        publicationStatus = "private";
                        break;

                }

                if (false == string.IsNullOrWhiteSpace(publicationStatus))
                {
                    var fullPath = GetFullPath(blogEntry);

                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }

                    var file = File.CreateText(fullPath);

                    WriteHeader(file, blogEntry, publicationStatus);
                    WriteContent(file, blogEntry);

                    file.Close();
                }
            }
        }


        private void WriteContent(StreamWriter file, BlogEntry blogEntry)
        {
            if (false == string.IsNullOrWhiteSpace(blogEntry.Content))
            {
                var markdown = _htmlToMarkdownConverter.Convert(blogEntry.Content);

                file.Write(markdown);
            }
        }

        private static string GetFullPath(BlogEntry blogEntry)
        {
            var postDate = DateTime.Parse(blogEntry.PostDate).Date.ToShortDateString();
            string postName;

            if (string.IsNullOrWhiteSpace(blogEntry.PostName))
            {
                postName = blogEntry.Title.Replace(" ", "-").Replace("\"", "");
            }
            else
            {
                postName = blogEntry.PostName;
            }
            var path = Environment.CurrentDirectory + @"\_posts\";
            var fileName = string.Format("{0}-{1}.{2}", postDate, postName, "md");
            var fullPath = path + fileName;
            return fullPath;
        }

        private void WriteHeader(StreamWriter file, BlogEntry blogEntry, string publicationStatus)
        {
            file.WriteLine("---");
            file.WriteLine("layout: post");
            file.WriteLine("title: {0}", blogEntry.Title);
            file.WriteLine("categories: {0}", string.Join(",", blogEntry.Categories));
            file.WriteLine("published: {0}", publicationStatus);
            file.WriteLine("---");
        }
    }
}
