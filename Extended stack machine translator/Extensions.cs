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
            for(int i = 0; i < list.Count; i++)
            {
                Debug.WriteLine("{0}| {1}", i, list[i]);
            }
        }
    }
}
