using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.GameMap;
using HuntTheWumpus.SharedCode.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using EmptyKeys.UserInterface.Generated;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HuntTheWumpus.SharedCode.GUI
{
    public class ArcadeFrame
    {
        // Render pipeline
        private GraphicsDevice Graphics;
        private SpriteBatch Target;

        Texture2D LeftFrameTexture, RightFrameTexture, MiddleFrameTexture;

        private float LeftFrameScale, RightFrameScale;

        public ArcadeFrame()
        {

        }

        /// <summary>
        /// Initializes the renderer for a new game.
        /// </summary>
        /// <param name="Graphics"></param>
        public void Initialize(GraphicsDevice Graphics)
        {
            this.Graphics = Graphics;
            Target = new SpriteBatch(Graphics);
        }

        /// <summary>
        /// Loads the media content (should be called ONCE).
        /// </summary>
        /// <param name="Content"></param>
        public void LoadContent(ContentManager Content)
        {
            LeftFrameTexture = Content.Load<Texture2D>("Images/Arcade left");
            RightFrameTexture = Content.Load<Texture2D>("Images/Arcade right");
            MiddleFrameTexture = Content.Load<Texture2D>("Images/Arcade middle");

            LeftFrameScale = LeftFrameTexture.Width / LeftFrameTexture.Height;
            RightFrameScale = RightFrameTexture.Width / RightFrameTexture.Height;
        }

        /// <summary>
        /// Updates the state of the map renerer to prepare for drawing.
        /// </summary>
        public void Update(GameTime Time)
        {

        }

        /// <summary>
        /// Draws the arcade frame.
        /// </summary>
        public void Draw()
        {
            Target.Begin();

            /*Target.Draw(MiddleFrameTexture, destinationRectangle: new Rectangle()
            {
                Width = Graphics.Viewport.Width,
                Height = Graphics.Viewport.Height
            });*/

            Target.Draw(LeftFrameTexture, destinationRectangle: new Rectangle()
                {
                    Width = (LeftFrameScale * Graphics.Viewport.Height).ToInt(),
                    Height = Graphics.Viewport.Height
                });

            Target.Draw(RightFrameTexture, destinationRectangle: new Rectangle()
            {
                Width = (RightFrameScale * Graphics.Viewport.Height).ToInt(),
                Height = Graphics.Viewport.Height,
                X = Graphics.Viewport.Width - (RightFrameScale * Graphics.Viewport.Height).ToInt()
            });


            Target.End();
        }
    }
}
