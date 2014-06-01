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
using System.Windows.Shapes;

namespace Lexer
{
    /// <summary>
    /// Interaction logic for StartUp.xaml
    /// </summary>
    public partial class StartUp : Window
    {
     
        public StartUp()
        {
            InitializeComponent();
            Header.Content = FindResource("course_project").ToString();
            Begin.Content = FindResource("start").ToString();
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();

        }
    }
}
