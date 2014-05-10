using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer
{
    class Token
    {

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

        public int Type;
        public Object Value;
    }
}
