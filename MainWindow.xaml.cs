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
    public class MainWindow : Window
    {
        private QuasarDysonAnimation? quasarAnimation;
        private DispatcherTimer? animationTimer;
        private DateTime lastUpdate;

        public MainWindow()
        {
            // Manually set up the window since InitializeComponent isn't working
            this.Title = "Photonic Quantum Animation - Quasar Dyson Sphere";
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
            this.Background = Brushes.Black;
            this.Content = new Grid();
            
            // Add key handler to exit fullscreen with Escape
            this.KeyDown += MainWindow_KeyDown;
            
            CreateAnimation();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void CreateAnimation()
        {
            // Create main grid with two sections
            Grid mainGrid = (Grid)Content;
            
            // Create a dock panel to organize layout - now full screen
            DockPanel dockPanel = new DockPanel();
            mainGrid.Children.Add(dockPanel);
            
            // Create 3D Quasar animation section - takes up most of the screen
            Create3DQuasarAnimation(dockPanel);
            
            // Create 2D photonic animation section - smaller sidebar
            Create2DPhotonicAnimation(dockPanel);
            
            // Start animation timer
            lastUpdate = DateTime.Now;
            animationTimer = new DispatcherTimer();
            animationTimer.Interval = TimeSpan.FromMilliseconds(16); // ~60 FPS
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
        }

        private void Create3DQuasarAnimation(DockPanel parent)
        {
            // Create a border to provide background for 3D viewport - much larger now
            Border border = new Border
            {
                Background = Brushes.Black
            };
            
            // Create 3D viewport for Quasar animation - fills most of screen
            Viewport3D viewport3D = new Viewport3D();
            
            border.Child = viewport3D;
            DockPanel.SetDock(border, Dock.Left);
            parent.Children.Add(border);
            
            // Initialize Quasar-Dyson animation
            quasarAnimation = new QuasarDysonAnimation(viewport3D);
        }

        private void Create2DPhotonicAnimation(DockPanel parent)
        {
            Canvas canvas = new Canvas { Width = 300, Background = Brushes.Black };
            DockPanel.SetDock(canvas, Dock.Right);
            parent.Children.Add(canvas);

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
                // Create a new SolidColorBrush for each qubit so it can be animated
                SolidColorBrush qubitBrush = new SolidColorBrush(Colors.Red);
                Rectangle qubit = new Rectangle
                {
                    Width = 20,
                    Height = 20,
                    Fill = qubitBrush,
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
                qubitBrush.BeginAnimation(SolidColorBrush.ColorProperty, color);
            }
        }

        private void AnimationTimer_Tick(object? sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            double deltaTime = (now - lastUpdate).TotalSeconds;
            lastUpdate = now;
            
            // Update 3D Quasar animation
            quasarAnimation?.Update(deltaTime);
        }
    }
}
