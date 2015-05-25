using Microsoft.Xna.Framework;
using System;
using HuntTheWumpus.SharedCode.Helpers;

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
        /// This should always return false before the animation has started and after Reset() is called.
        /// THIS SHOULD BE OVERRIDDEN IN MOST CASES
        /// </summary>
        public bool IsFinished
        {
            get { return false; }
        }

        /// <summary>
        /// Resets any animation state so that the animation can be re-initialized.
        /// </summary>
        public abstract void Reset();

        /// <summary>
        /// Clones the animation's settings, but not any of its state.
        /// </summary>
        public abstract SpriteAnimation Clone();
    }

    /// <summary>
    /// Describes each intended type of animation.
    /// These should be defined on a purpose-by-purpose
    /// basis.
    /// This is a substitute for using string IDs;
    /// if it gets to unwieldy, we can replace it.
    /// </summary>
    public enum AnimationType
    {
        FadeIn,
        FadeOut,
        MoveToNewRoom,
        MoveToNewMenuTile
    }
}
