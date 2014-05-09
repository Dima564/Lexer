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
        private static string input;



        public static void SetInput(String input)
        {
            Scanner.input = input;
            Position = 0;
        }

        private static char GetNextChar()
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

        private static char PeekNextChar()
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

        public static Token ScanOneToken()
        {
            while (Char.IsWhiteSpace(PeekNextChar()))
            {
                GetNextChar();
            }

            Token token = new Token();

            switch ((int)PeekNextChar())
            {
                case T_DIVIDE:
                case T_LPAREN:
                case T_RPAREN:
                case T_MINUS:
                case T_PLUS:
                case T_MULT:
                    token.Type = (int)PeekNextChar();
                    token.Value = (char)PeekNextChar();
                    GetNextChar();
                    break;
                    
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    String number = "";

                    while (Char.IsNumber(PeekNextChar()))
                    {
                        number += GetNextChar();
                    }

                    if (PeekNextChar().Equals('.') || PeekNextChar().Equals(','))
                    {
                        token.Type = T_FLOAT;
                        number += ',';
                        GetNextChar();

                        while (Char.IsNumber(PeekNextChar()))
                        {
                            number += GetNextChar();
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
                    token.Type = T_ERROR;
                    break;

            }

            return token;

        }


    

    }
}
