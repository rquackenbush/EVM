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
            if(sourceLine == null)
                throw new ArgumentNullException("sourceLine");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            if (opCodeInfo == null)
                throw new ArgumentNullException("opCodeInfo");

            this.SourceLine = sourceLine;
            this.Arguments = arguments;
            this.OpCodeInfo = opCodeInfo;
        }

        public SourceLine SourceLine { get; private set; }

        public string[] Arguments { get; private set; }

        public OpCodeInfo OpCodeInfo { get; private set; }
    }
}
