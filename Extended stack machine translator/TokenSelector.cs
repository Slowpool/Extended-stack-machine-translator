using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extended_stack_machine_translator
{
    internal static class TokenSelector
    {
        internal static List<string> SelectTokens(string line, List<string> tokens)
        {
            string token;
            foreach (string value in line.Split())
            {
                token = value.ToUpper();
                if (IsCommentToken(token))
                {
                    break;
                }
                else if (IsCommandToken(token))
                {
                    tokens.Add(token);
                }
                else if (IsLabelToken(token))
                {
                    throw new Exception("prematurelabel token");
                }
                else if (IsAddressToken(token))
                {
                    tokens.Add(token);
                }
                else
                {
                    //throw new Exception("unknown token");
                }

            }
            return tokens;
        }

        private static bool IsCommentToken(string token)
        {
            return token.StartsWith("#") || token.StartsWith(";");
        }

        private static bool IsLabelToken(string token)
        {
#warning not implemented
            return false;
        }

        private static bool IsCommandToken(string token)
        {
            return Interpreter.Commands.Contains(token);
        }

        private static bool IsAddressToken(string token)
        {
            return Interpreter.AddresValue.ContainsKey(token);
        }
    }
}
