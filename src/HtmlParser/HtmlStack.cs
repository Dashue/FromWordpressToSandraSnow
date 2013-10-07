using System.Collections.Generic;

namespace HtmlParser
{
    public class HtmlStack
    {
        private readonly List<string> _items;

        public int Length { get; set; }
        public int Count { get { return _items.Count; } }

        public HtmlStack()
        {
            _items = new List<string>();
        }

        public string Last()
        {
            if (_items.Count == 0)
            {
                return string.Empty;
            }
            return _items[_items.Count - 1];
        }

        public string At(int index)
        {
            return _items[index];
        }

        public void push(string tagName)
        {
            _items.Add(tagName);
            Length++;
        }

        public List<string> ToArray()
        {
            return new List<string>(_items);
        }
    }
}