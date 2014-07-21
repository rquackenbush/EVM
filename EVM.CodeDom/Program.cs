using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVM.CodeDom
{
    public class Program
    {
        public List<Variable> GlobalVariables { get; set; }

        public Function EntryPoint { get; set; }

        public List<Function> Functions { get; set; }
    }
}
