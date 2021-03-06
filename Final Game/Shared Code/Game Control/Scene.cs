﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HuntTheWumpus.SharedCode.Scenes
{
    /// <summary>
    /// Interface for a scene
    /// </summary>
    public abstract class Scene
    {
        /// <summary>
        /// Will be called once per game execution, when the program is started. This is where you load
        /// media assets.
        /// </summary>
        public abstract void LoadContent(ContentManager Content, GraphicsDevice GraphicsDevice);

        /// <summary>
        /// Called every time that the scene is loaded. This will be called before Update() or Draw().
        /// If the scene stores state, this is probably the place to reset it.
        /// </summary>
        public abstract void Initialize(GraphicsDevice GraphicsDevice);

        /// <summary>
        /// Will be called repeatedly as long as this scene is loaded. This is where you should update game state and
        /// handle user input to prepare for rendering.
        /// </summary>
        public abstract void Update(GameTime GameTime);

        /// <summary>
        /// This will be called every time a frame is drawn. You should draw everything here.
        /// </summary>
        public abstract void Draw(GameTime GameTime);

        /// <summary>
        /// Called every time that the scene is unloaded. This will be called after Update() and Draw().
        /// This is where you should deactivate any code that runs independently of your Scene.
        /// </summary>
        public abstract void Uninitialize();

        /// <summary>
        /// Will be called once per game execution, while the program is closing. This is where you should unload assets,
        /// save special content, etc.
        /// </summary>
        public abstract void UnloadContent();

        /// <summary>
        /// Specifies whether the scene wants access to the bottom frame area. If false, the renderer will
        /// automatically fill the hole.
        /// </summary>
        public abstract bool HasFrameContent { get; }
    }
}
