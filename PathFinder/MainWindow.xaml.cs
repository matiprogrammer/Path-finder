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
using System.Threading;
using System.ComponentModel;
using System.Windows.Threading;

namespace PathFinder
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    /// 

   
    public partial class MainWindow : Window
    {
        bool isAuto = true, isEndedGeneration = true;
        Random number = new Random();
        DispatcherTimer uiTimer;
        List<Individual> population = new List<Individual>();
        List<Individual> _Alive_ = new List<Individual>();
        List<Individual> InGoal = new List<Individual>();
        private static readonly object syncLock = new object();
        Rect obstacleRect, goalRect, obstacle2Rect, obstacle3Rect, obstacle4Rect, obstacle5Rect, obstacle6Rect;
        RectangleGeometry rectangleObstacle, goalRectangle, rectangle2Obstacle, rectangle3Obstacle, rectangle4Obstacle, rectangle5Obstacle, rectangle6Obstacle;
        Path obstaclePath, goalPath, obstacle2Path, obstacle3Path, obstacle4Path, obstacle5Path, obstacle6Path;
        
        private void Stop(object sender, RoutedEventArgs e)
        {
            isAuto = false;
        }

        private void Auto(object sender, RoutedEventArgs e)
        {

            isAuto = true;

        }
      

        int index = 0, generation=0, populationAmount = 100, alive =100, inGoal=0;
        public Ellipse goal;
       
        public MainWindow()
        {
            InitializeComponent();
            for(int i=0;i<populationAmount;i++)
            {
                lock (syncLock)
                {
                    population.Add(new Individual(number));
                    population[i].InitializeEllipse(canvas);
                }
            }
            
            rectangleObstacle = new RectangleGeometry();
            obstacleRect = new Rect();
            obstacleRect.Size = new Size(50, 300);
            rectangleObstacle.Rect = obstacleRect;
            obstaclePath = new Path();
            obstaclePath.Fill = Brushes.Yellow;
            obstaclePath.Stroke = Brushes.Black;
            obstaclePath.StrokeThickness = 1;
            obstaclePath.Data = rectangleObstacle;
            canvas.Children.Add(obstaclePath);
            Canvas.SetTop(obstaclePath, 149);
            Canvas.SetLeft(obstaclePath, 250);
            obstacleRect.Location = new Point(Canvas.GetLeft(obstaclePath), Canvas.GetTop(obstaclePath));

            rectangle2Obstacle = new RectangleGeometry();
            obstacle2Rect = new Rect();
            obstacle2Rect.Size = new Size(50, 200);
            rectangle2Obstacle.Rect = obstacle2Rect;
            obstacle2Path = new Path();
            obstacle2Path.Fill = Brushes.Green;
            obstacle2Path.Stroke = Brushes.Black;
            obstacle2Path.StrokeThickness = 1;
            obstacle2Path.Data = rectangle2Obstacle;
            canvas.Children.Add(obstacle2Path);
            Canvas.SetTop(obstacle2Path, 5);
            Canvas.SetLeft(obstacle2Path, 400);
            obstacle2Rect.Location = new Point(Canvas.GetLeft(obstacle2Path), Canvas.GetTop(obstacle2Path));

            rectangle3Obstacle = new RectangleGeometry();
            obstacle3Rect = new Rect();
            obstacle3Rect.Size = new Size(50, 220);
            rectangle3Obstacle.Rect = obstacle3Rect;
            obstacle3Path = new Path();
            obstacle3Path.Fill = Brushes.Green;
            obstacle3Path.Stroke = Brushes.Black;
            obstacle3Path.StrokeThickness = 1;
            obstacle3Path.Data = rectangle3Obstacle;
            canvas.Children.Add(obstacle3Path);
            Canvas.SetTop(obstacle3Path, 380);
            Canvas.SetLeft(obstacle3Path, 400);
            obstacle3Rect.Location = new Point(Canvas.GetLeft(obstacle3Path), Canvas.GetTop(obstacle3Path));

            rectangle4Obstacle = new RectangleGeometry();
            obstacle4Rect = new Rect();
            obstacle4Rect.Size = new Size(300, 20);
            rectangle4Obstacle.Rect = obstacle4Rect;
            obstacle4Path = new Path();
            obstacle4Path.Fill = Brushes.Orange;
            //obstacle4Path.Stroke = Brushes.Black;
            obstacle4Path.StrokeThickness = 1;
            obstacle4Path.Data = rectangle4Obstacle;
            canvas.Children.Add(obstacle4Path);
            Canvas.SetTop(obstacle4Path, 140);
            Canvas.SetLeft(obstacle4Path, 650);
            obstacle4Rect.Location = new Point(Canvas.GetLeft(obstacle4Path), Canvas.GetTop(obstacle4Path));

            rectangle5Obstacle = new RectangleGeometry();
            obstacle5Rect = new Rect();
            obstacle5Rect.Size = new Size(300, 20);
            rectangle5Obstacle.Rect = obstacle5Rect;
            obstacle5Path = new Path();
            obstacle5Path.Fill = Brushes.Orange;
           // obstacle5Path.Stroke = Brushes.Black;
            obstacle5Path.StrokeThickness = 1;
            obstacle5Path.Data = rectangle5Obstacle;
            canvas.Children.Add(obstacle5Path);
            Canvas.SetTop(obstacle5Path, 410);
            Canvas.SetLeft(obstacle5Path, 650);
            obstacle5Rect.Location = new Point(Canvas.GetLeft(obstacle5Path), Canvas.GetTop(obstacle5Path));

            rectangle6Obstacle = new RectangleGeometry();
            obstacle6Rect = new Rect();
            obstacle6Rect.Size = new Size(20, 290);
            rectangle6Obstacle.Rect = obstacle6Rect;
            obstacle6Path = new Path();
            obstacle6Path.Fill = Brushes.Orange;
           // obstacle6Path.Stroke = Brushes.Black;
            obstacle6Path.StrokeThickness = 1;
            obstacle6Path.Data = rectangle6Obstacle;
            canvas.Children.Add(obstacle6Path);
            Canvas.SetTop(obstacle6Path, 140);
            Canvas.SetLeft(obstacle6Path, 950);
            obstacle6Rect.Location = new Point(Canvas.GetLeft(obstacle6Path), Canvas.GetTop(obstacle6Path));

            goalRectangle = new RectangleGeometry();
            goalRect = new Rect();
            goalRect.Size = new Size(25, 25);
            
            goalRectangle.Rect = goalRect;
            goalPath = new Path();
            goalPath.Fill = Brushes.Yellow;
            goalPath.Stroke = Brushes.Black;
            goalPath.StrokeThickness = 1;
            goalPath.Data = goalRectangle;
            
            
            canvas.Children.Add(goalPath);
            Canvas.SetLeft(goalPath, 1002);
            Canvas.SetTop(goalPath, 286);
            goalRect.Location = new Point(Canvas.GetLeft(goalPath) + 2, Canvas.GetTop(goalPath) + 2);

            goal = new Ellipse();
            
            goal.Fill = Brushes.Red;
            goal.Width = 30;
            goal.Height = 30;
            goal.StrokeThickness = 2;
            goal.Stroke = Brushes.Black;
            canvas.Children.Add(goal);
            Canvas.SetLeft(goal, 1000);
            Canvas.SetTop(goal, 284);
            
        }


        private void Start(object sender, RoutedEventArgs e)
        {
            
            Alive.Text = "Żywych: " + alive;
            Goal.Text = "U celu: " + inGoal;
            uiTimer = new DispatcherTimer(); //This timer is created on GUI thread.
            uiTimer.Tick += new EventHandler(uiTimerTick);
            uiTimer.Interval = new TimeSpan(0, 0, 0, 0, 0); // 25 ticks per second
            uiTimer.Start();
        }

        public bool isIntersects(int index)
        {
            if (population[index].IsStopped(canvas) ||population[index].rect.IntersectsWith(obstacleRect) || population[index].rect.IntersectsWith(goalRect) ||
                population[index].rect.IntersectsWith(obstacle2Rect) || population[index].rect.IntersectsWith(obstacle3Rect) ||
                 population[index].rect.IntersectsWith(obstacle4Rect) || population[index].rect.IntersectsWith(obstacle5Rect) || population[index].rect.IntersectsWith(obstacle6Rect))
            {
                lock (syncLock)
                {
                    if (!_Alive_.Contains(population[index]))
                    {
                        _Alive_.Add(population[index]);
                        alive--;
                        Alive.Text = "Żywych: " + alive;
                    }
                    if (population[index].rect.IntersectsWith(goalRect) &&!InGoal.Contains(population[index]))
                    {
                        InGoal.Add(population[index]);
                        inGoal++;
                        Goal.Text = "U celu: " + inGoal;
                    }
                }
                return true;
            }
            return false;
        }
        private void uiTimerTick(object sender, EventArgs e)
        {
            int counter = 0;
            for (int i = 0; i < populationAmount; i++)
            {
                if (!isIntersects(i))
                    MoveRandom(population[i]);
                else
                {
                    counter++;
                   // Console.WriteLine(counter);
                    if (counter == 100)
                    {
                        uiTimer.Stop();
                        Console.WriteLine("Koniec generacji");
                    }
                }
                

              
            }
           
        }

        public void MoveRandom(Individual individual)
        { int a,b;
            int newX = (int)Canvas.GetLeft(individual.myPath), newY = (int)Canvas.GetTop(individual.myPath);
            lock (syncLock)
            {
               // a= number.Next(0, 8) * 45;
            //    if (individual.Angle < 46)
            //        a = number.Next(0, individual.Angle + 45);
            //    else if (individual.Angle > 314)
            //    {
            //        a = number.Next(individual.Angle - 45, 360);
            //        int c = number.Next(0, 45);
            //        if (a % 2 == 0) a = c;
            //    }
            //    else
            //        a = number.Next(individual.Angle - 45, individual.Angle + 45);
            }
            b = number.Next(1, 4);

            switch (individual.Angle/45)
                {
                    case (0):
                    if (b == 3) { newY--;newX++; }
                    else if (b == 2) { newX++;newY++; }
                    else { newX++; }
                    individual.AddAngle(0);
                        break;
                    case (1):
                    if (b == 3){newY--;}
                    else if (b == 2) { newX++; }
                    else{newX++;newY--;}
                    individual.AddAngle(45);
                    break;
                    case (2):
                    if (b == 3) { newY--;newX++; }
                    else if (b == 2) { newX--; newY--; }
                    else { newY--; }
                    individual.AddAngle(90);
                    break;
                    case (3):
                    if (b == 3) { newY--; }
                    else if (b == 2) { newX--; }
                    else { newY--; newX--; }
                    individual.AddAngle(135);
                    break;
                    case (4):
                    if (b == 3) { newY--; newX--; }
                    else if (b == 2) { newX--;newY++; }
                    else {  newX--; }
                    newX--;
                    individual.AddAngle(180);
                    break;
                    case (5):
                    if (b == 3) { newY++; }
                    else if (b == 2) { newX--; }
                    else { newY++; newX--; }
                    individual.AddAngle(225);
                    break;
                    case (6):
                    if (b == 3) { newY++; newX--; }
                    else if (b == 2) { newX++;newY++; }
                    else { newY++; }
                    individual.AddAngle(270);
                    break;
                    case (7):
                    if (b == 3) { newY++; }
                    else if (b == 2) { newX++; }
                    else { newY++; newX++; }
                    individual.AddAngle(315);
                    break;
                   

                }
                if (newX <= 0) newX = 0;
                if (newY <= 0) newY = 0;
                if (newX >= 1190) newX = 1190;
                if (newY >= 598) newY = 598;
            
            individual.AddPoint(new Point(newX, newY));
            //Console.WriteLine(individual.GetLastAngle());
           
            Canvas.SetLeft(individual.myPath, newX);
            Canvas.SetTop(individual.myPath, newY);
            individual.rect.Location = new Point(Canvas.GetLeft(individual.myPath), Canvas.GetTop(individual.myPath));

        }

        public void Move()
        {
            uiTimer = new DispatcherTimer(); //This timer is created on GUI thread.
            uiTimer.Tick += new EventHandler(Move_);
            uiTimer.Interval = new TimeSpan(0, 0, 0, 0, 0); // 25 ticks per second
            uiTimer.Start();
        }

        private void Move_(object sender, EventArgs e)
        {
            
            
                _Move();
            
        }

        public void _Move()
        {
            int counter = 0;
            for (int i = 0; i < populationAmount; i++)
            {
                if (!isIntersects(i))
                    MovePopulation(population[i], index);
                else
                {
                    counter++;
                    // Console.WriteLine(counter);
                    if (counter == 100)
                    {   
                       uiTimer.Stop();
                        isEndedGeneration = true;
                        Console.WriteLine("Koniec generacji");
                        next.IsEnabled = true;
                        index = 0;
                        
                        PrintAdjustment();
                        if (isAuto)
                            NextGeneration();
                    }
                }



            }
            index++;
        }

        public void MovePopulation(Individual individual, int index)
        {
            int newX = (int)Canvas.GetLeft(individual.myPath), newY = (int)Canvas.GetTop(individual.myPath);
            int maxMutation = 500 + generation * 15, lenghtMutation = number.Next(20, 100), randomAngle = number.Next(0, 8)*45;
            
                if (maxMutation > 800) maxMutation=800;
            if (number.Next(0, maxMutation ) == 1)
                individual.Angle = number.Next(0, 8) * 45;
            if (individual.anglePath.Count == index)
                individual.AddAngle(individual.Angle);

            if (number.Next(0, maxMutation+15000) == 12)
            {
                Console.WriteLine("mutacja");
                for (int i = 0; i < lenghtMutation; i++)
                {
                    if (individual.anglePath.Count == index + i)
                        individual.AddAngle(randomAngle);
                    else
                        individual.anglePath[index + i] = randomAngle;
                }
            }



            switch (individual.GetAngle(index)/45)
            {
                case 0:
                    newX++;
                    break;
                case (1):
                    newX++;
                    newY--;
                    
                    break;
                case (2):
                    newY--;
                    
                    break;
                case (3):
                    newY--;
                    newX--;
                    
                    break;
                case (4):
                    newX--;
                    
                    break;
                case (5):
                    newY++;
                    newX--;
                    
                    break;
                case (6):
                    newY++;
                    
                    break;
                case (7):
                    newX++;
                    newY++;
                    
                    break;
            }
            if (newX <= 0) newX = 0;
            if (newY <= 0) newY = 0;
            if (newX >= 1190) newX = 1190;
            if (newY >= 598) newY = 598;

            individual.AddPoint(new Point(newX, newY));
          // Console.WriteLine("X= "+individual.GetLastPoint().X);
            Canvas.SetLeft(individual.myPath, newX);
            Canvas.SetTop(individual.myPath, newY);
            individual.rect.Location = new Point(Canvas.GetLeft(individual.myPath), Canvas.GetTop(individual.myPath));
        }

        public double AdjustmentFunction(Individual individual)
        {
            double subtraction=individual.GetLastPoint().X -( Canvas.GetLeft(goalPath)+15);
            double sum = individual.GetLastPoint().Y - (Canvas.GetTop(goalPath)+15);
            double length = Math.Sqrt(Math.Abs((subtraction*subtraction)) + Math.Abs((sum*sum)));
            length += 230;
           // Console.WriteLine("length= "+length);
            if (individual.GetLastPoint().X == 240) length += 320;
            if (individual.path.Exists(x => x.X > 250)) length -= 100;
            if (individual.path.Exists(x => x.X > 280)) length -= 100;
            if (individual.path.Exists(x => ((x.X > 650 && x.X<939) && (x.Y > 159 && x.Y < 399)))) length += (individual.GetLastPoint().X-650)+150;
            if (individual.rect.IntersectsWith(goalRect)) length = 0;
           // if (individual.GetLastPoint().X == 390) length += 50;
            individual.adjustment = length;
            return length;
        }

        public Individual TournamentSelection(Individual fighter1, Individual fighter2, Individual fighter3)
        {
            Individual winner = fighter1;
            double tmp = AdjustmentFunction(fighter1);
            if (AdjustmentFunction(fighter2) < tmp)
            {
                tmp = AdjustmentFunction(fighter2);
                winner = fighter2;
            }
            if (AdjustmentFunction(fighter3) < tmp)
            {
                tmp = AdjustmentFunction(fighter3);
                winner = fighter3;
            }
            return winner;

        }

        public List<Individual> DrawingGeneration()
        {
            List<Individual> parents = new List<Individual>();
            for(int i=0;i<populationAmount;i++)
            {
               parents.Add(TournamentSelection(population[number.Next(0, populationAmount )], population[number.Next(0, populationAmount )], population[number.Next(0, populationAmount)]));
            }
            return parents;
        }

       
        private void KolejnePokolenie(object sender, RoutedEventArgs e)
        {

            NextGeneration();
            start.IsEnabled = false;
            next.IsEnabled = false;
        }
        public void NextGeneration()
        {
            int parent2, parent1;
            List<Individual> children = new List<Individual>();
            List<Individual> parents = DrawingGeneration();
            List<int> angles = new List<int>();
            RemovePrintAdjustment();
            generation++;
            alive = populationAmount;
            _Alive_.Clear();
            Alive.Text = "Żywych: " + alive;
            InGoal.Clear();
            inGoal = 0;
            Goal.Text = "U celu: " + inGoal;
            Generation.Text = "Pokolenie: "+generation.ToString();
            for (int i = 0; i < populationAmount; i++)
            {
                parent1 = number.Next(0, populationAmount);
                parent2 = number.Next(0, populationAmount);
                while (parent2 == parent1)
                    parent2 = number.Next(0, populationAmount);

                children.Add(new Individual(number));
                children[i].InitializeEllipse(canvas);
                for (int j = 0; j < parents[parent1].SizeAnglePath; j++)
                {
                    if (number.Next(0, 2) == 0 && parents[parent2].SizeAnglePath > j)
                    {
                        children[i].AddAngle(parents[parent2].GetAngle(j));
                    }
                    else
                    {
                        children[i].AddAngle(parents[parent1].GetAngle(j));
                    }
                }

            }
            for (int i = 0; i < population.Count; i++)
                canvas.Children.Remove(population[i].myPath);
            population = children;
            Move();
        }

        private void PrintAdjustment()
        {
            for(int i=0;i<populationAmount;i++)
            {
                AdjustmentFunction(population[i]);
                population[i].PrintAdjustment(canvas);
            }
        }
        public void RemovePrintAdjustment()
        {
            for (int i = 0; i < populationAmount; i++)
            {
                population[i].RemovePrintAdjustment(canvas);
            }
        }

       
    }
}
