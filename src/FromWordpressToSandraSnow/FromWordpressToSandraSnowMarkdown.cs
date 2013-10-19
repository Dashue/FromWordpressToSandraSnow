using BlogExportParsers;
using HtmlToMarkdown.Net;
using System;
using System.Collections.Generic;
using System.IO;

namespace FromWordpressToSandraSnow
{
    public class FromWordpressToMarkdown
    {
        private readonly WordpressExportParser _exportParser;
        private readonly HtmlToMarkdownConverter _htmlToMarkdownConverter;

        public FromWordpressToMarkdown()
        {
            _exportParser = new WordpressExportParser();
            _htmlToMarkdownConverter = new HtmlToMarkdownConverter();
        }

        public void Convert(string exportPath)
        {
            string blogExport = File.ReadAllText(exportPath);

            List<BlogEntry> blogEntries = _exportParser.Parse(blogExport);

            foreach (BlogEntry blogEntry in blogEntries)
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
                    string fullPath = GetFullPath(blogEntry);

                    string directoryPath = Path.GetDirectoryName(fullPath);
                    if (false == string.IsNullOrWhiteSpace(directoryPath) &&
                        false == Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }

                    StreamWriter file = File.CreateText(fullPath);

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
                string markdown = _htmlToMarkdownConverter.Convert(blogEntry.Content);

                file.Write(markdown);
            }
        }

        private static string GetFullPath(BlogEntry blogEntry)
        {
            string postDate = DateTime.Parse(blogEntry.PostDate).Date.ToShortDateString();
            string postName;

            if (string.IsNullOrWhiteSpace(blogEntry.PostName))
            {
                postName = blogEntry.Title.Replace(" ", "-").Replace("\"", "");
            }
            else
            {
                postName = blogEntry.PostName;
            }
            string path = Environment.CurrentDirectory + @"\_posts\";
            string fileName = string.Format("{0}-{1}.{2}", postDate, postName, "md");
            string fullPath = path + fileName;
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