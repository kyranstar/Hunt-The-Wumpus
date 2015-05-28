using HuntTheWumpus.SharedCode.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HuntTheWumpus.SharedCode.GUI
{
    /// <summary>
    /// A viewport camera to be used to
    /// calculate the transformation matrix
    /// to be passed into <code>SpriteBatch.Draw()</code>
    /// </summary>
    public class Camera2D
    {
        /// <summary>
        /// The camera's zoom. Higher zoom
        /// means larger objects.
        /// </summary>
        public float Zoom { get; set; }

        /// <summary>
        /// The position of the camera.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// The rotation of the camera.
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// The origin point of the camera
        /// (for rotation and positioning)
        /// </summary>
        public Vector2 Origin { get; set; }

        /// <summary>
        /// The camera's (virtual) viewport size,
        /// before rotation and zoom
        /// </summary>
        public Vector2? VirtualRawViewSize { get; set; }

        /// <summary>
        /// The section of virtual space that can
        /// be seen by this camera.
        /// </summary>
        public Viewport VirtualVisibleViewport
        {
            get
            {
                Viewport Result = new Viewport
                {
                    X = Position.X.ToInt(),
                    Y = Position.Y.ToInt()
                };

                if (VirtualRawViewSize.HasValue)
                {
                    Result.Width = (VirtualRawViewSize.Value.X / Zoom).ToInt();
                    Result.Height = (VirtualRawViewSize.Value.Y / Zoom).ToInt();
                }

                return Result;
            }
        }

        /// <summary>
        /// The graphics device's viewport
        /// </summary>
        public Viewport? RenderViewport { get; set; }


        public Camera2D(Vector2? VirtualViewSize = null, Viewport? RenderViewport = null)
        {
            Zoom = 1f;
            Position = Vector2.Zero;
            Rotation = 0;
            Origin = Vector2.Zero;
            Position = Vector2.Zero;

            VirtualRawViewSize = VirtualViewSize;
            this.RenderViewport = RenderViewport;
        }

        public void Move(Vector2 direction)
        {
            Position += direction;
        }

        public Matrix GetTransform()
        {
            // Calculate each individual transformation matrix
            // (one per property)
            Matrix TranslationMatrix = Matrix.CreateTranslation(new Vector3(Position.X, Position.Y, 0));
            Matrix RotationMatrix = Matrix.CreateRotationZ(Rotation);
            Matrix ZoomMatrix = Matrix.CreateScale(new Vector3(Zoom, Zoom, 1f));
            Matrix OriginMatrix = Matrix.CreateTranslation(new Vector3(Origin.X, Origin.Y, 0));

            // Multiply together each matrix to get the final transformation
            Matrix VirtualTransform = TranslationMatrix * RotationMatrix * ZoomMatrix * OriginMatrix;

            // If we don't have enough info to account for
            // DPI, just return what we have.
            if (!RenderViewport.HasValue || !VirtualRawViewSize.HasValue)
                return VirtualTransform;

            // Add another scale matrix to account for DPI and scaling
            Matrix RenderScaleMatrix = Matrix.CreateScale(new Vector3(
                RenderViewport.Value.Width / VirtualRawViewSize.Value.X,
                RenderViewport.Value.Height / VirtualRawViewSize.Value.Y,
                1f));

            return VirtualTransform * RenderScaleMatrix;

            //TODO: Fix client DPI scaling
        }

        public override string ToString()
        {
            return "Camera2D{" + "Position: " + Position + " Origin: " + Origin + " Zoom: " + Zoom + " Rotation: " + Rotation + "}";
        }
    }
}
