using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVM.Assembler
{
    public static class StringExtensions
    {
        // http://stackoverflow.com/a/20857897/232566
        public static string RemoveWhitespace(this string value)
        {
            if (value == null)
                return null;

            int j = 0, inputlen = value.Length;
            char[] newarr = new char[inputlen];

            for (int i = 0; i < inputlen; ++i)
            {
                char tmp = value[i];

                if (!char.IsWhiteSpace(tmp))
                {
                    newarr[j] = tmp;
                    ++j;
                }
            }

            return new String(newarr, 0, j);
        }
    }

    
}
