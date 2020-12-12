using System;
using System.Collections.Generic;
using System.Text;

namespace SakeponNode.Extensions
{
    internal static  class IReadOnlyListExtension
    {
        public static int IndexOf<T>(this IReadOnlyList<T> self, T elementToFind)
        {
            int i = 0;
            foreach (T element in self)
            {
                if (Equals(element, elementToFind))
                    return i;
                i++;
            }
            return -1;
        }
    }
}
