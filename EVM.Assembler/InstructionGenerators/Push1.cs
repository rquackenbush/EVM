using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVM.Assembler.InstructionGenerators
{
    public class Push1 : IInstructionGenerator
    {
        public byte[] GenerateInstruction(FinalAssembleContext context)
        {
            byte value;

            if (!byte.TryParse(context.ParsedInstruction.Arguments[0], out value))
            {
                context.Messages.Add(new AssemblerMessage(ErrorLevel.Error, 
                    string.Format("Unable to parse argument 1"), 
                    context.ParsedInstruction.SourceLine.LineNumber));
                return null;
            }

            return new[] {value};
        }
    }
}
