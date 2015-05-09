using HuntTheWumpus.SharedCode.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace HuntTheWumpus.SharedCode.GUI
{
    /// <summary>
    /// A sprite with a texture, position, size, and rotation to be drawn
    /// on a <code>SpriteBatch</code>.
    /// </summary>
    public class TiledTexture
    {
        public const float OpacityThreshold = 0.00001f;

        public float Scale { get; set; }

        public Texture2D Texture { get; set; }
        public float Opacity { get; set; }
        public Color DrawColor { get; set; }

        public bool IsTransparent
        {
            get { return Opacity < OpacityThreshold; }
        }

        private Camera2D Camera;

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
            int ViewportX = Camera.VirtualVisibleViewport.X;
            int ViewportY = Camera.VirtualVisibleViewport.Y;
            
            // TODO: Move layout into update loop
            for (float x = MathUtils.ClosestMultipleLessThan(TextureWidth, ViewportX);
                    x < MathUtils.ClosestMultipleLessThan(TextureWidth, ViewportX + ViewportWidth);x += TextureWidth)
            {
                for (float y = MathUtils.ClosestMultipleLessThan(TextureHeight, ViewportY);
                    y < MathUtils.ClosestMultipleLessThan(TextureHeight, ViewportY + ViewportHeight); y += TextureHeight)
                {
                    Target.Draw(Texture, position: new Vector2(x, y), scale: new Vector2(Scale), color: DrawColor * Opacity);
                }
            }
        }

        public void Update(GameTime Time)
        {
            
        }
    }
}
