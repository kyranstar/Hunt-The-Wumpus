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
    class HighScoreScene : Scene
    {
        HighScoreView ScoreGUI;

        public override void LoadContent(ContentManager Content)
        {

        }

        public override void Initialize(GraphicsDevice GraphicsDevice)
        {
            ScoreGUI = new HighScoreView(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            ScoreGUI.DataContext = new HighScoreContext();
        }

        public override void Update(GameTime GameTime)
        {
            ScoreGUI.UpdateInput(GameTime.ElapsedGameTime.TotalMilliseconds);
            ScoreGUI.UpdateLayout(GameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public override void Draw(GameTime GameTime)
        {
            ScoreGUI.Draw(GameTime.ElapsedGameTime.TotalSeconds);
        }

        public override void UnloadContent()
        {

        }

        public override void Uninitialize()
        {
            
        }
    }
}