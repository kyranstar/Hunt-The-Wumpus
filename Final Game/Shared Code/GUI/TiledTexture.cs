using HuntTheWumpus.SharedCode.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HuntTheWumpus.SharedCode.GUI
{
    /// <summary>
    /// A sprite with a texture, position, size, and rotation to be drawn
    /// on a <code>SpriteBatch</code>.
    /// </summary>
    public class TiledTexture
    {
        /// <summary>
        /// Any opacity less than this is considered 0.
        /// </summary>
        public const float OpacityThreshold = 0.00001f;

        public float Scale { get; set; }

        public Texture2D Texture { get; set; }
        public float Opacity { get; set; }
        public Color DrawColor { get; set; }

        /// <summary>
        /// Returns whether the texture is fully transparent
        /// </summary>
        public bool IsTransparent
        {
            get { return Opacity < OpacityThreshold; }
        }

        private readonly Camera2D Camera;

        public TiledTexture(
            Texture2D Texture,
            Camera2D Camera,
            float Scale = 1,
            float Opacity = 1,
            Color? DrawColor = null)
        {
            this.Scale = Scale;

            this.Opacity = Opacity;
            this.Texture = Texture;
            this.DrawColor = DrawColor ?? Color.White;

            this.Camera = Camera;
        }

        public virtual void Initialize()
        {
            // Don't do anything
        }

        public void Draw(SpriteBatch Target)
        {
            float TextureWidth = Texture.Width * Scale;
            float TextureHeight = Texture.Height * Scale;

            int ViewportWidth = Camera.VirtualVisibleViewport.Width;
            int ViewportHeight = Camera.VirtualVisibleViewport.Height;
            int ViewportX = -Camera.VirtualVisibleViewport.X;
            int ViewportY = -Camera.VirtualVisibleViewport.Y;

            Rectangle TextureSource = new Rectangle
            {
                Width = ViewportWidth,
                Height = ViewportHeight,
                X = (ViewportX % TextureWidth).ToInt(),
                Y = (ViewportY % TextureHeight).ToInt()
            };

            Rectangle TextureDest = new Rectangle
            {
                X = ViewportX,
                Y = ViewportY,
                Width = ViewportWidth,
                Height = ViewportHeight
            };

            Target.Draw(Texture, destinationRectangle: TextureDest, scale: new Vector2(Scale), color: DrawColor * Opacity, sourceRectangle: TextureSource);
        }

        public void Update(GameTime Time)
        {
            
        }
    }
}
