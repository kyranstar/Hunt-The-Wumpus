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
        HudRenderer HUD;

#if DESKTOP
        GodManager God;
#endif

        public override void LoadContent(ContentManager Content)
        {
            Map = new Map();

            InputHandler = new MapInputHandler(Map);

            MapRenderer = new MapRenderer(Map);
            MapRenderer.LoadContent(Content);

            HUD = new HudRenderer(MapRenderer, Map);
            HUD.LoadContent(Content);

        }

        public override void Initialize(GraphicsDevice GraphicsDevice)
        {
            this.Graphics = GraphicsDevice;

#if DESKTOP
            God = new GodManager(Map, MapRenderer);
            // Run god console on separate thread
            new Task(God.Initialize).Start();
#endif
            MapRenderer.Initialize(GraphicsDevice);
            HUD.Initialize(GraphicsDevice);
            
            // Ideally, the Map should have a reset method
            // TODO: Reset map here

            Map.Cave.RegenerateLayout();
        }

        public override void Update(GameTime GameTime)
        {
            InputHandler.Update(GameTime);
            MapRenderer.Update(GameTime);
            HUD.Update(GameTime);
        }

        public override void Draw(GameTime GameTime)
        {
            MapRenderer.Draw(GameTime);
            HUD.Draw(GameTime);
        }

        public override void UnloadContent()
        {

        }

        public override void Uninitialize()
        {

        }
    }
}
