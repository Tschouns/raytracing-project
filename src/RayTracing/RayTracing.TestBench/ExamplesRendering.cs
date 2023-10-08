using RayTracing.Base;
using RayTracing.CanvasClient;
using RayTracing.Math;
using RayTracing.ModelFiles.ColladaFormat;
using RayTracing.ModelFiles.ObjFormat;
using RayTracing.Rendering;

namespace RayTracing.TestBench
{
    public static class ExamplesRendering
    {
        public static void Camera(ICanvas canvas)
        {
            Argument.AssertNotNull(canvas, nameof(canvas));

            // Create a "model".
            var v1 = new Vector3(1, 0, -1);
            var v2 = new Vector3(3, 0, -2);
            var v3 = new Vector3(4, 0, 0);
            var v4 = new Vector3(2, 0, 1);
            var v5 = new Vector3(1, 2, -1);
            var v6 = new Vector3(3, 2, -2);
            var v7 = new Vector3(4, 2, 0);
            var v8 = new Vector3(2, 2, 1);

            var triangles = new List<Triangle3D>
            {
                new Triangle3D(v1, v2, v5),
                new Triangle3D(v2, v3, v6),
                new Triangle3D(v3, v4, v7),
                new Triangle3D(v4, v1, v8),
            };

            // Load a model.
            //var parser = new ObjFileParser();
            var parser = new ColladaFileParser();
            //var scene = parser.LoadFromFile(@"..\..\..\..\..\..\models\plant\indoor plant_02.obj");
            var scene = parser.LoadFromFile(@"..\..\..\..\..\..\models\dummy\dummy.dae");

            // Setup camera.
            ushort resX = 300;
            ushort resY = 300;

            var camera = new Camera(resX, resY);
            camera.Position = new Vector3(0, 1, -4f);
            camera.LookingDirection = new Vector3(0, 0, 2);
            camera.FocalLength /= 2.5f;

            // Setup canvas.
            canvas.Size(resX, resY);

            // Render image.
            var canvasTarget = new CanvasRenderTarget(canvas);
            var renderer = new SimpleRenderer();

            while (true)
            {
                var rotate = Matrix4x4.RotateY(0.523599f);
                //rotate = Matrix4x4.RotateX(0.2f).Multiply(rotate);

                foreach (var g in scene.Geometries)
                {
                    g.Transform(rotate);
                }
                renderer.Render(scene, camera, canvasTarget);
            }
        }
    }
}
