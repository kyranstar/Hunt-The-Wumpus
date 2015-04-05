using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HuntTheWumpus.SharedCode.Scenes;

namespace HuntTheWumpus.SharedCode.GameControl
{
    /// <summary>
    /// Manages the state of all scenes
    /// </summary>
    static class SceneManager
    {
        public static Scene MenuScene;
        public static Scene GameScene;
        public static Scene HighScoreScene;

        private static Scene CurrentScene;

        private static ContentManager Content;
        private static GraphicsDevice Graphics;

        public static void InitializeSceneManager(ContentManager Content, GraphicsDevice Graphics)
        {
            SceneManager.MenuScene = new MainMenuScene();
            SceneManager.GameScene = new GameScene();

            SceneManager.Content = Content;
            SceneManager.Graphics = Graphics;
        }

        public static void LoadAllSceneContent()
        {
            MenuScene.LoadContent(Content);
            GameScene.LoadContent(Content);
            //HighScoreScene.LoadContent(Content);
        }

        public static void LoadScene(Scene NewScene)
        {
            if(CurrentScene != null)
                CurrentScene.Uninitialize();

            NewScene.Initialize(Graphics);
            CurrentScene = NewScene;
        }

        public static void Update(GameTime GameTime)
        {
            CurrentScene.Update(GameTime);
        }

        public static void Draw(GameTime GameTime)
        {
            CurrentScene.Draw(GameTime);
        }

        public static void UnloadAllSceneContent()
        {
            CurrentScene = null;
            MenuScene.UnloadContent();
            GameScene.UnloadContent();
            //HighScoreScene.UnloadContent();
        }
    }
}
