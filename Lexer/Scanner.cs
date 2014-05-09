using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer
{
    static class Scanner
    {

        public struct Token
        {
            public int Type;
            public Object Value;
            
        }

        public const int T_LPAREN = '(';
        public const int T_RPAREN = ')';
        public const int T_PLUS = '+';
        public const int T_MINUS = '-';
        public const int T_MULT = '*';
        public const int T_DIVIDE = '/';
        public const int T_WHITESPACE = ' ';

        public const int T_INTEGER = 1;
        public const int T_FLOAT = 2;
        public const int T_END = 3;
        public const int T_ERROR = 4;

        private static int Position;




        public static void PrintLexemes(String input)
        {
            Position = 0;
        }

        private static char GetNextChar(String input)
        {
            if (Position < input.Length)
            {
                char ch = input[Position];
                Position++;
                return ch;
            }
            else
            {
                return '\0';
            }
        }

        private static char PeekNextChar(String input)
        {
            if (Position < input.Length)
            {
                return input[Position];
            }
            else
            {
                return '\0';
            }
        }

        public static Token ScanOneToken(String input)
        {
            char ch, nextch;
            ch = GetNextChar(input);
            while (Char.IsWhiteSpace(ch))
            {
                ch = GetNextChar(input);
            }

            Token token = new Token();

            switch ((int)ch)
            {
                case T_DIVIDE:
                case T_LPAREN:
                case T_RPAREN:
                case T_MINUS:
                case T_PLUS:
                case T_MULT:
                    token.Type = (int)ch;
                    token.Value = (char)ch;
                    break;
                    
                case '0':
                case '1':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    String number = "";
                    while (Char.IsNumber(ch))
                    {
                        number += ch.ToString();
                        ch = GetNextChar(input);
                    }

                    if (ch.Equals('.') || ch.Equals(','))
                    {
                        token.Type = T_FLOAT;
                        number += ',';
                        ch = GetNextChar(input);

                        while (Char.IsNumber(ch))
                        {
                            number += ch.ToString();
                            ch = GetNextChar(input);
                        }

                        token.Value = float.Parse(number);

                    }
                    else
                    {
                        token.Type = T_INTEGER;
                        token.Value = int.Parse(number);
                    }
                    break;

                case '\n':
                case '\0':
                    token.Type = T_END;
                    break;
                default:
                    Console.WriteLine("Error here: " + ch);
                    token.Type = T_ERROR;
                    break;

            }

            return token;

        }


    

    }
}
