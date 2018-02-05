using EVM.Assembler.OpCodes;

namespace EVM.Assembler.OpCodeServices
{
    public class Return : OpCodeService
    {
        public Return()
            : base(OpCode.Return,
                "return",
                1,
                0)
        {
            
        }
    }
}