using EmptyKeys.UserInterface.Generated;
using HuntTheWumpus.SharedCode.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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

        public override void Draw(GameTime GameTime)
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
