using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using EmptyKeys.UserInterface.Mvvm;
using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Generated;

using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.GUI;
using HuntTheWumpus.SharedCode;

namespace HuntTheWumpus.SharedCode.Scenes
{
    class MainMenuScene : Scene
    {
        MainMenuView MenuGUI;

        public override void LoadContent(ContentManager Content)
        {

        }

        public override void Initialize(GraphicsDevice GraphicsDevice)
        {
            MenuGUI = new MainMenuView(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            MenuGUI.DataContext = new MainMenuContext();
        }

        public override void Update(GameTime GameTime)
        {
            MenuGUI.UpdateInput(GameTime.ElapsedGameTime.TotalMilliseconds);
            MenuGUI.UpdateLayout(GameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public override void Draw(GameTime GameTime, SpriteBatch TargetBatch)
        {
            MenuGUI.Draw(GameTime.ElapsedGameTime.TotalSeconds);
        }

        public override void UnloadContent()
        {

        }

        public override void Uninitialize()
        {
            
        }
    }
}
