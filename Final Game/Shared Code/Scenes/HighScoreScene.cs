using EmptyKeys.UserInterface.Generated;
using HuntTheWumpus.SharedCode.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HuntTheWumpus.SharedCode.Scenes
{
    class HighScoreScene : Scene
    {
        HighScoreView ScoreGUI;

        public override void LoadContent(ContentManager Content, GraphicsDevice GraphicsDevice)
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