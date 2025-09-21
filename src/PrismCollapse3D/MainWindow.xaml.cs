using System;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows.Controls;

namespace PrismCollapse3D
{
    public partial class MainWindow : Window
    {
        private Core.GameLoop gameLoop;
        // List of objects in the scene (each with its own group and state)
        private class SceneObject
        {
            public Model3DGroup Group = new Model3DGroup();
            public double Angle = 0;
            public bool IsHeld = false;
            public ModelVisual3D Visual = new ModelVisual3D();
        }

        private List<SceneObject> sceneObjects = new List<SceneObject>();
        private SceneObject? heldObject = null;


        public MainWindow() : base()
        {
            InitializeComponent();
            gameLoop = new Core.GameLoop();
            Loaded += MainWindow_Loaded;
            MouseDown += MainWindow_MouseDown;
            MouseUp += MainWindow_MouseUp;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            gameLoop.Setup();
            // Create multiple tesseract objects at different positions
            for (int i = 0; i < 3; i++)
            {
                var obj = new SceneObject();
                AddTesseractProjection(obj.Group, obj.Angle);
                obj.Visual.Content = obj.Group;
                // Offset each object in X for visibility
                obj.Visual.Transform = new TranslateTransform3D(i * 2.5 - 2.5, 0, 0);
                sceneObjects.Add(obj);
                viewport.Children.Add(obj.Visual);
            }
            CompositionTarget.Rendering += GameLoop_Update;
        }

        private void GameLoop_Update(object? sender, EventArgs e)
        {
            gameLoop.Update();
            // Animate all objects except those being held
            foreach (var obj in sceneObjects)
            {
                if (!obj.IsHeld)
                {
                    obj.Angle += 0.01;
                }
                obj.Group.Children.Clear();
                AddTesseractProjection(obj.Group, obj.Angle);
            }
        }

        // Mouse interaction: select/hold nearest object on click
        private void MainWindow_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point mousePos = e.GetPosition(viewport);
            PointHitTestParameters hitParams = new PointHitTestParameters(mousePos);
            heldObject = null;
            VisualTreeHelper.HitTest(viewport, null, result =>
            {
                var mv3d = result.VisualHit as ModelVisual3D;
                if (mv3d != null)
                {
                    foreach (var obj in sceneObjects)
                    {
                        if (obj.Visual == mv3d)
                        {
                            obj.IsHeld = true;
                            heldObject = obj;
                            return HitTestResultBehavior.Stop;
                        }
                    }
                }
                return HitTestResultBehavior.Continue;
            }, hitParams);
        }

        private void MainWindow_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (heldObject != null)
            {
                heldObject.IsHeld = false;
                heldObject = null;
            }
        }

        // Projects a 4D tesseract (hypercube) to 3D and adds it as lines
        private void AddTesseractProjection(Model3DGroup group, double angle)
        {
            // 16 vertices of a tesseract in 4D
            var vertices4D = new System.Numerics.Vector4[16];
            for (int i = 0; i < 16; i++)
            {
                vertices4D[i] = new System.Numerics.Vector4(
                    ((i & 1) == 0 ? -1 : 1),
                    ((i & 2) == 0 ? -1 : 1),
                    ((i & 4) == 0 ? -1 : 1),
                    ((i & 8) == 0 ? -1 : 1)
                );
            }
            // 4D rotation (w-x plane)
            var cosA = Math.Cos(angle);
            var sinA = Math.Sin(angle);
            for (int i = 0; i < 16; i++)
            {
                float x = vertices4D[i].X;
                float w = vertices4D[i].W;
                vertices4D[i].X = (float)(x * cosA - w * sinA);
                vertices4D[i].W = (float)(x * sinA + w * cosA);
            }
            // Project 4D to 3D (ignore w, or use perspective)
            var vertices3D = new Point3D[16];
            for (int i = 0; i < 16; i++)
            {
                double w = 2 + vertices4D[i].W; // perspective
                vertices3D[i] = new Point3D(
                    vertices4D[i].X / w,
                    vertices4D[i].Y / w,
                    vertices4D[i].Z / w
                );
            }
            // Draw edges (32 per tesseract)
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int k = i ^ (1 << j);
                    if (i < k)
                    {
                        AddEdge(group, vertices3D[i], vertices3D[k]);
                    }
                }
            }
        }

        // Adds a line between two 3D points
        private void AddEdge(Model3DGroup group, Point3D p1, Point3D p2)
        {
            var mb = new MeshGeometry3D();
            // Simple line as a thin box (for visibility)
            double thickness = 0.02;
            var dir = p2 - p1;
            var len = dir.Length;
            dir.Normalize();
            var up = new Vector3D(0, 1, 0);
            var right = Vector3D.CrossProduct(dir, up);
            if (right.Length == 0) right = new Vector3D(1, 0, 0);
            right.Normalize();
            up = Vector3D.CrossProduct(right, dir);
            up.Normalize();
            // 8 vertices of a box
            var corners = new Point3D[8];
            for (int i = 0; i < 8; i++)
            {
                double s1 = ((i & 1) == 0 ? -1 : 1) * thickness / 2;
                double s2 = ((i & 2) == 0 ? -1 : 1) * thickness / 2;
                double s3 = ((i & 4) == 0 ? 0 : len);
                corners[i] = p1 + right * s1 + up * s2 + dir * s3;
            }
            // 12 triangles for box
            int[] tris = {0,1,2, 1,3,2, 0,2,4, 2,6,4, 1,5,3, 3,5,7, 4,6,5, 4,5,0, 2,3,6, 3,7,6, 0,5,1, 0,4,5};
            for (int i = 0; i < tris.Length; i += 3)
            {
                mb.Positions.Add(corners[tris[i]]);
                mb.Positions.Add(corners[tris[i+1]]);
                mb.Positions.Add(corners[tris[i+2]]);
                mb.TriangleIndices.Add(i);
                mb.TriangleIndices.Add(i+1);
                mb.TriangleIndices.Add(i+2);
            }
            var mat = new DiffuseMaterial(new SolidColorBrush(Colors.Cyan));
            group.Children.Add(new GeometryModel3D(mb, mat));
        }
        }
    }