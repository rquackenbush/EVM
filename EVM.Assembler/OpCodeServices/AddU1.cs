using EVM.Assembler.OpCodes;

namespace EVM.Assembler.OpCodeServices
{
    public class AddU1 : OpCodeService
    {
        public AddU1()
            : base(OpCode.AddU1,
                "add_u1",
                1,
                0)
        {
        }
    }
}