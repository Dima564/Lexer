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
            ResultsTextBlock.Document.Blocks.Clear();
            String input = ExpressionLabel.Text;
            String result = "";
            bool breakloop = false;
            Scanner.SetInput(input);
            while (!breakloop)
            {

                Paragraph paragraph = new Paragraph();
                paragraph.Margin = new Thickness(0);
                String output = "";

               
                Scanner.Token token = Scanner.ScanOneToken();
                switch (token.Type)
                {
                    case Scanner.T_LPAREN:
                    case Scanner.T_RPAREN:
                        output += "Parans: " + token.Value ;
                        paragraph.Foreground = Brushes.Silver;
                        break;
                    case Scanner.T_DIVIDE:
                    case Scanner.T_MINUS:
                    case Scanner.T_PLUS:
                    case Scanner.T_MULT:
                        output += "Operation: " + token.Value ;
                        paragraph.Foreground = Brushes.Green;
                        break;
                    case Scanner.T_FLOAT:
                        output += "Float: " + token.Value ;
                        paragraph.Foreground = Brushes.Blue;
                        break;
                    case Scanner.T_INTEGER:
                        output += "Integer: " + token.Value;
                        paragraph.Foreground = Brushes.BlueViolet;

                        break;
                    case Scanner.T_ERROR :
                        output += "error";
                        paragraph.Foreground = Brushes.Red;
                        break;
                    case Scanner.T_END:
                        breakloop = true;
                        break;
                    default:
                        break;
                }

                
                paragraph.Inlines.Add(new Run(output));
                paragraph.Margin = new Thickness(0);
                ResultsTextBlock.Document.Blocks.Add(paragraph);

            }
           
        }
    }
}
