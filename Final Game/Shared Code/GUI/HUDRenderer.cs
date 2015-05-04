using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.GameMap;
using HuntTheWumpus.SharedCode.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HuntTheWumpus.SharedCode.GUI
{
    public class HudRenderer
    {
        private GraphicsDevice Graphics;
        private SpriteBatch HudRenderTarget;

        private SpriteFont UIFont;

        private FrameCounter FramerateCounter = new FrameCounter();

        private MapRenderer MapRanderer;
        private Map Map;

        public HudRenderer(MapRenderer MapRenderer, Map Map)
        {
            this.MapRanderer = MapRenderer;
            this.Map = Map;
        }

        /// <summary>
        /// Initializes the renderer for a new game.
        /// </summary>
        /// <param name="Graphics"></param>
        public void Initialize(GraphicsDevice Graphics)
        {
            HudRenderTarget = new SpriteBatch(Graphics);
            this.Graphics = Graphics;
        }

        /// <summary>
        /// Loads the media content (should be called ONCE).
        /// </summary>
        /// <param name="Content"></param>
        public void LoadContent(ContentManager Content)
        {
            UIFont = Content.Load<SpriteFont>("Segoe_UI_9_Regular");
        }

        /// <summary>
        /// Updates the state of the map renerer to prepare for drawing.
        /// </summary>
        public void Update(GameTime time)
        {
            
        }

        /// <summary>
        /// Draws the HUD.
        /// </summary>
        /// <param name="GameTime"></param>
        public void Draw(GameTime GameTime)
        {
            HudRenderTarget.Begin();

            FramerateCounter.Update((float)GameTime.ElapsedGameTime.TotalSeconds);

            // Render general info
            var FPS = string.Format("FPS: {0}", FramerateCounter.AverageFramesPerSecond);
            var Particles = string.Format("Fog particles: {0}", MapRanderer.FogParticleCount);
            HudRenderTarget.DrawString(UIFont, FPS, new Vector2(5, 1), Color.Black);
            HudRenderTarget.DrawString(UIFont, Particles, new Vector2(5, 11), Color.Black);

            // Render room status
            string Room = Map.Cave[Map.PlayerRoom].ToString();
            HudRenderTarget.DrawString(UIFont, Room, new Vector2(5, 21), Color.Black);

            HudRenderTarget.End();
        }
    }
}
