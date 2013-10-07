using System.Collections.Generic;

namespace BlogExportParsers
{
    public class WordpressExportParser : IBlogExportParser
    {
        public List<BlogEntry> Parse(string export)
        {
            var cleanedContent = CleanExport(export);

            dynamic blogExport = DynamicXml.Parse(cleanedContent);

            var items = new List<BlogEntry>();

            foreach (dynamic item in blogExport.channel.item)
            {
                var categories = new List<string>();

                if (item.category != null)
                {
                    BlogCategoriesParser.TryParse(out categories, item.category);
                }

                items.Add(new BlogEntry
                {
                    Title = item.title,
                    Content = item.content,
                    PostName = item.post_name,
                    Status = item.status,
                    PostDate = item.post_date.Substring(0, 10),
                    Categories = categories
                });
            }

            return items;
        }

        internal string CleanExport(string export)
        {
            return export.Replace(":encoded", string.Empty).
                          Replace("wp:", string.Empty);
        }
    }
}