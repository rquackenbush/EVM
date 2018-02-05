using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EVM.Assembler.OpCodes;

namespace EVM.Assembler
{
    public class EvmAssembler
    {
        public AssemblerResult Assemble(AssemblerOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            
            List<AssemblerMessage> messages = new List<AssemblerMessage>();
            
            try
            {
                //Get the lines
                var sourceLines = GetSourceLines(options);

                //Do the first pass
                var pass1Result = Pass1(messages, sourceLines);

                if (messages.All(m => m.ErrorLevel != ErrorLevel.Error))
                {
                    Pass2(messages, pass1Result, options.OutputFile);
                }
            }
            catch (Exception ex)
            {
                messages.Add(new AssemblerMessage(ErrorLevel.Error, ex.Message));
            }

            //We're done here
            return new AssemblerResult()
            {
                Messages = messages.ToArray(),
                Succeeded = messages.All(m => m.ErrorLevel != ErrorLevel.Error)
            };
        }

        /// <summary>
        /// Gets the lines of source code. Ignores all empty lines and comments.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        private SourceLine[] GetSourceLines(AssemblerOptions options)
        {
            List<SourceLine> sourceLines = new List<SourceLine>();

            var lineNumber = 1;

            using (var sourceReader = new StreamReader(options.SourceFile))
            {
                var line = sourceReader.ReadLine();

                while (line != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        //Do nothing
                    }
                    else
                    {
                        if (!line.TrimStart().StartsWith("#"))
                        {
                            sourceLines.Add(new SourceLine(lineNumber, line));    
                        }
                    }

                    lineNumber++;
                    line = sourceReader.ReadLine();
                }
            }

            return sourceLines.ToArray();
        }

        private Pass1Result Pass1(IList<AssemblerMessage> messages, SourceLine[] sourceLines)
        {
            if (messages == null)
                throw new NullReferenceException("messages");

            if (sourceLines == null)
                throw new NullReferenceException("sourceLines");

            byte currentLocation = 0;
            
            var parsedInstructions = new List<ParsedInstruction>(sourceLines.Length);

            Dictionary<string, byte> labels = new Dictionary<string, byte>();

            foreach (var sourceLine in sourceLines)
            {
                //Ditch the whitespace
                var cleanedLine = sourceLine.Line.RemoveWhitespace();

                if (cleanedLine.StartsWith(":"))
                {
                    //This is a label
                    string labelName = LabelNameGetter.GetLabelName(cleanedLine);

                    if (string.IsNullOrWhiteSpace(labelName))
                    {
                        messages.Add(new AssemblerMessage(ErrorLevel.Error,
                            $"Invalid label  on line {sourceLine.LineNumber}", sourceLine.LineNumber));
                    }
                    else
                    {
                        labels.Add(labelName, currentLocation);    
                    }
                }
                else
                {
                    //This out to be a bloody instruction
                    var parts = cleanedLine.Split(',');

                    //This is the number of arguments specified
                    var numberOfArgumentsSpecified = parts.Length - 1;

                    //Get the instruction name
                    string instructionName = parts[0];

                    //Attempt to get the op code info
                    var opCodeInfo = OpCodeFactory.GetOpCodeInfo(parts[0]);

                    //Check to see
                    if (opCodeInfo == null)
                    {
                        messages.Add(new AssemblerMessage(ErrorLevel.Error,
                            $"Invalid instruction '{instructionName}' on line {sourceLine.LineNumber}", sourceLine.LineNumber));
                    }
                    else
                    {
                        if (numberOfArgumentsSpecified != opCodeInfo.NumberOfArguments)
                        {
                            messages.Add(new AssemblerMessage(ErrorLevel.Error,
                                $"The instruction '{opCodeInfo.AssemblerKeyword}' takes {opCodeInfo.NumberOfArguments} parameters but {numberOfArgumentsSpecified} were specified."));
                        }
                        else
                        {
                            //Get the arguments
                            var arguments = parts.Skip(1).ToArray();

                            //Create the parsed instruction
                            var parsedInstruction = new ParsedInstruction(sourceLine, arguments, opCodeInfo);

                            parsedInstructions.Add(parsedInstruction);

                            //Move the current location so that labels can be correctly set.
                            currentLocation += opCodeInfo.InstructionSize;
                        }
                    }
                }
            }

            return new Pass1Result(labels, parsedInstructions.ToArray());
        }

        private void Pass2(IList<AssemblerMessage> messages, Pass1Result pass1Result, string outputFile)
        {
            //If we run into an error, don't bother continuing to write the file
            bool stopWriting = false;

            using (var output = new MemoryStream())
            {
                //And the final phase!!!! Let's spit out a program
                foreach (var parsedInstruction in pass1Result.ParsedInstructions)
                {
                    var bytes = GetInstructionBytes(messages, pass1Result.Labels, parsedInstruction);

                    if (bytes == null)
                    {
                        stopWriting = true;
                    } 

                    if (!stopWriting)
                    {
                        output.Write(bytes, 0, bytes.Length);
                    }
                }

                //Only write the output file if we have done it
                if (messages.All(m => m.ErrorLevel != ErrorLevel.Error))
                {
                    File.WriteAllBytes(outputFile, output.ToArray());
                }
            }
           
        }

        private byte[] GetInstructionBytes(IList<AssemblerMessage> messages, IDictionary<string, byte> labels, ParsedInstruction parsedInstruction)
        {
            if (parsedInstruction.OpCodeInfo.InstructionSize > 1)
            {
                var context = new FinalAssembleContext(labels, messages, parsedInstruction);

                var argumentBytes = parsedInstruction.OpCodeInfo.GenerateInstruction(context);

                if (argumentBytes == null)
                    return null;

                if (argumentBytes.Length != parsedInstruction.OpCodeInfo.InstructionSize - 1)
                {
                    messages.Add(new AssemblerMessage(ErrorLevel.Error, string.Format("The number of bytes produced does not match the instruction size.")));
                    return null;
                }

                return new byte[] {(byte) parsedInstruction.OpCodeInfo.OpCode }.Concat(argumentBytes).ToArray();
            }

            return new byte[] { (byte)parsedInstruction.OpCodeInfo.OpCode};
        }

    }
}
