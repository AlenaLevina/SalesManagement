using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Helpers
{
    public static class StringHelper
    {
        public static string Concat(this IEnumerable<string> values, string separator)
        {
            if (values == null) throw new ArgumentNullException("values");

            if (separator == null) return String.Concat(values);
            var result = String.Empty;
            var enumerable = values as IList<string> ?? values.ToList();
            for (var i=0;i<enumerable.Count();i++)
            {
                if (i == 0) result = enumerable.ElementAt(i);
                else
                {
                    result = result + separator + enumerable.ElementAt(i);
                }
            }
            return result;
        }
    }
}
