using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HuntTheWumpus.SharedCode.GUI
{
    public class Camera2D
    {
        public float Zoom { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }

        /// <summary>
        /// The camera's (virtual) viewport size
        /// </summary>
        public Vector2? VirtualViewSize { get; set; }

        /// <summary>
        /// The graphics device's viewport
        /// </summary>
        public Viewport? RenderViewport { get; set; }


        public Camera2D(Vector2? VirtualViewSize = null, Viewport? RenderViewport = null)
        {
            Zoom = 1;
            Position = Vector2.Zero;
            Rotation = 0;
            Origin = Vector2.Zero;
            Position = Vector2.Zero;

            this.VirtualViewSize = VirtualViewSize;
            this.RenderViewport = RenderViewport;
        }

        public void Move(Vector2 direction)
        {
            Position += direction;
        }

        public Matrix GetTransform()
        {
            Matrix TranslationMatrix = Matrix.CreateTranslation(new Vector3(Position.X, Position.Y, 0));
            Matrix RotationMatrix = Matrix.CreateRotationZ(Rotation);
            Matrix ZoomMatrix = Matrix.CreateScale(new Vector3(Zoom, Zoom, 1));
            Matrix OriginMatrix = Matrix.CreateTranslation(new Vector3(Origin.X, Origin.Y, 0));

            Matrix VirtualTransform = TranslationMatrix * RotationMatrix * ZoomMatrix * OriginMatrix;

            if(!RenderViewport.HasValue || !VirtualViewSize.HasValue)
                return VirtualTransform;

            Matrix RenderScaleMatrix = Matrix.CreateScale( new Vector3(
                RenderViewport.Value.Width / VirtualViewSize.Value.X,
                RenderViewport.Value.Height / VirtualViewSize.Value.Y,
                1f));

            return VirtualTransform * RenderScaleMatrix;
        }

        public override string ToString()
        {
            return "Camera2D{" + "Position: " + Position + " Origin: " + Origin + " Zoom: " + Zoom + " Rotation: " + Rotation + "}";
        }
    }
}
