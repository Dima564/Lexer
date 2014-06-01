using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lexer
{
    static class TreeDrawer
    {

        private static int WIDENING_LEVEL = 2;
        private const int LEVEL_HEIGHT = 50;
        private static int minX = int.MaxValue;
        private static int maxX = 0;
        private static int maxY = 0;


        public static Image DrawTree(SyntaxTree.Node Root, int widening)
        {
            WIDENING_LEVEL = widening;
            return DrawTree(Root);

        }

        public static Image DrawTree(SyntaxTree.Node Root) {
             minX = int.MaxValue;
             maxX = 0;
             maxY = 0;


            Canvas c = new Canvas();
            // Draw tree on canvas
            DrawNode(Root, 0, 3000, 100,c);

            // Adjust tree position
            minX -= 30;
            maxY += 30;
            maxX += 50;

            Rect rect1 = new Rect(0, 0, 0, 0);

            System.Windows.Int32Rect rcFrom = new System.Windows.Int32Rect();
            rcFrom.X = minX;
            rcFrom.Y = 5;
            rcFrom.Width = maxX - minX;
            rcFrom.Height = maxY - 5;


            // VERY important
            Size size = new Size(maxX, maxY);
            c.Measure(size);
            c.Arrange(rect1);

            RenderTargetBitmap renderBitmap =
               new RenderTargetBitmap(
                maxX,
                maxY,
                 96d,
                 96d,
                 PixelFormats.Pbgra32);
            renderBitmap.Render(c);
            BitmapSource bs = new CroppedBitmap(renderBitmap as BitmapSource, rcFrom);

            Image img = new Image();
            img.Source = bs;
            return img;
        }



        private static void DrawNode(SyntaxTree.Node n, int level, int x, int y, Canvas c)
        {

            maxY = (y > maxY) ? y : maxY;
            maxX = (x > maxX) ? x : maxX;
            minX = (x < minX) ? x : minX;

            AddLabel(n.Content, x, y-20, c);
            Console.WriteLine(n.Content + " : " + level.ToString());
            if (n.Children.Count == 1)
            {
                AddLine(x + 10, y , x + 10, y + LEVEL_HEIGHT,c);
                DrawNode(n.Children[0] as SyntaxTree.Node, level + 1, x, y + LEVEL_HEIGHT, c);
            }
            else if (n.Children.Count == 2)
            {
                int xOffset = level == WIDENING_LEVEL ? 70 : 70 * (Math.Abs(WIDENING_LEVEL - level));
                AddLine(x, y, x - xOffset, y + LEVEL_HEIGHT - 20, c);
                AddLine(x, y, x + xOffset, y + LEVEL_HEIGHT - 20, c);
                DrawNode(n.Children[0] as SyntaxTree.Node, level + 1, x - xOffset, y + LEVEL_HEIGHT, c);
                DrawNode(n.Children[1] as SyntaxTree.Node, level + 1, x + xOffset, y + LEVEL_HEIGHT, c);

            }
            else if (n.Children.Count == 3)
            {
                if ((n.Children[0] as SyntaxTree.Node).Content.Equals("("))
                {
                    Console.WriteLine("Fine");
                }


                int xOffset = level == WIDENING_LEVEL ? 100 : 100 * (Math.Abs(WIDENING_LEVEL - level));
                AddLine(x + 10, y, x - xOffset, y + LEVEL_HEIGHT - 20, c);
                AddLine(x + 10, y, x + 10, y + LEVEL_HEIGHT - 20, c);
                AddLine(x + 10, y, x + xOffset, y + LEVEL_HEIGHT - 20, c);

                if (xOffset < 0)
                    throw new Exception("WTF");
                DrawNode(n.Children[0] as SyntaxTree.Node, level + 1, x - xOffset, y + LEVEL_HEIGHT, c);
                DrawNode(n.Children[1] as SyntaxTree.Node, level + 1, x, y + LEVEL_HEIGHT, c);
                DrawNode(n.Children[2] as SyntaxTree.Node, level + 1, x + xOffset, y + LEVEL_HEIGHT, c);
            }
        }

        private static void AddLabel(String content, int x, int y, Canvas c)
        {
            Ellipse myEllipse = new Ellipse();
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();

            mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
            myEllipse.Fill = mySolidColorBrush;
            myEllipse.StrokeThickness = 2;
            myEllipse.Stroke = Brushes.Black;

            myEllipse.Width = 60;
            myEllipse.Height = 30;
            myEllipse.Margin = new Thickness(x - 20, y - 10, 0, 0);
            c.Children.Add(myEllipse);


            TextBlock label = new TextBlock();
            label.Text = content;
            label.Width = 60;
            label.TextAlignment = TextAlignment.Center;
            label.Margin = new Thickness(x - 20, y - 10, 0, 0);
            c.Children.Add(label);
        }

        private static void AddLine(int stX, int stY, int endX, int endY, Canvas c)
        {
            Line myLine = new Line();
            myLine.Stroke = System.Windows.Media.Brushes.Black;
            myLine.X1 = stX;
            myLine.X2 = endX;
            myLine.Y1 = stY;
            myLine.Y2 = endY;
            myLine.HorizontalAlignment = HorizontalAlignment.Left;
            myLine.VerticalAlignment = VerticalAlignment.Center;
            myLine.StrokeThickness = 2;
            //Children.Add(myLine);
            c.Children.Insert(1, myLine);
        }

    }
}
