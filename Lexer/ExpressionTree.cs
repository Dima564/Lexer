using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer
{
    class ExpressionTree
    {
        SyntaxTree.Node Root;
        public ExpressionTree()
        {
            //this.Root = Root;
        }

        public void RemoveGrammarNotation(SyntaxTree.Node n)
        {
            if (n.Children.Count == 0)
            {

            } else if (n.Children.Count == 1) {
                RemoveGrammarNotation(n.Children[0] as SyntaxTree.Node);
                n.Content = (n.Children[0] as SyntaxTree.Node).Content;

                SyntaxTree.Node Child = n.Children[0] as SyntaxTree.Node;

                foreach (SyntaxTree.Node grandChild in Child.Children) {
                    n.Children.Add(grandChild);
                }

                n.Children.RemoveAt(0);
                
            } else if (n.Children.Count == 2) {
                //throw new Exception("Two children in SyntaxTree? ");
                if (((SyntaxTree.Node)n.Children[0]).Content == "-")
                {

                    n.Children.RemoveAt(0);
                    RemoveGrammarNotation(n.Children[0] as SyntaxTree.Node);
                    n.Content = "-" + ((SyntaxTree.Node)n.Children[0]).Content;
                    n.Children.RemoveAt(0);
                    
                }
            }
            else if (n.Children.Count == 3)
            {
                if (((SyntaxTree.Node)n.Children[0]).Content.Equals("("))
                {

                    n.Children.RemoveAt(0);
                    n.Children.RemoveAt(1);
                    RemoveGrammarNotation(n.Children[0] as SyntaxTree.Node);
                    n.Content = (n.Children[0] as SyntaxTree.Node).Content;

                    SyntaxTree.Node Child = n.Children[0] as SyntaxTree.Node;
                    foreach (SyntaxTree.Node grandChild in Child.Children)
                    {
                        n.Children.Add(grandChild);
                    }

                    n.Children.RemoveAt(0);

                }
                else
                {

                 
                    RemoveGrammarNotation(n.Children[0] as SyntaxTree.Node);
                    RemoveGrammarNotation(n.Children[1] as SyntaxTree.Node);
                    RemoveGrammarNotation(n.Children[2] as SyntaxTree.Node);

                    float a = float.Parse((n.Children[0] as SyntaxTree.Node).Content);
                    float b = float.Parse((n.Children[2] as SyntaxTree.Node).Content);
                    
                    n.Content = PerfomOperation(a, b, (n.Children[1] as SyntaxTree.Node).Content).ToString();
                }
            }

        }

        private float PerfomOperation(float a, float b, String op) {
            if (op.Equals("+")) {
                return a + b;
            }
            if (op.Equals("-")) {
                return a - b;
            }
            if (op.Equals("*")) {
                return a * b;
            }
            if (op.Equals("/")) {
                return a / b;
            }
            throw new Exception("Invalid operation " + op);
            return 0;
        }

    }
}
