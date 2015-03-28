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

        public static void LoadAllSceneContent(ContentManager Content)
        {
            //MenuScene.LoadContent(Content);
            GameScene.LoadContent(Content);
            //HighScoreScene.LoadContent(Content);
        }

        public static void LoadScene(Scene NewScene)
        {
            NewScene.Initialize();
            CurrentScene = NewScene;
        }

        public static void Update(GameTime GameTime)
        {
            CurrentScene.Update(GameTime);
        }

        public static void Draw(GameTime GameTime, SpriteBatch TargetBatch)
        {
            CurrentScene.Draw(GameTime, TargetBatch);
        }

        public static void UnloadAllSceneContent()
        {
            CurrentScene = null;
            //MenuScene.UnloadContent();
            GameScene.UnloadContent();
            //HighScoreScene.UnloadContent();
        }


        public static void Initialize()
        {
            GameScene = new GameScene();
        }
    }
}
