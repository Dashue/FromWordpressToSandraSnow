using System.Collections.Generic;

namespace BlogExportParsers
{
    public class BlogCategoriesParser
    {
        public static bool TryParse(out List<string> categories, dynamic category)
        {
            categories = new List<string>();
            if (category is IList<DynamicXml>)
            {
                foreach (var c in category)
                {
                    categories.Add(c.Value);
                }

                return categories.Count == category.Count;
            }
            else
            {
                categories.Add(category);

                return true;
            }
        }
    }
}