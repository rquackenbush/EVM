using EVM.Assembler.OpCodes;

namespace EVM.Assembler.OpCodeServices
{
    public class Push1 : OpCodeService
    {
        public Push1()
            : base(OpCodes.OpCode.Push1, "push_1",
                2,
                1)
        {
        }

        public override byte[] GenerateInstruction(FinalAssembleContext context)
        {
            if (!byte.TryParse(context.ParsedInstruction.Arguments[0], out var value))
            {
                context.Messages.Add(new AssemblerMessage(ErrorLevel.Error,
                    string.Format("Unable to parse argument 1"),
                    context.ParsedInstruction.SourceLine.LineNumber));
                return null;
            }

            return new[] { value };
        }
    }
}