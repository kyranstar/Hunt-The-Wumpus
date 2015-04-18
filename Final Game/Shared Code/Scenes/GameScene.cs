using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using EmptyKeys.UserInterface.Mvvm;
using EmptyKeys.UserInterface.Input;

using HuntTheWumpus.SharedCode;
using HuntTheWumpus.SharedCode.Helpers;
using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.GUI;
using HuntTheWumpus.SharedCode.GameMap;
using System.Threading.Tasks;

namespace HuntTheWumpus.SharedCode.Scenes
{
    class GameScene : Scene
    {
        Map Map;
        MapRenderer MapRenderer;
        MapInputHandler InputHandler;
        GraphicsDevice Graphics;

#if DESKTOP
        GodManager God;
#endif

        public override void LoadContent(ContentManager Content)
        {
            Map = new Map();
            InputHandler = new MapInputHandler(Map);
            MapRenderer = new MapRenderer(Map);
            MapRenderer.LoadContent(Content);
        }

        public override void Initialize(GraphicsDevice GraphicsDevice)
        {
            this.Graphics = GraphicsDevice;

#if DESKTOP
            God = new GodManager(Map, MapRenderer);

            new Task(God.Initialize).Start();
#endif

            MapRenderer.Initialize(GraphicsDevice);
            
            // Ideally, the Map should have a reset method
            // TODO: Reset map here

            MapRenderer.RegenerateLayout();
        }

        public override void Update(GameTime GameTime)
        {
            InputHandler.Update(GameTime);
            MapRenderer.Update(GameTime);
        }

        public override void Draw(GameTime GameTime)
        {
            MapRenderer.Draw(GameTime);
        }

        public override void UnloadContent()
        {

        }

        public override void Uninitialize()
        {

        }
    }
}
