using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HuntTheWumpus.SharedCode.GUI
{
    /// <summary>
    /// A sprite with a texture, position, size, and rotation to be drawn
    /// on a <code>SpriteBatch</code>.
    /// </summary>
    public class Sprite2D
    {
        public int RenderWidth { get; set; }
        public int RenderHeight { get; set; }

        public float HalfWidth { get { return RenderWidth / 2f; } }
        public float HalfHeight { get { return RenderHeight / 2f; } }

        // Use integers for now (could use a Vector2 instead)
        public int RenderX { get; set; }
        public int RenderY { get; set; }

        public float Rotation { get; set; }

        public Texture2D Texture { get; set; }


        public Vector2 Position
        {
            get { return new Vector2(RenderX, RenderY); }
            set { RenderX = (int)(value.X); RenderY = (int)(value.Y); }
        }

        public Rectangle TargetArea
        {
            get
            {
                return new Rectangle(RenderX, RenderY, RenderWidth, RenderHeight);
            }

            set
            {
                RenderWidth = value.Width;
                RenderHeight = value.Height;
                RenderX = value.X;
                RenderY = value.Y;
            }
        }

        public Sprite2D(int X, int Y, int Width, int Height, float Rotation, Texture2D Texture)
        {
            this.RenderX = X;
            this.RenderY = Y;

            this.RenderWidth = Width;
            this.RenderHeight = Height;

            this.Rotation = Rotation;

            this.Texture = Texture;
        }

        public Sprite2D(Rectangle Target, float Rotation, Texture2D Texture)
        {
            this.TargetArea = Target;
            this.Rotation = Rotation;
            this.Texture = Texture;
        }

        public Sprite2D(Texture2D Texture)
        {
            this.Texture = Texture;
        }

        public void Draw(SpriteBatch Target)
        {
            if (Texture != null)
                Target.Draw(Texture, destinationRectangle: TargetArea, rotation: Rotation);
        }

        public void Reset()
        {
            RenderWidth = RenderHeight = 0;
            RenderX = RenderY = 0;
            Rotation = 0f;

            Texture = null;
        }
    }
}
