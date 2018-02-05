using System.Collections.Generic;

namespace EVM.CodeDom
{
    public class Program
    {
        public List<Variable> GlobalVariables { get; set; }

        public Function EntryPoint { get; set; }

        public List<Function> Functions { get; set; }
    }
}
