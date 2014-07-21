using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVM.Assembler
{
    public interface IInstructionGenerator
    {
        byte[] GenerateInstruction(FinalAssembleContext context);
    }
}
