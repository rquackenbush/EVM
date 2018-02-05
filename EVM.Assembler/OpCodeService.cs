namespace EVM.Assembler.OpCodes
{
    public abstract class OpCodeService
    {
        protected OpCodeService(OpCode opCode, string assemblerKeyword, byte instructionSize, byte numberOfArguments)
        {
            OpCode = opCode;
            AssemblerKeyword = assemblerKeyword;
            InstructionSize = instructionSize;
            NumberOfArguments = numberOfArguments;

            //if (instructionSize > 1 && instructionGenerator == null)
            //    throw new ArgumentNullException(nameof(instructionGenerator),
            //        $"[{assemblerKeyword}]: instructionGenerator is required when the instruction size is greater than 1.");

            //InstructionGenerator = instructionGenerator;
        }

        public OpCode OpCode { get; }
        
        public string AssemblerKeyword { get; }

        public byte InstructionSize { get; }

        public byte NumberOfArguments { get; }

        public virtual byte[] GenerateInstruction(FinalAssembleContext context)
        {
            return null;
        }
    }
}
