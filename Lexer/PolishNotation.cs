using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer
{
    class PolishNotation
    {

        public static string ToString(ArrayList lexemes)
        {
            string output = "";
            foreach (Token token in lexemes)
                output += token.Value.ToString() + " ";
            return output;
        }

        private static int operatorRank(Token token)
        {
            switch (token.Type)
            {
                case Token.T_MULT:
                case Token.T_DIVIDE:
                    return 2;
                case Token.T_PLUS:
                case Token.T_MINUS:
                    return 1;
                default:
                    return 0;
            }


        }


        private static bool IsHigherPrecedence(Token a, Token b)
        {

            if ((a.Type == Token.T_MULT || a.Type == Token.T_DIVIDE) &&
                (b.Type == Token.T_PLUS || b.Type == Token.T_MINUS))
                return true;



            //if (a.Type == Token.T_MULT)
            //    return true;
            //if (a.Type == Token.T_DIVIDE && b.Type != Token.T_MULT)
            //    return true;

            return false;

        }

        private static float PerfomOperation(float a, Token op, float b)
        {
            switch (op.Type)
            {
                case Token.T_PLUS:
                    return a + b;
                case Token.T_MINUS:
                    return b - a;
                case Token.T_MULT:
                    return a * b;
                case Token.T_DIVIDE:
                    return b / a;
            }
            return 0;
        }


        public static ArrayList InfixToPostfix(ArrayList lexemes)
        {
            ArrayList postfix = new ArrayList();
            Stack<Token> stack = new Stack<Token>();
            foreach (Token token in lexemes)
            {
                if (token.isNumber())
                    postfix.Add(token);

                if (token.Type == Token.T_LPAREN)
                    stack.Push(token);

                if (token.isOperator())
                {
                    if (stack.Count == 0)
                        stack.Push(token);
                    else
                    {
                        while (stack.Count > 0)
                        {

                            if (IsHigherPrecedence(stack.Peek(), token))
                                postfix.Add(stack.Pop());

                            else break;
                        }

                        stack.Push(token);
                    }
                }

                if (token.Type == Token.T_RPAREN)
                {
                    while (stack.Count > 0 && stack.Peek().Type != Token.T_LPAREN)
                        postfix.Add(stack.Pop());

                    stack.Pop();
                }
            }

            while (stack.Count > 0)
                postfix.Add(stack.Pop());


            return postfix;
        }


        public static float EvaluatePostfix(ArrayList postfixLexemes)
        {
            Stack<float> stack = new Stack<float>();
            foreach (Token token in postfixLexemes)
                if (token.isOperator())
                    stack.Push(PerfomOperation(stack.Pop(), token, stack.Pop()));
                else
                    stack.Push(float.Parse(token.Value.ToString()));

            return stack.Pop();

        }

        public static ArrayList InfixToPrefix(ArrayList lexemes)
        {
            ArrayList output = new ArrayList();
            Stack<Token> operatorStack = new Stack<Token>();
            Stack<Token> operandStack = new Stack<Token>();

            foreach (Token token in lexemes)
            {

                if (token.isNumber())
                    operandStack.Push(token);


                else if (token.Type == Token.T_LPAREN || operatorStack.Count == 0
                    || operatorRank(token) > operatorRank(operatorStack.Peek()))
                    operatorStack.Push(token);

                else if (token.Type == Token.T_RPAREN)
                {
                    while (operatorStack.Peek().Type != Token.T_LPAREN)
                    {
                        Token op = operatorStack.Pop();
                        Token rightOperand = operandStack.Pop();
                        Token leftOperand = operandStack.Pop();

                        Token t = new Token(Token.T_FLOAT, "" + op.Value + " " + leftOperand.Value + " " + rightOperand.Value);
                        operandStack.Push(t);
                    }
                    operatorStack.Pop();
                }



                else if (operatorRank(token) <= operatorRank(operatorStack.Peek()))
                {
                    while (operatorStack.Count > 0 && operatorRank(token) <= operatorRank(operatorStack.Peek())) {
                        Token op = operatorStack.Pop();
                        Token rightOperand = operandStack.Pop();
                        Token leftOperand = operandStack.Pop();

                        Token t = new Token(Token.T_FLOAT, "" + op.Value + " " + leftOperand.Value + " " + rightOperand.Value);
                        operandStack.Push(t);
                    }
                    operatorStack.Push(token);
                }


              

            }

            while (operatorStack.Count != 0)
            {
                Token op = operatorStack.Pop();
                Token rightOperand = operandStack.Pop();
                Token leftOperand = operandStack.Pop();

                Token t = new Token(Token.T_FLOAT, "" + op.Value + " " + leftOperand.Value + " " + rightOperand.Value);
                operandStack.Push(t);
            }

            while (operandStack.Count > 0)
                output.Add(operandStack.Pop());
            return output;
        }
    }
}
