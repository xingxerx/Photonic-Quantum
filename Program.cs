using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Windows.Threading;
using PrismCollapse3D;

namespace PhotonicQuantumAnimation
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            
            Window window = new Window
            {
                Title = "Photonic Quantum Animation - Quasar Dyson Sphere",
                Width = 1200,
                Height = 800,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Background = Brushes.Black,
                Content = new Grid()
            };

            // Add key handler to exit with Escape or close normally
            window.KeyDown += (sender, e) =>
            {
                if (e.Key == Key.Escape)
                {
                    window.Close();
                }
            };

            // Create the animation
            var animationControl = new QuasarAnimationControl();
            ((Grid)window.Content).Children.Add(animationControl);

            app.Run(window);
        }
    }

    public class QuasarAnimationControl : UserControl
    {
        private QuasarDysonAnimation? quasarAnimation;
        private DispatcherTimer? animationTimer;
        private DateTime lastUpdate;

        public QuasarAnimationControl()
        {
            CreateAnimation();
        }

        private void CreateAnimation()
        {
            // Create main layout
            DockPanel dockPanel = new DockPanel();
            this.Content = dockPanel;

            // Create 3D section - larger for better visibility
            Border border = new Border
            {
                Background = Brushes.Black,
                Width = 900
            };

            Viewport3D viewport3D = new Viewport3D
            {
                Width = 900
            };
            border.Child = viewport3D;
            DockPanel.SetDock(border, Dock.Left);
            dockPanel.Children.Add(border);

            // Create 2D section - smaller sidebar
            Canvas canvas = new Canvas { Width = 300, Background = Brushes.DarkBlue };
            DockPanel.SetDock(canvas, Dock.Right);
            dockPanel.Children.Add(canvas);

            // Add 2D animations
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

                DoubleAnimation move = new DoubleAnimation
                {
                    From = 0,
                    To = 280,
                    Duration = TimeSpan.FromSeconds(2),
                    RepeatBehavior = RepeatBehavior.Forever
                };
                photon.BeginAnimation(Canvas.LeftProperty, move);
            }

            // Initialize 3D animation
            quasarAnimation = new QuasarDysonAnimation(viewport3D);

            // Start timer
            lastUpdate = DateTime.Now;
            animationTimer = new DispatcherTimer();
            animationTimer.Interval = TimeSpan.FromMilliseconds(16);
            animationTimer.Tick += (sender, e) =>
            {
                DateTime now = DateTime.Now;
                double deltaTime = (now - lastUpdate).TotalSeconds;
                lastUpdate = now;
                quasarAnimation?.Update(deltaTime);
            };
            animationTimer.Start();
        }
    }
}