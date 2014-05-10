using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer
{
    static class Scanner
    {

        private static int Position;
        private static string input;

        public static void SetInput(String input)
        {
            Scanner.input = input;
            Position = 0;
        }


        public static ArrayList GetLexemes()
        {
            ArrayList lexemes = new ArrayList();
            Token t = ScanOneToken();
            while (t.Type != Token.T_END)
            {
                lexemes.Add(t);
                t = ScanOneToken();
            }

            return lexemes;
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
            token.Value = "";

            switch ((int)PeekNextChar())
            {
                case Token.T_DIVIDE:
                case Token.T_LPAREN:
                case Token.T_RPAREN:
                case Token.T_MINUS:
                case Token.T_PLUS:
                case Token.T_MULT:
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
                        token.Type = Token.T_FLOAT;
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
                        token.Type = Token.T_INTEGER;
                        token.Value = int.Parse(number);
                    }

                    
                    break;

                case '\n':
                case '\0':
                    token.Type = Token.T_END;
                    break;
                default:
                    token.Type = Token.T_ERROR;
                    while (!"0123456789()".Contains(PeekNextChar() ) && !PeekNextChar().Equals('\0'))
                        GetNextChar();
                    token.Value = "Invalid symbol";
                    break;

            }

            return token;

        }


    

    }
}
