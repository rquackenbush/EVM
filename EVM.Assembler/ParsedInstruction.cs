using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EVM.Assembler
{
    public class ParsedInstruction
    {
        public ParsedInstruction(SourceLine sourceLine, string[] arguments, OpCodeInfo opCodeInfo)
        {
            this.SourceLine = sourceLine ?? throw new ArgumentNullException(nameof(sourceLine));
            this.Arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
            this.OpCodeInfo = opCodeInfo ?? throw new ArgumentNullException(nameof(opCodeInfo));
        }

        public SourceLine SourceLine { get; }

        public string[] Arguments { get; }

        public OpCodeInfo OpCodeInfo { get; }
    }
}
