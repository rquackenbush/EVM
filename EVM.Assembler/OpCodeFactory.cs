using EVM.Assembler.InstructionGenerators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EVM.Assembler
{
    public static class OpCodeFactory
    {
        private static readonly Lazy<Dictionary<string, OpCodeInfo>> _opCodeInfos;

        static OpCodeFactory()
        {
            _opCodeInfos = new Lazy<Dictionary<string, OpCodeInfo>>(GetOpCodeInfos);
        }

        private static Dictionary<string, OpCodeInfo> GetOpCodeInfos()
        {
            var infos = new OpCodeInfo[]
            {
                new OpCodeInfo(0, "push_1", 2, 1, new Push1()),
                new OpCodeInfo(1, "add_u1", 1, 0),
                new OpCodeInfo(2, "write_u1", 1, 0),
                new OpCodeInfo(3, "call", 2, 1, new Call()),
                new OpCodeInfo(4, "return", 1, 0)
            };

            //Put it into a dictionary
            return infos.ToDictionary(o => o.AssemblerKeyword, o => o);
        }

        public static OpCodeInfo GetOpCodeInfo(string assemblerKeyword)
        {
            OpCodeInfo opCodeInfo;

            if (_opCodeInfos.Value.TryGetValue(assemblerKeyword, out opCodeInfo))
                return opCodeInfo;

            return null;
        }
    }
}
