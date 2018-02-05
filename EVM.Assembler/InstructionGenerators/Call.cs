namespace EVM.Assembler.InstructionGenerators
{
    public class Call : IInstructionGenerator
    {
        public byte[] GenerateInstruction(FinalAssembleContext context)
        {
            var labelName = LabelNameGetter.GetLabelName(context.ParsedInstruction.Arguments[0]);

            if (string.IsNullOrWhiteSpace(labelName))
            {
                context.Messages.Add(new AssemblerMessage(ErrorLevel.Error,
                                    string.Format("Invalid label argument - argument 1"),
                                    context.ParsedInstruction.SourceLine.LineNumber));

                return null;
            }

            //Attempt to get the address
            byte? address = context.GetLabelAddress(labelName);

            if (address == null)
            {
                context.Messages.Add(new AssemblerMessage(ErrorLevel.Error,
                    string.Format("Unable to find label '{0}'", labelName),
                    context.ParsedInstruction.SourceLine.LineNumber));

                return null;
            }

            return new[] { address.Value };
        }
    }
}
