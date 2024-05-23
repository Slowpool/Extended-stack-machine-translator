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
        internal static Dictionary<string, int> AddressValue;
        internal static Dictionary<int, int> RAM;
        private int MemoryCellIndex { get; set; }
        internal static readonly string[] Commands = new string[] {
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
        private readonly Stack<int> DataStack;
        private readonly Stack<int> ReturnStack;
        private int PC { get; set; }

        public Translator()
        {
            commandsSequence = new List<string>();
            AddressValue = new Dictionary<string, int>();
            RAM = new Dictionary<int, int>();
            DataStack = new Stack<int>();
            ReturnStack = new Stack<int>();
            Reset();
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
            for (int i = 0; i != commandsSequence.Count;)
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
                if (TokenSelector.IsAddressToken(commandsSequence[i]))
                {
                    ReplaceAdress(i);
                }
            }

            Debug.WriteLine("second traversal: ");
            commandsSequence.Print();

            // Third traversal. Executing
            while ( PC < commandsSequence.Count)
            {
                Execute(commandsSequence[PC]);
            }

            Reset();
        }

        private void ReplaceAdress(int i)
        {
            string currentAddress = commandsSequence[i];
            if (AddressValue.ContainsKey(currentAddress))
            {
                commandsSequence[i] = AddressValue[currentAddress].ToString();
            }
            else
            {
                AddressValue[currentAddress] = MemoryCellIndex++;
                commandsSequence[i] = AddressValue[currentAddress].ToString();
            }
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

        private void Execute(string token)
        {
            if (Commands.Contains(token))
            {
                ExecuteCommand(token);
            }
            else if (IsNumber(token))
            {
                DataStack.Push(int.Parse(token));
            }
        }

        private bool IsNumber(string token)
        {
            foreach(char character in token)
            {
                if (!char.IsNumber(character))
                {
                    return false;
                }
            }
            return true;
        }

        private void ExecuteCommand(string comamnd)
        {
            switch (comamnd)
            {
                case "DUP":
                    DataStack.Push(DataStack.Peek());
                    break;
                case "DROP":
                    DataStack.Pop();
                    break;
                case "SWAP":
                    {
                        int temp = DataStack.Pop();
                        int temp2 = DataStack.Pop();
                        DataStack.Push(temp);
                        DataStack.Push(temp2);
                        break;
                    }
                case "OVER":
                    {
                        int temp = DataStack.Pop();
                        int temp2 = DataStack.Peek();
                        DataStack.Push(temp);
                        DataStack.Push(temp2);
                        break;
                    }
                case "ADD":
                    {
                        int sum = DataStack.Pop() + DataStack.Pop();
                        DataStack.Push(sum);
                        break;
                    }
                case "SUB":
                    {
                        int difference = -DataStack.Pop() + DataStack.Pop();
                        DataStack.Push(difference);
                        break;
                    }
                case "MUL":
                    {
                        int product = DataStack.Pop() * DataStack.Pop();
                        DataStack.Push(product);
                        break;
                    }
                case "DIV":
                    {
                        int divisor = DataStack.Pop();
                        int divisible = DataStack.Pop();
                        int remainder = divisible % divisor;
                        int quotient = divisible / divisor;
                        DataStack.Push(remainder);
                        DataStack.Push(quotient);
                        break;
                    }
                case "NEG":
                    DataStack.Push(-DataStack.Pop());
                    break;
                case "ABS":
                    DataStack.Push(Math.Abs(DataStack.Pop()));
                    break;
                case "AND":
                    {
                        int number1 = DataStack.Pop();
                        int number2 = DataStack.Pop();
                        int result = number1 & number2;
                        DataStack.Push(result);
                        break;
                    }
                case "OR":
                    {
                        int number1 = DataStack.Pop();
                        int number2 = DataStack.Pop();
                        int result = number1 | number2;
                        DataStack.Push(result);
                        break;
                    }
                case "XOR":
                    {
                        int number1 = DataStack.Pop();
                        int number2 = DataStack.Pop();
                        int result = number1 ^ number2;
                        DataStack.Push(result);
                        break;
                    }
                case "SHL":
                    DataStack.Push(DataStack.Pop() * 2);
                    break;
                case "SHR":
                    DataStack.Push(DataStack.Pop() / 2);
                    break;
                case "BR":
                    PC = DataStack.Pop();
                    return;
                case "BRN":
                    {
                        int address = DataStack.Pop();
                        int flag = DataStack.Pop();
                        if (flag < 0)
                        {
                            PC = address;
                        }
                        return;
                    }
                case "BRZ":
                    {
                        int address = DataStack.Pop();
                        int flag = DataStack.Pop();
                        if (flag == 0)
                        {
                            PC = address;
                        }
                        return;
                    }
                case "BRP":
                    {
                        int address = DataStack.Pop();
                        int flag = DataStack.Pop();
                        if (flag > 0)
                        {
                            PC = address;
                        }
                        return;
                    }
                case "SAVE":
                    {
                        int address = DataStack.Pop();
                        int number = DataStack.Pop();
                        RAM[address] = number;
                        break;
                    }
                case "LOAD":
                    {
                        int address = DataStack.Pop();
                        DataStack.Push(RAM[address]);
                        break;
                    }
                case "NOP":
                    break;
                case "IN":
                    Console.Write("enter value>>");
                    DataStack.Push(int.Parse(Console.ReadLine()));
                    break;
                case "OUTN":
                    Console.WriteLine(DataStack.Pop());
                    break;
                case "OUTC":
                    Console.WriteLine((char)DataStack.Pop());
                    break;
                case "HALT":
                    PC = commandsSequence.Count;
                    return;
                case "LPC":
                    DataStack.Push(PC);
                    break;
                case "DEPTH":
                    DataStack.Push(DataStack.Count());
                    break;
                case "CALL":
                    ReturnStack.Push(PC + 1);
                    PC = DataStack.Pop();
                    return;
                case "RET":
                    PC = ReturnStack.Pop();
                    return;
                case "DTR":
                    ReturnStack.Push(DataStack.Pop());
                    break;
                case "RTD":
                    DataStack.Push(ReturnStack.Pop());
                    break;
            }
            PC++;
        }

        internal void Reset()
        {
            commandsSequence.Clear();
            AddressValue.Clear();
            DataStack.Clear();
            ReturnStack.Clear();
            PC = 0;
            MemoryCellIndex = 0;
        }
    }
}
