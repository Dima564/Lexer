using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lexer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void AppendTextToRTB(RichTextBox RTB, string Text, Brush Color)
        {
            TextRange range = new TextRange(RTB.Document.ContentEnd, RTB.Document.ContentEnd);
            range.Text = Text ;
            range.ApplyPropertyValue(TextElement.ForegroundProperty, Color);
        }

        private void LexicalAnalysis(object sender, RoutedEventArgs e)
        {
            
            LexerLogTextBox.Document.Blocks.Clear();
            LexerResultTextBox.Document.Blocks.Clear();


            //Init Scanner with input
            String input = LexerExpressionTextBox.Text;
            Scanner.SetInput(input);

            bool breakloop = false;
            while (!breakloop)
            {

                String prefix = "";
                SolidColorBrush brush = Brushes.Silver;

                Token token = Scanner.ScanOneToken();


                switch (token.Type)
                {
                    case Token.T_LPAREN:
                    case Token.T_RPAREN:
                        prefix = "Parans: ";
                        brush = Brushes.Silver;
                        break;
                    case Token.T_DIVIDE:
                    case Token.T_MINUS:
                    case Token.T_PLUS:
                    case Token.T_MULT:
                        prefix = "Operation: ";
                        brush = Brushes.Green;
                        break;
                    case Token.T_FLOAT:
                        prefix  = "Float: ";
                        brush = Brushes.Blue;
                        break;
                    case Token.T_INTEGER:
                        prefix  = "Integer: ";
                        brush = Brushes.Brown;
                        break;
                    case Token.T_ERROR:
                        prefix = "Error: ";
                        brush =  Brushes.Red;
                        break;
                    case Token.T_END:
                        breakloop = true;
                        break;
                    default:
                        break;
                }

                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run(prefix + token.Value));
                paragraph.Foreground = brush;
                paragraph.Margin = new Thickness(0);
                LexerLogTextBox.Document.Blocks.Add(paragraph);
                AppendTextToRTB(LexerResultTextBox, token.Value.ToString(), brush);


            }
           
        }

     
        private void ShowSyntaxTree(object sender, RoutedEventArgs e)
        {

            if (SyntaxExpressionTextBox.Text.Length == 0)
            {
                SyntaxResultLabel.Content = "Field is empty. Please, enter expression";
                return;
            }
            Scanner.SetInput(SyntaxExpressionTextBox.Text);
            ArrayList lexemes = Scanner.GetLexemes();
            Parser.InitParser(lexemes);

            if (Parser.Match())
            {
                SyntaxResultLabel.Foreground = Brushes.Green;
                SyntaxResultLabel.Content = "The expression is correct.";
            }
            else
            {
                SyntaxResultLabel.Foreground = Brushes.Red;
                SyntaxResultLabel.Content = "The expression is incorrect.";
                return;
            }

            SyntaxTree.Node Root = new SyntaxTree(lexemes).GetRoot();

            if (Root != null)
            {
                new ExpressionTree().Traverse(Root);
                new TreeWindow(TreeDrawer.DrawTree(Root,3)).Show();

            }
           
        }


        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void ShowGrammarTree(object sender, RoutedEventArgs e)
        {
            if (GrammarExpressionTextBox.Text.Length == 0)
            {
                GrammarResultLabel.Content = "Field is empty. Please, enter expression";
                return;
            }
            Scanner.SetInput(GrammarExpressionTextBox.Text);
            ArrayList lexemes = Scanner.GetLexemes();
            Parser.InitParser(lexemes);

            if (Parser.Match())
            {
                GrammarResultLabel.Foreground = Brushes.Green;
                GrammarResultLabel.Content = "The expression is correct.";
            }
            else
            {
                GrammarResultLabel.Foreground = Brushes.Red;
                GrammarResultLabel.Content = "The expression is incorrect.";
                return;
            }

            SyntaxTree.Node Root = new SyntaxTree(lexemes).GetRoot();
            if (Root != null)
                new TreeWindow(TreeDrawer.DrawTree(Root)).Show();
            
          
        }


    }
}
