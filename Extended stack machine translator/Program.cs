using System;
using System.Collections.Generic;
using System.IO;
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
            var translator = new Translator();
            do
            {
                Console.Write("enter file name>>");
                string fileName = Path.Combine(@"C:\Users\azgel\source\repos\Extended stack machine translator\Extended stack machine translator", Console.ReadLine());
                try
                {
                    translator.ExecuteFile(fileName);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
                //translator.ExecuteFile(@"C:\Users\azgel\Desktop\biggest common divisor.txt");
                Console.WriteLine("FINISHED");
                Console.WriteLine("exit - to exit | any key to select file again");
                Console.Write("insert command>>");
                string userAnswer = Console.ReadLine();
                if (userAnswer == "exit")
                {
                    break;
                }
            }
            while (true);
            Console.WriteLine("translator program finished");
        }
    }
}
