using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Lexer
{
    /// <summary>
    /// Interaction logic for TreeWindow.xaml
    /// </summary>
    public partial  class TreeWindow : Window
    {

        Canvas c;
        private int _height;
        public int CustomHeight
        {
            get { return _height; }
            set
            {
                if (value != _height)
                {
                    _height = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("CustomHeight"));
                }
            }
        }


        public TreeWindow(Canvas c)
        {
            this.c = c;
            InitializeComponent();
            MainGrid.Children.Add(c);
            CustomHeight = (int)c.ActualHeight;

        }

    

        public void AnimateWindowHeight(Window window, double height)
        {
            window.BeginInit();
            window.SizeToContent = System.Windows.SizeToContent.Manual;
            window.Dispatcher.BeginInvoke(new Action(() =>
            {
                DoubleAnimation heightAnimation = new DoubleAnimation();
                heightAnimation.Duration = new Duration(TimeSpan.FromSeconds(10.0));
                heightAnimation.To = height;
                heightAnimation.FillBehavior = FillBehavior.HoldEnd;
                window.BeginAnimation(Window.HeightProperty, heightAnimation);
            }), null);

            window.EndInit();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        //    AnimateWindowHeight(this, c.Height);

        }

        private void SaveImage(object sender, RoutedEventArgs e)
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;



    }
}
