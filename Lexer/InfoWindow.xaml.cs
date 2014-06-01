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
    /// Interaction logic for InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        public InfoWindow(string imgname)
        {
            InitializeComponent();
            BitmapImage image = new BitmapImage(new Uri("pack://application:,,,/Resources/" + imgname ,UriKind.RelativeOrAbsolute));
            InfoImage.Source = image;
            this.Height = image.Height;
            Width = image.Width;
        }
    }
}
