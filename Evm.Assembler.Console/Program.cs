﻿using EVM.Assembler;

namespace Evm.Assembler.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new AssemblerOptions()
            {
                SourceFile = "HelloWorld.asm",
                OutputFile = @"..\..\..\Debug\HelloWorld.evm"
            };

            var assembler = new EvmAssembler();

            var result = assembler.Assemble(options);

            foreach (var message in result.Messages)
            {
                System.Console.WriteLine("{0}:{1} {2}", 
                    message.ErrorLevel, 
                    message.LineNumber == null ? "" : $"Line [{message.LineNumber}]",
                    message.Message);
            }

            if (result.Succeeded)
            {
                System.Console.WriteLine("Succeeded");
            }
            else
            {
                System.Console.WriteLine("Failed.");
            }

            System.Console.ReadKey();
        }
    }
}
