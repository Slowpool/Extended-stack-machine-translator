﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Extended_stack_machine_translator
{
    internal class Program
    {
        static void Main()
        {
            var interpreter = new Interpreter();
            do
            {
                Console.Write("enter full file path>>");
                //interpreter.ExecuteFile(Console.ReadLine());
                interpreter.ExecuteFile(@"C:\Users\azgel\Desktop\biggest common divisor.txt");
                interpreter.Reset();
                Console.WriteLine("interpreting finished");
                Console.WriteLine("e - exit | c - choose file | r - repeat");
                Console.Write("insert command>>");
            }
            while (Console.ReadLine() != "exit");
            Console.WriteLine("translator program finished");
        }
    }
}
