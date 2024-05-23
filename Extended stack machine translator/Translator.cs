using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extended_stack_machine_translator
{
    internal class Translator
    {
        internal readonly List<string> commandsSequence;
        internal static Dictionary<string, int> AddressValue = new Dictionary<string, int>();
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
        //internal List<string>

        public Translator()
        {
            commandsSequence = new List<string>();
            stack = new Stack<int>();
        }

        internal void ExecuteFile(string filePath)
        {
            StreamReader reader = new StreamReader(filePath);

            // Zero traversal. Tokens selecting to list. Here compiler removes comments.
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                TokenSelector.SelectTokens(line, commandsSequence);
            }

            Debug.WriteLine("zero traversal: ");
            commandsSequence.Print();

            // First traversal. Reading of labels and writing their addresses.
            for (int i = 0; i != commandsSequence.Count; )
            {
                if (ReplaceIfLabel(i))
                {
                    continue;
                }
                else
                {
                    i++;
                }
            }

            Debug.WriteLine("first traversal: ");
            commandsSequence.Print();

            // Second traversal. Replace addreses with numbers.
            for (int i = 0; i < commandsSequence.Count; i++)
            {
                ReplaceAdress(i);
            }

            Debug.WriteLine("second traversal: ");
            commandsSequence.Print();

            // Second traversal. Executing
            //foreach (string line in lines)
            //{
            //    for(int pointer = 0; pointer < commandsSequence.Count; pointer++)
            //    {
            //        ExecuteToken(commandsSequence[pointer], ref pointer);
            //    }
            //    commandsSequence.Clear();
            //}
            Reset();
        }

        private void ReplaceAdress(int i)
        {

        }

        private bool ReplaceIfLabel(int i)
        {
            if (TokenSelector.IsLabelToken(commandsSequence[i]))
            {
                // because label in code is :some_label should to remove :
                string labelName = commandsSequence[i].Substring(1);
                AddressValue[labelName] = i;
                commandsSequence.RemoveAt(i);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ExecuteToken(string token, ref int pointer)
        {
            if (Commands.Contains(token))
            {
                ExecuteCommand(token);
            }
        }

        private void ExecuteCommand(string comamnd)
        {
            switch (comamnd)
            {
                case "DUP":
                    stack.Push(stack.Peek());
                    break;
                case "DROP":
                    stack.Pop();
                    break;
                case "SWAP":
                    {
                        int temp = stack.Pop();
                        int temp2 = stack.Pop();
                        stack.Push(temp);
                        stack.Push(temp2);
                        break;
                    }
                case "OVER":
                    {
                        int temp = stack.Pop();
                        int temp2 = stack.Peek();
                        stack.Push(temp);
                        stack.Push(temp2);
                        break;
                    }
                case "ADD":
                    {
                        int sum = stack.Pop() + stack.Pop();
                        stack.Push(sum);
                        break;
                    }
                case "SUB":
                    {
                        int difference = -stack.Pop() + stack.Pop();
                        stack.Push(difference);
                        break;
                    }
                case "MUL":
                    {
                        int product = stack.Pop() * stack.Pop();
                        stack.Push(product);
                        break;
                    }
                case "DIV":
                    {
                        int divisor = stack.Pop();
                        int divisible = stack.Pop();
                        int remainder = divisible % divisor;
                        int quotient = divisible / divisor;
                        stack.Push(remainder);
                        stack.Push(quotient);
                        break;
                    }
                case "NEG":
                    stack.Push(-stack.Pop());
                    break;
                case "ABS":
                    stack.Push(Math.Abs(stack.Pop()));
                    break;
                case "AND":
                    {
                        int number1 = stack.Pop();
                        int number2 = stack.Pop();
                        int result = number1 & number2;
                        stack.Push(result);
                        break;
                    }
                case "OR":
                    {
                        int number1 = stack.Pop();
                        int number2 = stack.Pop();
                        int result = number1 | number2;
                        stack.Push(result);
                        break;
                    }
                case "XOR":
                    {
                        int number1 = stack.Pop();
                        int number2 = stack.Pop();
                        int result = number1 ^ number2;
                        stack.Push(result);
                        break;
                    }
                case "SHL":
                    stack.Push(stack.Pop() * 2);
                    break;
                case "SHR":
                    stack.Push(stack.Pop() / 2);
                    break;
                case "BR":

                    break;
                case "BRN":

                    break;
                case "BRZ":

                    break;
                case "BRP":

                    break;
                case "SAVE":

                    break;
                case "LOAD":

                    break;
                case "NOP":

                    break;
                case "IN":

                    break;
                case "OUTN":

                    break;
                case "OUTC":

                    break;
                case "HALT":

                    break;
                case "LPC":

                    break;
                case "DEPTH":

                    break;
                case "CALL":

                    break;
                case "RET":

                    break;
                case "DTR":

                    break;
                case "RTD":

                    break;
            }
        }

        internal void Reset()
        {
            commandsSequence.Clear();
            AddressValue.Clear();
        }
    }
}
