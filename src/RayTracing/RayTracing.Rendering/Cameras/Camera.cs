﻿using RayTracing.Math;
using RayTracing.Rendering.Rays;

namespace RayTracing.Rendering.Cameras
{
    /// <summary>
    /// Represents a camera which is placed in space, and produces the rays.
    /// </summary>
    public class Camera : ICameraFixedProperties, ICameraSettings, ICamera
    {
        private static readonly Vector2 defaultSize = new Vector2(0.008f, 0.006f);
        private static readonly ushort defaultHorizontalResolution = 800;
        private static readonly ushort defaultVerticalResolution = 600;
        private static readonly float defaultFocalLength = 0.01f;
        private static readonly Vector3 defaultPosition = new Vector3(0, 0, -10);
        private static readonly Vector3 defaultLookingDirection = new Vector3(0, 0, 1);

        public Camera()
        {
        }

        public Camera(
            ushort horizontalResolution,
            ushort verticalResolution)
        {
            HorizontalResolution = horizontalResolution;
            VerticalResolution = verticalResolution;

            var ratio = (float)horizontalResolution / VerticalResolution;
            Size = new Vector2(
                defaultSize.Y * ratio,
                defaultSize.Y);

            FocalLength = defaultFocalLength;
        }

        public Camera(
            Vector2 size,
            ushort horizontalResolution,
            ushort verticalResolution,
            float focalLength,
            Vector3 position,
            Vector3 lookingDirection)
        {
            Size = size;
            HorizontalResolution = horizontalResolution;
            VerticalResolution = verticalResolution;

            FocalLength = focalLength;
            Position = position;
            LookingDirection = lookingDirection;
        }

        public Vector2 Size { get; } = defaultSize;
        public ushort HorizontalResolution { get; } = defaultHorizontalResolution;
        public ushort VerticalResolution { get; } = defaultVerticalResolution;

        public float FocalLength { get; set; } = defaultFocalLength;
        public Vector3 Position { get; set; } = defaultPosition;
        public Vector3 LookingDirection { get; set; } = defaultLookingDirection;

        public IEnumerable<PixelRay> GeneratePixelRays()
        {
            if (LookingDirection.LengthSquared() == 0)
            {
                throw new InvalidOperationException("The looking direction vector has length 0.");
            }

            // Find raster X and Y directions -- already flipped to account for the "lense".
            var rasterDirectionX = LookingDirection.Cross(Vector3.Up).Norm()!.Value;
            var rasterDirectionY = rasterDirectionX.Cross(LookingDirection).Norm()!.Value;

            // Scale to the right size.
            var rasterVectorX = rasterDirectionX * Size.X;
            var rasterVectorY = rasterDirectionY * Size.Y;

            // Determine raster origin.
            var rasterCenter = Position - LookingDirection.Norm()!.Value * FocalLength;
            var rasterOrigin = rasterCenter - rasterVectorX / 2 - rasterVectorY / 2;

            // Rasterize...
            var stepX = Size.X / HorizontalResolution;
            var stepY = Size.Y / VerticalResolution;

            var pixelRays = new List<PixelRay>();

            for (var x = 0; x < HorizontalResolution; x++)
            {
                for (var y = 0; y < VerticalResolution; y++)
                {
                    var rasterPos = rasterOrigin
                        + rasterDirectionX * x * stepX
                        + rasterDirectionY * y * stepY;

                    var ray = new Ray(
                        origin: Position,
                        direction: (Position - rasterPos).Norm()!.Value);

                    pixelRays.Add(new PixelRay(ray, x, y));
                }
            }

            return pixelRays;
        }
    }
}
