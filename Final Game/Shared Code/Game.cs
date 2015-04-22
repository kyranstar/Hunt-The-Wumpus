using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using HuntTheWumpus.SharedCode.GameControl;
using EmptyKeys.UserInterface;
using EmptyKeys.UserInterface.Media;
using HuntTheWumpus.SharedCode.Helpers;

namespace HuntTheWumpus.SharedCode.GameCore
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameHost : Game
    {
        private GraphicsDeviceManager GraphicsManager;
        private Latch SlowLatch = new Latch();

        public GameHost()
            : base()
        {
            GraphicsManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Hunt the Wumpus";

            GraphicsManager.DeviceCreated += GraphicsManager_DeviceCreated;

#if !WINDOWS_PHONE_APP
            this.IsMouseVisible = true;
#endif
        }

        void GraphicsManager_DeviceCreated(object sender, System.EventArgs e)
        {
            Log.Info("Graphics device created");
            Engine Engine = new MonoGameEngine(GraphicsDevice, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Log.Info("Initializing game...");
            SceneManager.InitializeSceneManager(this.Content, this.GraphicsDevice);
            SceneManager.LoadScene(SceneManager.MenuScene);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Log.Info("Loading game content...");

            SoundManager.Instance.LoadSounds(Content);
            SpriteFont Font = Content.Load<SpriteFont>("Segoe_UI_9_Regular");
            FontManager.DefaultFont = Engine.Instance.Renderer.CreateFont(Font); 


            // TODO: use this.Content to load your game content here
            SceneManager.LoadAllSceneContent();

            FontManager.Instance.LoadFonts(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            Log.Info("Unloading game content...");
            // TODO: Unload any non ContentManager content here
            SceneManager.UnloadAllSceneContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="GameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime GameTime)
        {
#if !NETFX_CORE
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
#endif
            EdgeType SlowLatchEvent = SlowLatch.ProcessValue(GameTime.IsRunningSlowly);
            if (SlowLatchEvent == EdgeType.RisingEdge)
                Log.Warn("Game running slowly! Time since last update: " + GameTime.ElapsedGameTime);
            else if (SlowLatchEvent == EdgeType.FallingEdge)
                Log.Info("Game no longer running slowly.");

            // TODO: Add your update logic here
            SceneManager.Update(GameTime);

            base.Update(GameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Goldenrod);

            SceneManager.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
