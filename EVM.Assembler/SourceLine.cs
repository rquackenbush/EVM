using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EVM.Assembler
{
    public class SourceLine
    {
        public SourceLine(int lineNumber, string line)
        {
            this.LineNumber = lineNumber;
            this.Line = line;
        }

        public int LineNumber { get; private set; }

        /// <summary>
        /// This is the original line of source code
        /// </summary>
        public string Line { get; private set; }
    }
}
