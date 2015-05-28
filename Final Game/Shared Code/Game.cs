using EmptyKeys.UserInterface;
using EmptyKeys.UserInterface.Media;
using HuntTheWumpus.SharedCode.GameControl;
using HuntTheWumpus.SharedCode.Helpers;
using HuntTheWumpus.SharedCode.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HuntTheWumpus.SharedCode.GameCore
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameHost : Game
    {
        private GraphicsDeviceManager GraphicsManager;
        private Latch SlowLatch = new Latch();

        private ArcadeFrame ArcadeFrame;

        public GameHost()
            : base()
        {
            GraphicsManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "Hunt the PacMan";
            GraphicsManager.DeviceCreated += GraphicsManager_DeviceCreated;
            Mouse.WindowHandle = Window.Handle;

#if !WINDOWS_PHONE_APP
            this.IsMouseVisible = true;
#endif

            ArcadeFrame = new ArcadeFrame();
        }

        void GraphicsManager_DeviceCreated(object sender, System.EventArgs e)
        {
            Log.Info("Graphics device created");
            GraphicsManager.PreferredBackBufferWidth = 1366;
            GraphicsManager.PreferredBackBufferHeight = 768;
            Window.AllowUserResizing = true;
            //GraphicsManager.PreferredBackBufferWidth = (int)(GraphicsManager.GraphicsDevice.Adapter.CurrentDisplayMode.Width * 0.6);
            //GraphicsManager.PreferredBackBufferHeight = (int)(GraphicsManager.GraphicsDevice.Adapter.CurrentDisplayMode.Height * 0.6);
            GraphicsManager.ApplyChanges();

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
            Log.Info("Initializing game...");
            SceneManager.InitializeSceneManager(this.Content, this.GraphicsDevice);
            ArcadeFrame.Initialize(this.GraphicsDevice);
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
            
            SceneManager.LoadAllSceneContent();
            SceneManager.LoadScene(SceneManager.MenuScene);

            FontManager.Instance.LoadFonts(Content);

            ArcadeFrame.LoadContent(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            Log.Info("Unloading game content...");
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
            
            SceneManager.Update(GameTime);
            ArcadeFrame.Update(GameTime);
            base.Update(GameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DimGray);
            SceneManager.Draw(gameTime);
            ArcadeFrame.Draw(SceneManager.OverrideFrame);
            base.Draw(gameTime);
        }
    }
}
