using System;

namespace EVM.Assembler
{
    public class OpCodeInfo
    {
        public OpCodeInfo(byte opCode, string assemblerKeyword, byte instructionSize, byte numberOfArguments, IInstructionGenerator instructionGenerator = null)
        {
            this.OpCode = opCode;
            this.AssemblerKeyword = assemblerKeyword;
            this.InstructionSize = instructionSize;
            this.NumberOfArguments = numberOfArguments;

            if (instructionSize > 1 && instructionGenerator == null)
                throw new ArgumentNullException("instructionGenerator", string.Format("[{0}]: instructionGenerator is required when the instruction size is greater than 1.", assemblerKeyword));

            this.InstructionGenerator = instructionGenerator;
        }

        public byte OpCode { get; private set; }
        
        public string AssemblerKeyword { get; private set; }

        public byte InstructionSize { get; private set; }

        public byte NumberOfArguments { get; private set; }

        public IInstructionGenerator InstructionGenerator { get; private set; }
    }
}
