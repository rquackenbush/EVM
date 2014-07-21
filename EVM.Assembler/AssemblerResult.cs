using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVM.Assembler
{
    public class AssemblerResult
    {
        public AssemblerMessage[] Messages { get; set; }

        public bool Succeeded { get; set; }
    }
}
