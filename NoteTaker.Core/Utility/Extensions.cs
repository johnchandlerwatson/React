using System.Collections.Generic;
using System.Linq;

namespace NoteTaker.Core.Utility
{
    public static class Extensions
    {
        public static bool InList<TItem>(this TItem item, List<TItem> list)
        {
            return list.Contains(item);
        }

        public static bool InList<TItem>(this TItem item, params TItem[] list)
        {
            return list.Contains(item);
        }
    }
}
