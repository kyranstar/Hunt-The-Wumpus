using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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
        public Vector2? VirtualRawViewSize { get; set; }

        public Viewport VirtualVisibleViewport
        {
            get
            {
                Viewport Result = new Viewport()
                {
                    X = (int)Math.Round(Position.X),
                    Y = (int)Math.Round(Position.Y)
                };

                if (VirtualRawViewSize.HasValue)
                {
                    Result.Width = (int)Math.Round(VirtualRawViewSize.Value.X / Zoom);
                    Result.Height = (int)Math.Round(VirtualRawViewSize.Value.Y / Zoom);
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
            Zoom = 1;
            Position = Vector2.Zero;
            Rotation = 0;
            Origin = Vector2.Zero;
            Position = Vector2.Zero;

            this.VirtualRawViewSize = VirtualViewSize;
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
            
            return VirtualTransform;

            //TODO: Fix client DPI scaling
        }

        public override string ToString()
        {
            return "Camera2D{" + "Position: " + Position + " Origin: " + Origin + " Zoom: " + Zoom + " Rotation: " + Rotation + "}";
        }
    }
}
