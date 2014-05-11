using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lexer
{
    class TreeCanvas : Canvas
    {
        private const int LEVEL_HEIGHT = 50;

        public TreeCanvas()
        {
            Height = 500;
            Width = 500;
        }

        public void DrawTree(SyntaxTree.Node Root)
        {
            this.Children.Clear();
          
            Background = Brushes.White;
         //   AddLabel("hello", 50, 50);
            DrawNode(Root, 0, 250, 25);

        }

        private void DrawNode(SyntaxTree.Node n, int level, int x, int y)
        {
            AddLabel(n.Content, x, y);

            if (n.Children.Count == 1)
            {
                AddLine(x+10, y+20, x+10, y + LEVEL_HEIGHT);
                DrawNode(n.Children[0] as SyntaxTree.Node, level + 1, x, y + LEVEL_HEIGHT);
            }
            else if (n.Children.Count == 2)
            {
                AddLine(x, y, x - 70, y + LEVEL_HEIGHT);
                AddLine(x, y, x + 70, y + LEVEL_HEIGHT);
                DrawNode(n.Children[0] as SyntaxTree.Node, level + 1, x - 70, y + LEVEL_HEIGHT);
                DrawNode(n.Children[1] as SyntaxTree.Node, level + 1, x + 70, y + LEVEL_HEIGHT);

            }
            else if (n.Children.Count == 3)
            {
                AddLine(x + 10, y, x - 100, y + LEVEL_HEIGHT);
                AddLine(x + 10, y, x + 10, y + LEVEL_HEIGHT);
                AddLine(x + 10, y, x + 100, y + LEVEL_HEIGHT);
                DrawNode(n.Children[0] as SyntaxTree.Node, level + 1, x - 100, y + LEVEL_HEIGHT);
                DrawNode(n.Children[1] as SyntaxTree.Node, level + 1, x, y + LEVEL_HEIGHT);
                DrawNode(n.Children[2] as SyntaxTree.Node, level + 1, x + 100, y + LEVEL_HEIGHT);
            }
        }

        private void AddLabel(String content, int x, int y)
        {
            Ellipse myEllipse = new Ellipse();
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();

            mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
            myEllipse.Fill = mySolidColorBrush;
            myEllipse.StrokeThickness = 2;
            myEllipse.Stroke = Brushes.Black;

            myEllipse.Width = 60;
            myEllipse.Height = 30;
            myEllipse.Margin = new Thickness(x-20, y-10, 0, 0);
            Children.Add(myEllipse);


            TextBlock label = new TextBlock();
            label.Text = content;
            label.Width = 60;
            label.TextAlignment = TextAlignment.Center;
            label.Margin = new Thickness(x-20, y-10, 0, 0);
            Children.Add(label);
        }

        private void AddLine(int stX, int stY, int endX, int endY)
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
            Children.Insert(1, myLine);
        }

    }
}
