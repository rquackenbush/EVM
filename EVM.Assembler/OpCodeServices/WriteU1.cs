using EVM.Assembler.OpCodes;

namespace EVM.Assembler.OpCodeServices
{
    public class WriteU1 : OpCodeService
    {
        public WriteU1()
            : base(OpCodes.OpCode.WriteU1,
                "write_u1",
                1,
                0)
        {
        }
    }
}