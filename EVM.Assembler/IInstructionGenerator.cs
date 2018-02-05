namespace EVM.Assembler
{
    public interface IInstructionGenerator
    {
        byte[] GenerateInstruction(FinalAssembleContext context);
    }
}
