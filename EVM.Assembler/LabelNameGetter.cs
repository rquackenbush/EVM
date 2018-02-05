namespace EVM.Assembler
{
    public static class LabelNameGetter
    {
        public static string GetLabelName(string labelIdentifier)
        {
            if (string.IsNullOrWhiteSpace(labelIdentifier))
                return null;

            return labelIdentifier.Replace(":", "");

        }
    }
}
