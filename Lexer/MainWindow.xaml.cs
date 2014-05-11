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

        private void SyntaxAnalysis(object sender, RoutedEventArgs e)
        {
            Scanner.SetInput(SyntaxExpressionTextBox.Text);
            ArrayList lexemes = Scanner.GetLexemes();
            Parser.InitParser(lexemes);
            if (Parser.Match())
            {
                ResultLabel.Foreground = Brushes.Green;
                ResultLabel.Content = "Введений вираз правильний.";
            }
            else
            {
                ResultLabel.Foreground = Brushes.Red;
                ResultLabel.Content = "Введений вираз неправильний.";
            }

            SyntaxTree tree = new SyntaxTree(lexemes);
            tree.Traverse();
        }

        private void ShowSyntaxTree(object sender, RoutedEventArgs e)
        {
            Canvas c = new Canvas();
            c.Height = 200;
            c.Width = 500;

            //Line myLine = new Line();
            //myLine.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
            //myLine.X1 = 1;
            //myLine.X2 = 50;
            //myLine.Y1 = 1;
            //myLine.Y2 = 50;
            //myLine.HorizontalAlignment = HorizontalAlignment.Left;
            //myLine.VerticalAlignment = VerticalAlignment.Center;
            //myLine.StrokeThickness = 2;
            //c.Children.Add(myLine);
            new TreeWindow(c).Show();
        }


        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }


        /* Window Animations
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Console.WriteLine(this.ActualHeight);
            TabItem a = e.AddedItems[0] as TabItem;
            switch (a.Name)
            {
                case "Lexer":
                    AnimateWindowHeight(this, 350);                

                    break;
                case "Syntax":
                    AnimateWindowHeight(this, 500);                
                    break;

                default:
                    AnimateWindowHeight(this, 350);                

                    break;
            }


        }

        public void AnimateWindowHeight(Window window,double height)
        {
            window.BeginInit();
            //setting SizeToContent of window to Height get you the exact value of window height required to display completely
            window.SizeToContent = System.Windows.SizeToContent.Height;
           
            window.SizeToContent = System.Windows.SizeToContent.Manual;
            //run the animation code at backgroud for smoothness
            window.Dispatcher.BeginInvoke(new Action(() =>
            {
                DoubleAnimation heightAnimation = new DoubleAnimation();
                heightAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
                heightAnimation.From = window.ActualHeight;
                heightAnimation.To = height;
                heightAnimation.FillBehavior = FillBehavior.HoldEnd;
                window.BeginAnimation(Window.HeightProperty, heightAnimation);
                LexerGrid.BeginAnimation(Grid.HeightProperty, heightAnimation);
            }), null);
            window.EndInit();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.ActualHeight == 500)
                AnimateWindowHeight(this, 350);
            else 
                AnimateWindowHeight(this, 500);
        }
         * 
         * */



     

    }
}
