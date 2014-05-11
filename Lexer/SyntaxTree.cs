using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lexer
{
    class SyntaxTree
    {
        
        private ArrayList lexemes;
        private Node Root;
        private int Position = 0;

        public class Node
        {
            public String Content;
            public ArrayList Children;
            public Node(String C)
            {
                Content = C;
                Children = new ArrayList();
            }
        }

        public SyntaxTree(ArrayList lexemes)
        {
            this.lexemes = lexemes;
            Root = Parse();
        }

        public Node GetRoot() {
            return Root;
        }

     

        private bool End()
        {
            return Position >= lexemes.Count;
        }


        private Node Terminal(int TockenT)
        {
            if (End()) return null;
            Node node = null;
            Token token = (Token)lexemes[Position];
            if (token.Type == TockenT)
            {
                switch (TockenT)
                {
                    case Token.T_INTEGER:
                    case Token.T_FLOAT:
                        node = new Node(token.Value.ToString());
                        break;
                    default:
                        node = new Node(((char)token.Type).ToString());
                        break;
                }
            }
            Position++;
            return node;
        }


        private Node Factor()
        {
            int save = Position;
            Node node = new Node("factor");

            Node num = Terminal(Token.T_INTEGER);
            if (num != null) {
                node.Children.Add(num);
                return node;
            }

            Position = save;
            num = Terminal(Token.T_FLOAT);
            if (num != null) {
                node.Children.Add(num);
                return node;
            }
             
            Position = save;
            Node lparen = null;
            Node signedExpr = null;
            Node rparen = null;

            lparen = Terminal(Token.T_LPAREN);
            if (lparen != null)
            {
                signedExpr = SignedExpression();
                if(signedExpr != null) 
                {
                    rparen = Terminal(Token.T_RPAREN);
                    if(rparen != null)
                    {
                        node.Children.Add(lparen);
                        node.Children.Add(signedExpr);
                        node.Children.Add(rparen);
                        return node;
                    }
                }
            }
 
            return null;
        }

        private Node Term()
        {

            int save = Position;
            Node node = new Node("term");

            Node factor = null;
            Node sign = null;
            Node term = null;
            factor = Factor();
            if (factor != null)
            {
                sign = Terminal(Token.T_MULT);
                if (sign != null) 
                {
                    term = Term();
                    if(term != null)
                    {
                        node.Children.Add(factor);
                        node.Children.Add(sign);
                        node.Children.Add(term);
                        return node;
                    }
                }
            }


            Position = save;
            factor = Factor();
            if (factor != null)
            {
                sign = Terminal(Token.T_DIVIDE);
                if (sign != null)
                {
                    term = Term();
                    if (term != null)
                    {
                        node.Children.Add(factor);
                        node.Children.Add(sign);
                        node.Children.Add(term);
                        return node;
                    }
                }
            }

            Position = save;
            factor = Factor();
            if (factor != null)
            {
                node.Children.Add(factor);
                return node;
            }

            return null;
        }

        private Node Expression()
        {

            int save = Position;
            Node node = new Node("expr");

            Node term = null;
            Node sign = null;
            Node expr = null;


            term = Term();
            if (term != null)
            {
                sign = Terminal(Token.T_PLUS);
                if (sign != null)
                {
                    expr = Expression();
                    if (expr != null)
                    {
                        node.Children.Add(term);
                        node.Children.Add(sign);
                        node.Children.Add(expr);
                        return node;
                    }
                }
            }


            Position = save;
            term = Term();
            if (term != null)
            {
                sign = Terminal(Token.T_MINUS);
                if (sign != null)
                {
                    expr = Expression();
                    if (expr != null)
                    {
                        node.Children.Add(term);
                        node.Children.Add(sign);
                        node.Children.Add(expr);
                        return node;
                    }
                }
            }

            Position = save;
            term = Term();
            if (term != null)
            {
                node.Children.Add(term);
                return node;
            }

            return null;
        }

        private Node SignedExpression()
        {
            int save = Position;
            Node node = new Node("sExpr");

            Node sign = null;
            Node expr = null;

            sign = Terminal(Token.T_MINUS);
            if (sign != null)
            {
                expr = Expression();
                if (expr != null)
                {
                    node.Children.Add(sign);
                    node.Children.Add(expr);
                    return node;
                }
            }

            Position = save;
            sign = Terminal(Token.T_PLUS);
            if (sign != null)
            {
                expr = Expression();
                if ( expr != null)
                {
                    node.Children.Add(sign);
                    node.Children.Add(expr);
                    return node;
                }
            }

            Position = save;
            expr = Expression();
            if (expr != null)
            {
                node.Children.Add(expr);
                return node;
            }

            return null;
        }


        private Node Parse()
        {
            if (lexemes.Count <= 0) return null;
            Node node = SignedExpression();
            if (Position != lexemes.Count) return null ;
            return node;
        }

        private void ConsoleTraverse(Node n, int level)
        {

            
            int pos = 0;
            for (; pos < n.Children.Count/2; pos++)
                ConsoleTraverse((Node)n.Children[pos],level+1);

            for (int i = 0; i < level; i++)
                Console.Write("\t");
            Console.WriteLine(n.Content);

            for (; pos < n.Children.Count; pos++)
                ConsoleTraverse((Node)n.Children[pos],level+1);
            
        }

        public void Traverse()
        {
            ConsoleTraverse(Root,0);
        }




     



    }
}
