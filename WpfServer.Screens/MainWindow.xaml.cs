using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MouseButton = System.Windows.Input.MouseButton;

namespace WpfServer.Screens
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : StreamableWindow
    {
        private static Brush[] someBrushes;
        private static Dictionary<Brush, string> brushCollection;
        private static readonly Random rand = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        public override void OnMouseAction(MouseAction action, int x, int y)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                var pnt=this.TranslatePoint(new Point(x, y), this.root);
                this.DrawShape(pnt);
            }));

            base.OnMouseAction(action, x, y);
        }

        /// <summary>
        /// Draws the shape.
        /// </summary>
        /// <param name="p">The p.</param>
        private void DrawShape(Point p)
        {
            Shape shape = new System.Windows.Shapes.Rectangle
            {
                Stroke = SystemColors.WindowTextBrush,
                StrokeThickness = 3,
                Fill = GetRandomBrush(),
                //Remove the height to see horizantal shape
                Height = 50,
                //Remove the width to see vertical shape
                Width = 50
                //Remove both to see a point. :-)  Creates a Paint Program!
            };

            root.Children.Add(shape);
            Canvas.SetLeft(shape, p.X - 25);
            Canvas.SetTop(shape, p.Y - 25);
        }

        /// <summary>
        /// Gets the random brush.
        /// </summary>
        /// <returns></returns>
        public static Brush GetRandomBrush()
        {
            brushCollection = new Dictionary<Brush, string> { { Brushes.Red, "Red" },
                                  { Brushes.Blue, "Blue" },
                                  { Brushes.Yellow, "Yellow" },
                                  { Brushes.Green, "Green" },
                                  { Brushes.Purple, "Purple" },
                                  { Brushes.Pink, "Pink" },
                                  { Brushes.Orange, "Orange" },
                                  { Brushes.Tan, "Tan" },
                                  { Brushes.Gray, "Gray" }
                                 };


            someBrushes = new Brush[brushCollection.Count];
            brushCollection.Keys.CopyTo(someBrushes, 0);

            return someBrushes[rand.Next(0, someBrushes.Length)];
        }

        private void MyRectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var f = MyRectangle.Fill;
            MyRectangle.Fill = MyRectangle2.Fill;
            MyRectangle2.Fill = f;
        }
     

     
       
    }
}
