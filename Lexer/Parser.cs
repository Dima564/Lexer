using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer
{
    class Parser
    {
        private static ArrayList lexemes;
        private static int Position;

        public static void InitParser(ArrayList lexemes) {
            Parser.lexemes = lexemes;
            Position = 0;
        }

        private static bool End()
        {
            return Position >= lexemes.Count;
        }


        private static bool Terminal(int TockenT)
        {
            if (End()) return false;
            bool match = ((Token)lexemes[Position]).Type == TockenT;
            Position++;
            return match;
        }
        public static bool Factor()
        {
            int save = Position;
            bool match = Terminal(Token.T_INTEGER);
            if (!match)
            {
                Position = save;
                match = Terminal(Token.T_FLOAT);

                if (!match)
                {
                    Position = save;
                    match = Terminal(Token.T_LPAREN) && SignedExpression() && Terminal(Token.T_RPAREN);
                }
            }
            return match;
        }

        public static bool Term()
        {
            int save = Position;
            bool match = Factor() && Terminal(Token.T_MULT) && Term();
            if (match) return true;

            Position = save;
            match = Factor() && Terminal(Token.T_DIVIDE) && Term();
            if (match) return true;
            Position = save;
            match = Factor();
            return match;
        }

        private static bool Expression()
        {
            int save = Position;
            bool match = Term() && Terminal(Token.T_PLUS) && Expression();
            if (!match)
            {
                Position = save;
                match = Term() && Terminal(Token.T_MINUS) && Expression();
                if (!match)
                {
                    Position = save;
                    match = Term();
                }
            }
            return match;
        }

        private static bool SignedExpression()
        {
            int save = Position;
            bool match = Terminal(Token.T_MINUS) && Expression();
            if (match) return true;
            Position = save;
            match = Terminal(Token.T_PLUS) && Expression();
            if (match) return true;
            Position = save;
            match = Expression();
            return match;

            
        }


        public static bool Match()
        {
            if (lexemes.Count <= 0) return false;
            bool match = SignedExpression();
            if (Position != lexemes.Count) return false;
            return match;
        }

        
    }
}
