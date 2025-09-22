using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Controls;

namespace PrismCollapse3D
{
    public class QuasarDysonAnimation
    {
        private Model3DGroup scene;
        private Viewport3D viewport;
        private double time;

        public QuasarDysonAnimation(Viewport3D viewport)
        {
            this.viewport = viewport;
            scene = new Model3DGroup();
            time = 0;
            InitializeScene();
        }

        private void InitializeScene()
        {
            // Quasar: Central point light for intense emission
            PointLight quasarLight = new PointLight
            {
                Color = Colors.White,
                Position = new Point3D(0, 0, 0),
                Range = 100,
                ConstantAttenuation = 0.1,
                LinearAttenuation = 0.01
            };

            // Dyson Sphere: Simplified wireframe sphere
            MeshGeometry3D sphereMesh = CreateSphereMesh(5, 20, 20);
            GeometryModel3D dysonSphere = new GeometryModel3D
            {
                Geometry = sphereMesh,
                Material = new DiffuseMaterial(new SolidColorBrush(Color.FromArgb(50, 255, 255, 255)))
            };

            // Jets: Two conical jets along Z-axis
            MeshGeometry3D jetMesh1 = CreateConeMesh(0.5, 10, 10, new Vector3D(0, 0, 1));
            MeshGeometry3D jetMesh2 = CreateConeMesh(0.5, 10, 10, new Vector3D(0, 0, -1));
            GeometryModel3D jet1 = new GeometryModel3D
            {
                Geometry = jetMesh1,
                Material = new DiffuseMaterial(new SolidColorBrush(Color.FromArgb(100, 255, 165, 0)))
            };
            GeometryModel3D jet2 = new GeometryModel3D
            {
                Geometry = jetMesh2,
                Material = new DiffuseMaterial(new SolidColorBrush(Color.FromArgb(100, 255, 165, 0)))
            };

            scene.Children.Add(quasarLight);
            scene.Children.Add(dysonSphere);
            scene.Children.Add(jet1);
            scene.Children.Add(jet2);

            ModelVisual3D modelVisual = new ModelVisual3D { Content = scene };
            viewport.Children.Add(modelVisual);

            // Camera setup
            PerspectiveCamera camera = new PerspectiveCamera
            {
                Position = new Point3D(0, 0, 20),
                LookDirection = new Vector3D(0, 0, -1),
                FieldOfView = 60
            };
            viewport.Camera = camera;
        }

        private MeshGeometry3D CreateSphereMesh(double radius, int slices, int stacks)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            Point3DCollection positions = new Point3DCollection();
            Int32Collection indices = new Int32Collection();

            for (int stack = 0; stack <= stacks; stack++)
            {
                double phi = Math.PI * stack / stacks;
                for (int slice = 0; slice <= slices; slice++)
                {
                    double theta = 2 * Math.PI * slice / slices;
                    double x = radius * Math.Sin(phi) * Math.Cos(theta);
                    double y = radius * Math.Sin(phi) * Math.Sin(theta);
                    double z = radius * Math.Cos(phi);
                    positions.Add(new Point3D(x, y, z));
                }
            }

            for (int stack = 0; stack < stacks; stack++)
            {
                for (int slice = 0; slice < slices; slice++)
                {
                    int top = stack * (slices + 1) + slice;
                    int topRight = top + 1;
                    int bottom = (stack + 1) * (slices + 1) + slice;
                    int bottomRight = bottom + 1;

                    indices.Add(top);
                    indices.Add(bottom);
                    indices.Add(topRight);
                    indices.Add(topRight);
                    indices.Add(bottom);
                    indices.Add(bottomRight);
                }
            }

            mesh.Positions = positions;
            mesh.TriangleIndices = indices;
            return mesh;
        }

        private MeshGeometry3D CreateConeMesh(double radius, double height, int sides, Vector3D direction)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            Point3DCollection positions = new Point3DCollection();
            Int32Collection indices = new Int32Collection();

            // Base center
            positions.Add(new Point3D(0, 0, 0));
            // Apex
            positions.Add(new Point3D(direction.X * height, direction.Y * height, direction.Z * height));

            // Base vertices
            for (int i = 0; i < sides; i++)
            {
                double theta = 2 * Math.PI * i / sides;
                double x = radius * Math.Cos(theta);
                double y = radius * Math.Sin(theta);
                positions.Add(new Point3D(x, y, 0));
            }

            // Base faces
            for (int i = 0; i < sides; i++)
            {
                int next = (i + 1) % sides;
                indices.Add(0);
                indices.Add(2 + next);
                indices.Add(2 + i);
            }

            // Side faces
            for (int i = 0; i < sides; i++)
            {
                int next = (i + 1) % sides;
                indices.Add(1);
                indices.Add(2 + i);
                indices.Add(2 + next);
            }

            mesh.Positions = positions;
            mesh.TriangleIndices = indices;
            return mesh;
        }

        public void Update(double deltaTime)
        {
            time += deltaTime;
            // Rotate Dyson sphere
            RotateTransform3D rotation = new RotateTransform3D(
                new AxisAngleRotation3D(new Vector3D(0, 1, 0), time * 30));
            scene.Children[1].Transform = rotation; // Dyson sphere
            // Pulse jets
            double pulse = Math.Sin(time * 2) * 0.2 + 1;
            scene.Children[2].Transform = new ScaleTransform3D(1, 1, pulse); // Jet1
            scene.Children[3].Transform = new ScaleTransform3D(1, 1, pulse); // Jet2
        }
    }
}