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


        private void AppendTextToRTB(RichTextBox RTB, string Text, Brush Color)
        {
            TextRange range = new TextRange(RTB.Document.ContentEnd, RTB.Document.ContentEnd);
            range.Text = Text ;
            range.ApplyPropertyValue(TextElement.ForegroundProperty, Color);
        }

        private void LexicalAnalysis(object sender, RoutedEventArgs e)
        {
            
            LxLogRTB.Document.Blocks.Clear();
            LxResultRTB.Document.Blocks.Clear();


            //Init Scanner with input
            String input = LxExprLabel.Text;
            Scanner.SetInput(input);

            bool breakloop = false;
            while (!breakloop)
            {

                String prefix = "";
                SolidColorBrush brush = Brushes.Silver;

                Scanner.Token token = Scanner.ScanOneToken();


                switch (token.Type)
                {
                    case Scanner.T_LPAREN:
                    case Scanner.T_RPAREN:
                        prefix = "Parans: ";
                        brush = Brushes.Silver;
                        break;
                    case Scanner.T_DIVIDE:
                    case Scanner.T_MINUS:
                    case Scanner.T_PLUS:
                    case Scanner.T_MULT:
                        prefix = "Operation: ";
                        brush = Brushes.Green;
                        break;
                    case Scanner.T_FLOAT:
                        prefix  = "Float: ";
                        brush = Brushes.Blue;
                        break;  
                    case Scanner.T_INTEGER:
                        prefix  = "Integer: ";
                        brush = Brushes.Brown;
                        break;
                    case Scanner.T_ERROR :
                        prefix = "Error: ";
                        brush =  Brushes.Red;
                        break;
                    case Scanner.T_END:
                        breakloop = true;
                        break;
                    default:
                        break;
                }

                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run(prefix + token.Value));
                paragraph.Foreground = brush;
                paragraph.Margin = new Thickness(0);
                LxLogRTB.Document.Blocks.Add(paragraph);
                AppendTextToRTB(LxResultRTB, token.Value.ToString(), brush);


            }
           
        }
    }
}
