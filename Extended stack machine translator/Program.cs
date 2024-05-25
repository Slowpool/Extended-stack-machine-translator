using Extended_stack_machine_translator.Properties;
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
        private static List<FileInfo> Files = new List<FileInfo>();

        static void Main()
        {
            FillNumberFileMapping();
            var translator = new Translator();
            do
            {
                Console.Clear();
                PrintListOfPrograms();
                Console.Write("choose program>>");
                string fileName;

                try
                {
                    fileName = Files[int.Parse(Console.ReadLine())].FullName;
                }
                catch
                {
                    Console.WriteLine("incorrect number, try again");
                    continue;
                }

                try
                {
                    translator.ExecuteFile(fileName);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }

                Console.WriteLine("Press any key to choose file...");
                Console.ReadKey();
            }
            while (true);
            //Console.WriteLine("translator program finished");
        }

        private static void FillNumberFileMapping()
        {
            var files = new DirectoryInfo(Environment.CurrentDirectory)
                            .GetFiles()
                            .Where(file => file.Extension == ".txt");
            int i = 0;
            foreach (var file in files)
            {
                Files.Add(file);
            }
        }

        private static void PrintListOfPrograms()
        {
            for (int i = 0; i < Files.Count; i++)
            {
                Console.WriteLine("{0} {1}", i, Files[i]);
            }
        }
    }
}
