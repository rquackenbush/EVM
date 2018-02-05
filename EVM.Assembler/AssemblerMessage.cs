using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVM.Assembler
{
    public class AssemblerMessage
    {
        public AssemblerMessage(ErrorLevel errorLevel, string message, int? lineNumber = null)
        {
            this.ErrorLevel = errorLevel;
            this.Message = message;
            this.LineNumber = lineNumber;
        }

        public ErrorLevel ErrorLevel { get; }

        public int? LineNumber { get; }

        public string Message { get; }
    }
}
