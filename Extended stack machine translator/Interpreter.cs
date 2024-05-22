using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extended_stack_machine_translator
{
    internal class Interpreter
    {
        private readonly List<string> tokens;
        internal static Dictionary<string, int> AddresValue = new Dictionary<string, int>();
        internal static string[] Commands = new string[] {
            "DUP",
            "DROP",
            "SWAP",
            "OVER",
            "ADD",
            "SUB",
            "MUL",
            "DIV",
            "NEG",
            "ABS",
            "AND",
            "OR",
            "XOR",
            "SHL",
            "SHR",
            "BR",
            "BRN",
            "BRZ",
            "BRP",
            "SAVE",
            "LOAD",
            "NOP",
            "IN",
            "OUTN",
            "OUTC",
            "HALT",
            "LPC",
            "DEPTH",
            "CALL",
            "RET",
            "DTR",
            "RTD"
        };
        private readonly Stack<int> stack;

        public Interpreter()
        {
            tokens = new List<string>();
            stack = new Stack<int>();
        }
        internal void ExecuteFile(string filePath)
        {
            StreamReader reader = new StreamReader(filePath);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                TokenSelector.SelectTokens(line, tokens);
                foreach (string token in tokens)
                {
                    ExecuteToken(token);
                }
                tokens.Clear();
            }
        }

        private void ExecuteToken(string token)
        {
            Console.WriteLine("token " + token + " was executed.");
        }

        internal void Reset()
        {
            tokens.Clear();
            AddresValue.Clear();
        }
    }
}
