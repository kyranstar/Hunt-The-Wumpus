using EmptyKeys.UserInterface.Generated;
using HuntTheWumpus.SharedCode.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HuntTheWumpus.SharedCode.Scenes
{
    class GameOverScene : Scene
    {
        GameOverView GameOverGUI;

        public override void LoadContent(ContentManager Content)
        {

        }

        public override void Initialize(GraphicsDevice GraphicsDevice)
        {
            GameOverGUI = new GameOverView(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            GameOverGUI.DataContext = new GameOverContext();
        }

        public override void Update(GameTime GameTime)
        {
            GameOverGUI.UpdateInput(GameTime.ElapsedGameTime.TotalMilliseconds);
            GameOverGUI.UpdateLayout(GameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public override void Draw(GameTime GameTime)
        {
            GameOverGUI.Draw(GameTime.ElapsedGameTime.TotalSeconds);
        }

        public override void UnloadContent()
        {

        }

        public override void Uninitialize()
        {

        }
    }
}
