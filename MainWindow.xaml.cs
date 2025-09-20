using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PhotonicQuantumAnimation
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateAnimation();
        }

        private void CreateAnimation()
        {
            Canvas canvas = new Canvas { Width = 800, Height = 400, Background = Brushes.Black };
            
            // Add canvas to the main grid
            Grid mainGrid = (Grid)Content;
            mainGrid.Children.Add(canvas);

            // Photonic pulses (light particles)
            for (int i = 0; i < 5; i++)
            {
                Ellipse photon = new Ellipse
                {
                    Width = 10,
                    Height = 10,
                    Fill = Brushes.Cyan,
                    Opacity = 0.8
                };
                Canvas.SetLeft(photon, 0);
                Canvas.SetTop(photon, 50 + i * 60);
                canvas.Children.Add(photon);

                // Animate photon movement
                DoubleAnimation move = new DoubleAnimation
                {
                    From = 0,
                    To = 780,
                    Duration = TimeSpan.FromSeconds(2),
                    RepeatBehavior = RepeatBehavior.Forever
                };
                photon.BeginAnimation(Canvas.LeftProperty, move);
            }

            // Quantum qubits (spinning shapes)
            for (int i = 0; i < 3; i++)
            {
                Rectangle qubit = new Rectangle
                {
                    Width = 20,
                    Height = 20,
                    Fill = Brushes.Red,
                    Opacity = 0.9
                };
                Canvas.SetLeft(qubit, 350);
                Canvas.SetTop(qubit, 100 + i * 80);
                canvas.Children.Add(qubit);

                // Rotate qubit
                RotateTransform rotate = new RotateTransform();
                qubit.RenderTransform = rotate;
                qubit.RenderTransformOrigin = new Point(0.5, 0.5);
                DoubleAnimation spin = new DoubleAnimation
                {
                    From = 0,
                    To = 360,
                    Duration = TimeSpan.FromSeconds(1),
                    RepeatBehavior = RepeatBehavior.Forever
                };
                rotate.BeginAnimation(RotateTransform.AngleProperty, spin);

                // Color change (quantum state)
                ColorAnimation color = new ColorAnimation
                {
                    From = Colors.Red,
                    To = Colors.Blue,
                    Duration = TimeSpan.FromSeconds(1.5),
                    AutoReverse = true,
                    RepeatBehavior = RepeatBehavior.Forever
                };
                qubit.Fill.BeginAnimation(SolidColorBrush.ColorProperty, color);
            }
        }
    }
}
