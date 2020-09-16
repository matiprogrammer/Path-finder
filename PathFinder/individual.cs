using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PathFinder
{
    public class Individual
    {
        public List<Point> path = new List<Point>();
        public List<int> anglePath = new List<int>();
        int angle, sizePath, sizeAnglePath;
        public RectangleGeometry ellipse;
        public Rect rect;
        public Path myPath;
        public double adjustment;
        public TextBlock text;
        public Individual(Random rnd)
        {
            
            ellipse = new RectangleGeometry();
            rect = new Rect();
            angle = rnd.Next(0, 8)*45;
            sizePath = 0;
            sizeAnglePath = 0;
            
            rect.Size = new Size(10, 10);
            
            ellipse.Rect = rect;
            myPath = new Path();
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();

           
            mySolidColorBrush.Color = Color.FromRgb((byte)rnd.Next(0, 256), (byte)rnd.Next(0, 256), (byte)rnd.Next(0, 256));
            myPath.Fill = mySolidColorBrush;
            myPath.Stroke = Brushes.Black;
            myPath.StrokeThickness = 1;
            myPath.Data = ellipse;

           
            

        }


        public int Angle { get { return angle; } set { angle = value; } }
        public int SizePath { get { return sizePath; } set { sizePath = value; } }
        public int SizeAnglePath { get { return sizeAnglePath; } set { sizeAnglePath = value; } }
        public void InitializeEllipse(Canvas canvas)
        {
            canvas.Children.Add(myPath);
            Canvas.SetTop(myPath, 294);
            Canvas.SetLeft(myPath, 50);
            rect.Location = new Point(Canvas.GetLeft(myPath), Canvas.GetTop(myPath));
            
        }
        public Point GetPoint(int index)
        {
            return path[index];
        }
        public Point GetLastPoint()
        {
            return path[sizePath - 1];
        }

        public void AddPoint(Point point)
        {
            path.Add(point);
            sizePath++;
        }
        public bool IsStopped(Canvas canvas)
        {
            return path.Exists(x => x.X == 0 || x.X == canvas.ActualWidth || x.Y == canvas.ActualHeight-10 || x.Y == 0);
        }

        public int GetLastAngle()
        {
            return anglePath[sizeAnglePath - 1];
        }

        public int GetAngle(int index)
        {
            return anglePath[index];
        }

        public void AddAngle(int x)
        {
            anglePath.Add(x);
            sizeAnglePath++;
        }

        public void PrintAdjustment(Canvas canvas)
        {
            text = new TextBlock();
            //text.Text =GetLastPoint().X.ToString()+ "," +GetLastPoint().Y.ToString();
            text.Text = ((int)adjustment).ToString();
            canvas.Children.Add(text);
            Canvas.SetLeft(text, GetLastPoint().X);
            Canvas.SetTop(text, GetLastPoint().Y);
        }

        public void RemovePrintAdjustment(Canvas canvas)
        {
            canvas.Children.Remove(text);
        }
    }
}
