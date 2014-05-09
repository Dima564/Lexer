using System;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String input = ExpressionLabel.Text;
            String output = "";
            bool breakloop = false;
            Scanner.SetInput(input);
            while (!breakloop)
            {
                Scanner.Token token = Scanner.ScanOneToken();
                switch (token.Type)
                {
                    case Scanner.T_LPAREN:
                    case Scanner.T_RPAREN:
                        output += "Parans: " + token.Value ;
                        break;
                    case Scanner.T_DIVIDE:
                    case Scanner.T_MINUS:
                    case Scanner.T_PLUS:
                    case Scanner.T_MULT:
                        output += "Operation: " + token.Value ;
                        break;
                    case Scanner.T_FLOAT:
                        output += "Float: " + token.Value ;
                        break;

                    case Scanner.T_INTEGER:
                        output += "Integer: " + token.Value;
                        break;
                    case Scanner.T_ERROR :
                        output += "error";
                        break;
                    case Scanner.T_END:
                        breakloop = true;
                        break;
                    default:
                        break;
                }
                output += '\n';

            }
            Console.Write(output);
            ResultsTextBlock.Text = output;
        }
    }
}
