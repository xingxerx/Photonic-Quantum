using System.Windows;

namespace PhotonicQuantumAnimation
{
    public partial class App : Application
    {
        [System.STAThreadAttribute()]
        public static void Main()
        {
            PhotonicQuantumAnimation.App app = new PhotonicQuantumAnimation.App();
            app.Run(new MainWindow());
        }
    }
}