using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HuntTheWumpus.SharedCode.GUI
{
    /// <summary>
    /// Describes the action and state of an animation
    /// </summary>
    public abstract class SpriteAnimation
    {
        /// <summary>
        /// Tracking property to help manage the state of the animation
        /// </summary>
        public bool IsStarted { get; set; }

        /// <summary>
        /// Initializes the animation and assigns a target.
        /// </summary>
        /// <param name="Target"></param>
        public abstract void Initialize(Sprite2D Target);

        /// <summary>
        /// Updates the state of the animation.
        /// </summary>
        /// <param name="Time"></param>
        public abstract void Update(GameTime Time);

        /// <summary>
        /// Indicates whether the animation has been completed.
        /// </summary>
        public abstract bool IsFinished { get; }

        /// <summary>
        /// Resets any animation state so that the animation can be re-initialized.
        /// </summary>
        public abstract void Reset();
    }

    /// <summary>
    /// Describes each declared type of animation
    /// </summary>
    public enum AnimationType
    {

    }
}
