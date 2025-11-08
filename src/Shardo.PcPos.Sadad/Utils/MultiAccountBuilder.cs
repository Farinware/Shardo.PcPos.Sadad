using System.Text;
namespace Shardo.PcPos.Sadad
{
    public sealed class MultiAccountBuilder
    {
        private readonly List<(string Key, string Value)> _items = new List<(string Key, string Value)>();
        public static MultiAccountBuilder ByRows(params (string rowNo, string percentOrAmount)[] items)
        {
            var b = new MultiAccountBuilder();
            foreach (var (k, v) in items) b._items.Add((k, v));
            return b;
        }
        public static MultiAccountBuilder Empty() => new MultiAccountBuilder();
        public MultiAccountBuilder Add(string key, string percentOrAmount)
        {
            _items.Add((key, percentOrAmount));
            return this;
        }
        public string Build()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < _items.Count; i++)
            {
                var (k, v) = _items[i];
                if (i > 0) sb.Append(',');
                sb.Append(k).Append(':').Append(v);
            }
            return sb.ToString();
        }
    }
}
