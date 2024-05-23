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
            foreach (string value in line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries))
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
                    tokens.Add(token);
                }
                else if (IsAddressToken(token))
                {
                    tokens.Add(token);
                }
                else
                {
                    throw new Exception("unknown token");
                }

            }
            return tokens;
        }

        private static bool IsCommentToken(string token)
        {
            return token.StartsWith("#") || token.StartsWith(";");
        }

        internal static bool IsLabelToken(string token)
        {
            return token.StartsWith(":");
        }

        private static bool IsCommandToken(string token)
        {
            return Translator.Commands.Contains(token);
        }

        internal static bool IsAddressToken(string address)
        {
            foreach(char character in address)
            {
                if (!char.IsLetter(character) && character != '_')
                {
                    return false;
                }
            }
            return !IsCommandToken(address);
        }
    }
}
