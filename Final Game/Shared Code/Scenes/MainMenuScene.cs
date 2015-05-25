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
        SpriteBatch ChaseTarget;
        WumpusChaseAnimation ChaseAnimation;

        public override void LoadContent(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            ChaseAnimation = new WumpusChaseAnimation(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 60, 10, 0.15);
            ChaseAnimation.LoadContent(Content);
        }

        public override void Initialize(GraphicsDevice GraphicsDevice)
        {
            MenuGUI = new MainMenuView(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            MenuGUI.DataContext = new MainMenuContext();

            ChaseTarget = new SpriteBatch(GraphicsDevice);
            ChaseAnimation.Initialize();
        }

        public override void Update(GameTime GameTime)
        {
            MenuGUI.UpdateInput(GameTime.ElapsedGameTime.TotalMilliseconds);
            MenuGUI.UpdateLayout(GameTime.ElapsedGameTime.TotalMilliseconds);
            ChaseAnimation.Update(GameTime);
        }

        public override void Draw(GameTime GameTime)
        {
            MenuGUI.Draw(GameTime.ElapsedGameTime.TotalSeconds);

            ChaseTarget.Begin();
            ChaseAnimation.Draw(ChaseTarget);
            ChaseTarget.End();
        }

        public override void UnloadContent()
        {

        }

        public override void Uninitialize()
        {

        }
    }
}
