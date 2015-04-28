using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.GameMap;
using HuntTheWumpus.SharedCode.GUI;
using HuntTheWumpus.SharedCode.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace HuntTheWumpus.SharedCode.Scenes
{
    class GameScene : Scene
    {
        Map Map;
        MapRenderer MapRenderer;
        MapInputHandler InputHandler;
        GraphicsDevice Graphics;

        SpriteFont UIFont;

        private FrameCounter FramerateCounter = new FrameCounter();
        SpriteBatch InfoOverlay;

#if DESKTOP
        GodManager God;
#endif

        public override void LoadContent(ContentManager Content)
        {
            Map = new Map();
            InputHandler = new MapInputHandler(Map);
            MapRenderer = new MapRenderer(Map);
            MapRenderer.LoadContent(Content);

            UIFont = Content.Load<SpriteFont>("Segoe_UI_9_Regular");

        }

        public override void Initialize(GraphicsDevice GraphicsDevice)
        {
            this.Graphics = GraphicsDevice;

#if DESKTOP
            God = new GodManager(Map, MapRenderer);

            new Task(God.Initialize).Start();
#endif
            InfoOverlay = new SpriteBatch(Graphics);
            MapRenderer.Initialize(GraphicsDevice);
            
            // Ideally, the Map should have a reset method
            // TODO: Reset map here

            Map.Cave.RegenerateLayout();
        }

        public override void Update(GameTime GameTime)
        {
            InputHandler.Update(GameTime);
            MapRenderer.Update(GameTime);
        }

        public override void Draw(GameTime GameTime)
        {
            MapRenderer.Draw(GameTime);


            InfoOverlay.Begin();

            FramerateCounter.Update((float)GameTime.ElapsedGameTime.TotalSeconds);

            // Render general info
            var FPS = string.Format("FPS: {0}", FramerateCounter.AverageFramesPerSecond);
            var Particles = string.Format("Fog particles: {0}", MapRenderer.FogParticleCount);
            InfoOverlay.DrawString(UIFont, FPS, new Vector2(5, 1), Color.Black);
            InfoOverlay.DrawString(UIFont, Particles, new Vector2(5, 11), Color.Black);

            // Render room status
            string Room = Map.Cave[Map.PlayerRoom].ToString();
            InfoOverlay.DrawString(UIFont, Room, new Vector2(5, 21), Color.Black);

            InfoOverlay.End();
        }

        public override void UnloadContent()
        {

        }

        public override void Uninitialize()
        {

        }
    }
}
