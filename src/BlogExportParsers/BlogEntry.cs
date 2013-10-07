using System.Collections.Generic;

namespace BlogExportParsers
{
    public class BlogEntry
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string PostName { get; set; }

        public string Status { get; set; }

        public string PostDate { get; set; }
        public List<string> Categories { get; set; }
    }
}