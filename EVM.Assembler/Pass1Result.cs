using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVM.Assembler
{
    public class Pass1Result
    {
        public Pass1Result(IDictionary<string, byte> labels, ParsedInstruction[] parsedInstructions)
        {
            if (labels == null)
                throw new ArgumentNullException("labels");

            if (parsedInstructions == null)
                throw new ArgumentNullException("parsedInstructions");

            this.Labels = labels;
            this.ParsedInstructions = parsedInstructions;
        }

        public IDictionary<string, byte> Labels { get; private set; }

        public ParsedInstruction[] ParsedInstructions { get; private set; }
    }
}
