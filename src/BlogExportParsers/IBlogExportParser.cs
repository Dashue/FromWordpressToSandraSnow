using System.Collections.Generic;

namespace BlogExportParsers
{
    public interface IBlogExportParser
    {
        List<BlogEntry> Parse(string export);
    }
}