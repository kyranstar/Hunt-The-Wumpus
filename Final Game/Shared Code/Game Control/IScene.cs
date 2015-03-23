using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpus.SharedCode.GameControl
{
    /// <summary>
    /// Interface for a scene
    /// </summary>
    interface IScene
    {
        /// <summary>
        /// Called every time that the scene is loaded. This will be called before Update() or Draw.
        /// If the scene stores state, this is probably the place to reset it.
        /// </summary>
        public void Initialize();

        /// <summary>
        /// Will be called once per game execution, when the program is started. This is where you load
        /// media assets.
        /// </summary>
        public void LoadContent();

        /// <summary>
        /// Will be called once per game execution, while the program is closing. This is where you should unload assets,
        /// save special content, etc.
        /// </summary>
        public void UnloadContent();

        /// <summary>
        /// Will be called repeatedly as long as this scene is loaded. This is where you should update game state and
        /// handle user input to prepare for rendering.
        /// </summary>
        public void Update(GameTime GameTime);

        /// <summary>
        /// This will be called every time a frame is drawn. You should draw everything here using the given
        /// <code>SpriteBatch</code>.
        /// </summary>
        public void Draw(GameTime GameTime, SpriteBatch TargetBatch);
    }
}
