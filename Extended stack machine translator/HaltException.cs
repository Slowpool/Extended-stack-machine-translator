using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extended_stack_machine_translator
{
    internal class HaltException : Exception
    {
        internal HaltException()
        { }

        internal HaltException(string message) : base(message)
        { }
    }
}
