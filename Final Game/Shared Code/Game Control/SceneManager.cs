using EmptyKeys.UserInterface;
using HuntTheWumpus.SharedCode.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HuntTheWumpus.SharedCode.GameControl
{
    /// <summary>
    /// Manages the state of all scenes
    /// </summary>
    public static class SceneManager
    {
        public static Scene MenuScene;
        public static Scene GameScene;
        public static Scene HighScoreScene;

        private static Scene CurrentScene;

        private static ContentManager Content;
        private static GraphicsDevice Graphics;

        /// <summary>
        /// Initializez the scene manager to prepare
        /// all state
        /// </summary>
        /// <param name="Content">The game's ContentManager</param>
        /// <param name="Graphics">The game's GraphicsDevice</param>
        public static void InitializeSceneManager(ContentManager Content, GraphicsDevice Graphics)
        {
            // Initialize each scene
            MenuScene = new MainMenuScene();
            GameScene = new GameScene();
            HighScoreScene = new HighScoreScene();

            // Store the graphics and content for future use
            SceneManager.Content = Content;
            SceneManager.Graphics = Graphics;
        }
        
        /// <summary>
        /// Loads the content of all scenes
        /// </summary>
        public static void LoadAllSceneContent()
        {
            // Load content for each scene
            MenuScene.LoadContent(Content, Graphics);
            GameScene.LoadContent(Content, Graphics);
            HighScoreScene.LoadContent(Content, Graphics);
        }

        /// <summary>
        /// Unloads the current scene and switches
        /// to the specified new one. All Update
        /// and Draw cycles will be fed to this
        /// new scene.
        /// </summary>
        /// <param name="NewScene">The new scene to load</param>
        public static void LoadScene(Scene NewScene)
        {
            // Uninitialize the current scene
            if(CurrentScene != null)
                CurrentScene.Uninitialize();

            // Initialize the new scene and set it as the current scene
            NewScene.Initialize(Graphics);
            CurrentScene = NewScene;

            // Load any EmptyKeys GUI content
            ImageManager.Instance.LoadImages(Content);
            FontManager.Instance.LoadFonts(Content);
        }
        /// <summary>
        /// Passes update call to current scene
        /// </summary>
        /// <param name="GameTime"></param>
        public static void Update(GameTime GameTime)
        {
            CurrentScene.Update(GameTime);
        }
        /// <summary>
        /// Passes draw call to current scene
        /// </summary>
        /// <param name="GameTime"></param>
        public static void Draw(GameTime GameTime)
        {
            CurrentScene.Draw(GameTime);
        }

        /// <summary>
        /// Unloads the content of all scenes
        /// </summary>
        public static void UnloadAllSceneContent()
        {
            CurrentScene = null;
            MenuScene.UnloadContent();
            GameScene.UnloadContent();
            HighScoreScene.UnloadContent();
        }
        /// <summary>
        /// Whether the scene is drawing its own frame or not.
        /// </summary>
        public static bool OverrideFrame
        {
            get
            {
                return CurrentScene.HasFrameContent;
            }
        }
    }
}
