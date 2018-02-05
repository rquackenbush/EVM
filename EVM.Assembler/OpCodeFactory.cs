using System;
using System.Collections.Generic;
using System.Linq;

namespace EVM.Assembler.OpCodes
{
    public static class OpCodeFactory
    {
        private static readonly Lazy<Dictionary<string, OpCodeService>> _opCodeInfos;

        static OpCodeFactory()
        {
            _opCodeInfos = new Lazy<Dictionary<string, OpCodeService>>(GetOpCodeInfos);
        }

        private static Dictionary<string, OpCodeService> GetOpCodeInfos()
        {
            Type serviceType = typeof(OpCodeService);

            OpCodeService[] services = typeof(OpCodeFactory).Assembly.GetTypes()
                .Where(t => !t.IsAbstract && serviceType.IsAssignableFrom(t))
                .Select(t => (OpCodeService)Activator.CreateInstance(t))
                .ToArray();

            //Put it into a dictionary
            return services.ToDictionary(o => o.AssemblerKeyword, o => o);
        }

        public static OpCodeService GetOpCodeInfo(string assemblerKeyword)
        {
            OpCodeService opCodeInfo;

            if (_opCodeInfos.Value.TryGetValue(assemblerKeyword, out opCodeInfo))
                return opCodeInfo;

            return null;
        }
    }
}
