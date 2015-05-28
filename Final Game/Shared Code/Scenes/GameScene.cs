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
        GameController GameController;
        MapRenderer MapRenderer;
        GraphicsDevice Graphics;
        HudRenderer HUD;

#if DESKTOP
        GodManager God;
#endif

        public override void LoadContent(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            GameController = new GameController();


            MapRenderer = new MapRenderer(GameController);
            MapRenderer.LoadContent(Content);

            HUD = new HudRenderer(MapRenderer, GameController);
            HUD.LoadContent(Content);
        }

        public override void Initialize(GraphicsDevice GraphicsDevice)
        {
            this.Graphics = GraphicsDevice;
            GameController.Reset();
#if DESKTOP
            God = new GodManager(GameController, MapRenderer);
            // Run god console on separate thread
            new Task(God.Initialize).Start();
#endif
            MapRenderer.Initialize(GraphicsDevice);
            HUD.Initialize(GraphicsDevice);

            GameController.Initialize();
        }

        public override void Update(GameTime GameTime)
        {
            GameController.Update(GameTime);
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

        public override bool HasFrameContent
        {
            get { return true; }
        }
    }
}
