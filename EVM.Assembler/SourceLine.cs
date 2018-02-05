namespace EVM.Assembler
{
    public class SourceLine
    {
        public SourceLine(int lineNumber, string line)
        {
            this.LineNumber = lineNumber;
            this.Line = line;
        }

        public int LineNumber { get; }

        /// <summary>
        /// This is the original line of source code
        /// </summary>
        public string Line { get; }
    }
}
