using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

        Image img;
        private int _height;
        private int _width;
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

        public int CustomWidth
        {
            get { return _width; }
            set
            {
                if (value != _width)
                {
                    _width = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("CustomWidth"));
                }
            }
        }


        public TreeWindow(Image img)
        {
            this.img = img;
            InitializeComponent();
            CustomHeight = (int)img.Height;
            CustomWidth = (int)img.Width;
            MainGrid.Children.Add(img);
        }

   

        private void SaveImage(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Image"; // Default file name
            dlg.DefaultExt = ".png"; // Default file extension
            dlg.Filter = "PNG (.png)|*.png"; // Filter files by extension
            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                using (FileStream outStream = new FileStream(filename, FileMode.Create))
                {
                    // Use png encoder for our data
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    // push the rendered bitmap to it
                    encoder.Frames.Add(BitmapFrame.Create(img.Source as BitmapSource));
                    // save the data to the stream
                    encoder.Save(outStream);
                }
            }



        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Window_loaded(object sender, RoutedEventArgs e)
        {

        }



    }
}
