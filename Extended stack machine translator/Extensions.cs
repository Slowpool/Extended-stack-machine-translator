using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extended_stack_machine_translator
{
    internal static class Extensions
    {
        public static void Print(this List<string> list)
        {
            foreach(var val in list)
            {
                Debug.Write(val + " | ");
            }
            Debug.WriteLine("");
        }
    }
}
