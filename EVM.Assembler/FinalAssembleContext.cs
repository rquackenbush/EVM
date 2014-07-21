using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVM.Assembler
{
    public class FinalAssembleContext
    {
        private readonly IDictionary<string, byte> _labels;
        private readonly IList<AssemblerMessage> _messages;
        private readonly ParsedInstruction _parsedInstruction;
        
        public FinalAssembleContext(IDictionary<string, byte> labels, 
            IList<AssemblerMessage> messages,
            ParsedInstruction parsedInstruction)
        {
            if (labels == null)
                throw new ArgumentNullException("labels");

            if (messages == null)
                throw new ArgumentNullException("messages");

            if (parsedInstruction == null)
                throw new ArgumentNullException("parsedInstruction");

            _labels = labels;
            _messages = messages;
            _parsedInstruction = parsedInstruction;
        }

        /// <summary>
        /// Returns the address if the label was found, null otherwise.
        /// </summary>
        /// <param name="labelName"></param>
        /// <returns></returns>
        public byte? GetLabelAddress(string labelName)
        {
            byte value;

            if (_labels.TryGetValue(labelName, out value))
                return value;

            return null;
        }

        public IList<AssemblerMessage> Messages
        {
            get { return _messages; }
        }

        public ParsedInstruction ParsedInstruction
        {
            get { return _parsedInstruction; }
        }

    }
}
