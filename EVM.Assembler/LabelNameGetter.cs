using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVM.Assembler
{
    public static class LabelNameGetter
    {
        public static string GetLabelName(string labelIdentifier)
        {
            if (string.IsNullOrWhiteSpace(labelIdentifier))
                return null;

            return labelIdentifier.Replace(":", "");

        }
    }
}
