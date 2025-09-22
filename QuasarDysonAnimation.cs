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
            // Ambient light for better visibility
            AmbientLight ambientLight = new AmbientLight
            {
                Color = Color.FromRgb(30, 30, 30)
            };

            // Quasar: Central point light for intense emission
            PointLight quasarLight = new PointLight
            {
                Color = Colors.White,
                Position = new Point3D(0, 0, 0),
                Range = 100,
                ConstantAttenuation = 0.1,
                LinearAttenuation = 0.01
            };

            // Additional directional light for better visibility
            DirectionalLight directionalLight = new DirectionalLight
            {
                Color = Color.FromRgb(100, 100, 150),
                Direction = new Vector3D(-1, -1, -1)
            };

            // Dyson Sphere: More visible with emission material
            MeshGeometry3D sphereMesh = CreateSphereMesh(5, 20, 20);
            GeometryModel3D dysonSphere = new GeometryModel3D
            {
                Geometry = sphereMesh,
                Material = new DiffuseMaterial(new SolidColorBrush(Color.FromArgb(100, 0, 255, 255))),
                BackMaterial = new DiffuseMaterial(new SolidColorBrush(Color.FromArgb(50, 255, 255, 255)))
            };

            // Jets: Brighter and more visible
            MeshGeometry3D jetMesh1 = CreateConeMesh(0.5, 10, 10, new Vector3D(0, 0, 1));
            MeshGeometry3D jetMesh2 = CreateConeMesh(0.5, 10, 10, new Vector3D(0, 0, -1));
            GeometryModel3D jet1 = new GeometryModel3D
            {
                Geometry = jetMesh1,
                Material = new DiffuseMaterial(new SolidColorBrush(Color.FromArgb(200, 255, 100, 0)))
            };
            GeometryModel3D jet2 = new GeometryModel3D
            {
                Geometry = jetMesh2,
                Material = new DiffuseMaterial(new SolidColorBrush(Color.FromArgb(200, 255, 100, 0)))
            };

            // Central core - visible quasar representation
            MeshGeometry3D coreMesh = CreateSphereMesh(0.5, 10, 10);
            GeometryModel3D quasarCore = new GeometryModel3D
            {
                Geometry = coreMesh,
                Material = new DiffuseMaterial(new SolidColorBrush(Colors.White))
            };

            scene.Children.Add(ambientLight);
            scene.Children.Add(quasarLight);
            scene.Children.Add(directionalLight);
            scene.Children.Add(dysonSphere);
            scene.Children.Add(jet1);
            scene.Children.Add(jet2);
            scene.Children.Add(quasarCore);

            ModelVisual3D modelVisual = new ModelVisual3D { Content = scene };
            viewport.Children.Add(modelVisual);

            // Camera setup - better positioning
            PerspectiveCamera camera = new PerspectiveCamera
            {
                Position = new Point3D(0, 0, 15),
                LookDirection = new Vector3D(0, 0, -1),
                UpDirection = new Vector3D(0, 1, 0),
                FieldOfView = 60
            };
            viewport.Camera = camera;
        }

        private MeshGeometry3D CreateSphereMesh(double radius, int slices, int stacks)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            Point3DCollection positions = new Point3DCollection();
            Int32Collection indices = new Int32Collection();
            PointCollection textureCoords = new PointCollection();

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
                    
                    // Add texture coordinates
                    textureCoords.Add(new Point((double)slice / slices, (double)stack / stacks));
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

                    // First triangle
                    indices.Add(top);
                    indices.Add(bottom);
                    indices.Add(topRight);
                    
                    // Second triangle
                    indices.Add(topRight);
                    indices.Add(bottom);
                    indices.Add(bottomRight);
                }
            }

            mesh.Positions = positions;
            mesh.TriangleIndices = indices;
            mesh.TextureCoordinates = textureCoords;
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
            
            // Rotate Dyson sphere (index 3 - after ambient, point, directional lights)
            RotateTransform3D sphereRotation = new RotateTransform3D(
                new AxisAngleRotation3D(new Vector3D(0, 1, 0), time * 30));
            scene.Children[3].Transform = sphereRotation; // Dyson sphere
            
            // Pulse jets (indices 4 and 5)
            double pulse = Math.Sin(time * 2) * 0.3 + 1;
            ScaleTransform3D jetScale1 = new ScaleTransform3D(1, 1, pulse);
            ScaleTransform3D jetScale2 = new ScaleTransform3D(1, 1, pulse);
            scene.Children[4].Transform = jetScale1; // Jet1
            scene.Children[5].Transform = jetScale2; // Jet2
            
            // Pulsate quasar core (index 6)
            double corePulse = Math.Sin(time * 4) * 0.2 + 1;
            ScaleTransform3D coreScale = new ScaleTransform3D(corePulse, corePulse, corePulse);
            scene.Children[6].Transform = coreScale; // Quasar core
        }
    }
}