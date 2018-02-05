using System;
using System.Collections.Generic;

namespace EVM.Assembler
{
    public class Pass1Result
    {
        public Pass1Result(IDictionary<string, byte> labels, ParsedInstruction[] parsedInstructions)
        {
            this.Labels = labels ?? throw new ArgumentNullException(nameof(labels));
            this.ParsedInstructions = parsedInstructions ?? throw new ArgumentNullException(nameof(parsedInstructions));
        }

        public IDictionary<string, byte> Labels { get; }

        public ParsedInstruction[] ParsedInstructions { get; }
    }
}
