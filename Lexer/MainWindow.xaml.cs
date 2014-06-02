using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
using WPFLocalizeExtension.Engine;
using WPFLocalizeExtension.Extensions;

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
            initLang("uk");

        }

        private void initLang(String locale) {
            Console.WriteLine("Changing locale to " + locale);
            ConvertButton.Content = GetLocalizedValue("convert", CultureInfo.GetCultureInfo(locale));
            Lexical_menuItem.Header = GetLocalizedValue("lexical_tab", CultureInfo.GetCultureInfo(locale));
            Syntax_menuItem.Header = GetLocalizedValue("syntax_tab", CultureInfo.GetCultureInfo(locale));
            Grammar_menuItem.Header = GetLocalizedValue("grammar_tab", CultureInfo.GetCultureInfo(locale));
            Polish_menuItem.Header = GetLocalizedValue("polish_tab", CultureInfo.GetCultureInfo(locale));
            Automata_menuItem.Header = GetLocalizedValue("automata_tab", CultureInfo.GetCultureInfo(locale));
            Information.Header = GetLocalizedValue("information", CultureInfo.GetCultureInfo(locale));
            Help.Header = GetLocalizedValue("help", CultureInfo.GetCultureInfo(locale));
            About_program.Header = GetLocalizedValue("About_program", CultureInfo.GetCultureInfo(locale));
            About_author.Header = GetLocalizedValue("About_author", CultureInfo.GetCultureInfo(locale));
            Instructions.Header = GetLocalizedValue("Instructions", CultureInfo.GetCultureInfo(locale));
            Expression_label.Content = GetLocalizedValue("Expression_label", CultureInfo.GetCultureInfo(locale));
            Lexical_analyze_button.Content = GetLocalizedValue("Lexical_analyze_button", CultureInfo.GetCultureInfo(locale));
            Lexer.Header = GetLocalizedValue("lexical_tab", CultureInfo.GetCultureInfo(locale));
            Grammar_tab.Header = GetLocalizedValue("grammar_tab", CultureInfo.GetCultureInfo(locale));
            Syntax.Header = GetLocalizedValue("syntax_tab", CultureInfo.GetCultureInfo(locale));
            Polish_tab.Header = GetLocalizedValue("polish_tab", CultureInfo.GetCultureInfo(locale));
            Grammar_expression_label.Content = GetLocalizedValue("Expression_label", CultureInfo.GetCultureInfo(locale));
            Show_grammar_tree.Content = GetLocalizedValue("show_grammar_tree", CultureInfo.GetCultureInfo(locale));
            Syntax_expr_label.Content = GetLocalizedValue("Expression_label", CultureInfo.GetCultureInfo(locale));
            Show_syntax_tree_button.Content = GetLocalizedValue("show_sytntax_tree", CultureInfo.GetCultureInfo(locale));
            Polish_expression_label.Content = GetLocalizedValue("Expression_label", CultureInfo.GetCultureInfo(locale));
            Prefix_notation.Content = GetLocalizedValue("Prefix_notation", CultureInfo.GetCultureInfo(locale));
            Postfix_notation.Content = GetLocalizedValue("Postfix_notation", CultureInfo.GetCultureInfo(locale));
            Evaluate_polish.Content = GetLocalizedValue("Evaluate_polish", CultureInfo.GetCultureInfo(locale));
            Settings.Header = GetLocalizedValue("settings", CultureInfo.GetCultureInfo(locale));
            English_lang.Header = GetLocalizedValue("english", CultureInfo.GetCultureInfo(locale));
            Ukrainian_lang.Header = GetLocalizedValue("ukrainian", CultureInfo.GetCultureInfo(locale));
       
        
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
                        prefix = FindResource("parans").ToString();
                        brush = Brushes.Silver;
                        break;
                    case Token.T_DIVIDE:
                    case Token.T_MINUS:
                    case Token.T_PLUS:
                    case Token.T_MULT:
                        prefix = FindResource("op").ToString();
                        brush = Brushes.Green;
                        break;
                    case Token.T_FLOAT:
                        prefix = FindResource("float").ToString();
                        brush = Brushes.Blue;
                        break;
                    case Token.T_INTEGER:
                        prefix = FindResource("int").ToString();
                        brush = Brushes.Brown;
                        break;
                    case Token.T_ERROR:
                        prefix = FindResource("error").ToString();
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
                SyntaxResultLabel.Content = FindResource("Empty_error").ToString();
                return;
            }
            Scanner.SetInput(SyntaxExpressionTextBox.Text);
            ArrayList lexemes = Scanner.GetLexemes();
            Parser.InitParser(lexemes);

            if (Parser.Match())
            {
                SyntaxResultLabel.Foreground = Brushes.Green;
                SyntaxResultLabel.Content = FindResource("valid_expr").ToString();
            }
            else
            {
                SyntaxResultLabel.Foreground = Brushes.Red;
                SyntaxResultLabel.Content = FindResource("invalid_expr").ToString();
                return;
            }

            SyntaxTree.Node Root = new SyntaxTree(lexemes).GetRoot();

            if (Root != null)
            {
                new ExpressionTree().RemoveGrammarNotation(Root);
                new TreeWindow(TreeDrawer.DrawTree(Root,2)).Show();

            }
           
        }

        private void ShowGrammarTree(object sender, RoutedEventArgs e)
        {
            if (GrammarExpressionTextBox.Text.Length == 0)
            {
                GrammarResultLabel.Content = FindResource("Empty_error").ToString();
                return;
            }
            Scanner.SetInput(GrammarExpressionTextBox.Text);
            ArrayList lexemes = Scanner.GetLexemes();
            Parser.InitParser(lexemes);

            if (Parser.Match())
            {
                GrammarResultLabel.Foreground = Brushes.Green;
                GrammarResultLabel.Content = FindResource("invalid_expr").ToString();
            }
            else
            {
                GrammarResultLabel.Foreground = Brushes.Red;
                GrammarResultLabel.Content = FindResource("invalid_expr").ToString();
                return;
            }

            SyntaxTree.Node Root = new SyntaxTree(lexemes).GetRoot();
            if (Root != null)
                new TreeWindow(TreeDrawer.DrawTree(Root,2)).Show();
            
          
        }



        private void Convert(object sender, RoutedEventArgs e)
        {
            PolishInvalidExpression.Content = "";
            PrefixLabel.Content = "";
            PostfixLabel.Content = "";
            PolishResult.Content = "";

            String text = PolishExprTextBox.Text;
            if (text.Length > 0)
            {
                Scanner.SetInput(text);
                ArrayList lexemes = Scanner.GetLexemes();
                Parser.InitParser(lexemes);

                if (Parser.Match())
                {
                    try
                    {
                        PrefixLabel.Content = PolishNotation.ToString(PolishNotation.InfixToPrefix(lexemes));
                        PostfixLabel.Content = PolishNotation.ToString(PolishNotation.InfixToPostfix(lexemes));

                    }
                    catch (Exception ex)
                    {
                        PolishInvalidExpression.Foreground = Brushes.Red;
                        PolishInvalidExpression.Content = FindResource("unary_error").ToString();
                    }
                }
                else
                {
                    PolishInvalidExpression.Foreground = Brushes.Red;
                    PolishInvalidExpression.Content = FindResource("invalid_expr").ToString();
                    return;
                }
            }
        }

        private void Evaluate(object sender, RoutedEventArgs e)
        {
            if (PolishInvalidExpression.Content.ToString().Equals("") && PostfixLabel.Content.ToString().Length > 0)
            {
                String text = PolishExprTextBox.Text;
                Scanner.SetInput(text);
                ArrayList lexemes = Scanner.GetLexemes();
                ArrayList postfixLexemes = PolishNotation.InfixToPostfix(lexemes);
                PolishResult.Content = PolishNotation.EvaluatePostfix(postfixLexemes);
            }
            else
            {
                PolishInvalidExpression.Content = FindResource("Enter_expression_error").ToString();
            }
           
        }

        private void SwitchCulture(string culture)
        {

            Console.WriteLine("SwitchCulture");
            CultureInfo ci = CultureInfo.InvariantCulture;
            try
            {
                ci = new CultureInfo(culture);
            }
            catch (CultureNotFoundException)
            {
                Console.WriteLine("Exception");
                try
                {
                    // Try language without region
                    ci = new CultureInfo(culture.Substring(0, 2));
                }
                catch (Exception)
                {
                    ci = CultureInfo.InvariantCulture;
                }
            }
            finally
            {
                Console.WriteLine(ci.ToString());
                
                LocalizeDictionary.Instance.Culture = ci;
            }
        }

        private void changeLocaleToEng(object sender, RoutedEventArgs e)
        {
            initLang("en");
            SwitchCulture("en");
        }

        private void changeLocaleToUkr(object sender, RoutedEventArgs e)
        {
            initLang("uk");
            SwitchCulture("uk");
        }

        private string GetLocalizedValue(
           string resourceKey,
           CultureInfo cultureToUse)
        {
            string resourceAssembly = "Lexer";
            string resourceDictionary = "Strings";
            try
            {
                return LocalizeDictionary.Instance.GetLocalizedObject<string>(
                             resourceAssembly,
                             resourceDictionary,
                             resourceKey,
                             cultureToUse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "@" + resourceKey;
            }
        }

        private void LexicalInfo(object sender, RoutedEventArgs e)
        {
            new InfoWindow("lexical_info.png").Show();
        }

        private void SyntaxInfo(object sender, RoutedEventArgs e)
        {
            new InfoWindow("syntax_info.png").Show();
        }

        private void GrammarInfo(object sender, RoutedEventArgs e)
        {
            new InfoWindow("grammar.png").Show();

        }

        private void PolishInfo(object sender, RoutedEventArgs e)
        {
            new InfoWindow("polish.png").Show();

        }

        private void AutomataInfo(object sender, RoutedEventArgs e)
        {
            new InfoWindow("automata.png").Show();

        }

        private void about_prg(object sender, RoutedEventArgs e)
        {
            new InfoWindow("about_program.png").Show();

        }

        private void about_author(object sender, RoutedEventArgs e)
        {
            new InfoWindow("about_author.png").Show();

        }

        private void instr(object sender, RoutedEventArgs e)
        {
            new InfoWindow("instructions.png").Show();

        }

   


       

    }
}
