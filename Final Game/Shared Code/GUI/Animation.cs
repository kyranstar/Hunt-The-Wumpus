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
        MoveToNewRoom
    }

    public class SpriteFadeAnimation : SpriteAnimation
    {
        Sprite2D Target;

        readonly float InitialOpacity = 1;
        readonly float FinalOpacity = 0;
        readonly int FadeDuration = 1000;

        float CurrentOpacity = 0;

        public SpriteFadeAnimation(float InitialOpacity, float FinalOpacity, int FadeDuration)
        {
            this.InitialOpacity = InitialOpacity;
            this.FinalOpacity = FinalOpacity;
            this.FadeDuration = FadeDuration;
        }

        public SpriteFadeAnimation()
        {

        }

        public override void Initialize(Sprite2D Target)
        {
            this.Target = Target;
            CurrentOpacity = InitialOpacity;
        }

        public override void Update(GameTime Time)
        {
            double OpacityDelta = (FinalOpacity - InitialOpacity) / FadeDuration * Time.ElapsedGameTime.TotalMilliseconds;
            CurrentOpacity += (float)OpacityDelta;

            float MinBound = Math.Min(InitialOpacity, FinalOpacity);
            float MaxBound = Math.Max(InitialOpacity, FinalOpacity);


            if(CurrentOpacity <= MinBound + Sprite2D.OpacityThreshold)
            {
                CurrentOpacity = MinBound;
                IsFinished = true;
            }

            if (CurrentOpacity >= MaxBound - Sprite2D.OpacityThreshold)
            {
                CurrentOpacity = MaxBound;
                IsFinished = true;
            }


            Target.Opacity = CurrentOpacity;
        }

        new public bool IsFinished
        {
            get;
            protected set;
        }

        public override void Reset()
        {
            CurrentOpacity = InitialOpacity;
            IsFinished = false;
        }

        public override SpriteAnimation Clone()
        {
            return new SpriteFadeAnimation(InitialOpacity, FinalOpacity, FadeDuration);
        }
    }

    public class SpriteMoveAnimation : SpriteAnimation
    {
        Sprite2D Target;

        protected Vector2? InitialPosition;
        readonly int MoveDuration = 1000;

        private Vector2? _TargetPosition;
        public Vector2? TargetPosition
        {
            get
            {
                return _TargetPosition;
            }

            set
            {
                _TargetPosition = value;
                InitialPosition = CurrentPosition.Clone();
            }
        }

        Vector2 CurrentPosition;

        public SpriteMoveAnimation(Vector2? InitialPosition, Vector2? FinalPosition, int MoveDuration)
        {
            this.InitialPosition = InitialPosition;
            this.TargetPosition = FinalPosition;
            this.MoveDuration = MoveDuration;
        }

        public SpriteMoveAnimation(Vector2? FinalPosition, int MoveDuration)
            : this(null, FinalPosition, MoveDuration)
        {
            
        }

        public SpriteMoveAnimation(int MoveDuration)
            : this(null, MoveDuration)
        {

        }

        public override void Initialize(Sprite2D Target)
        {
            this.Target = Target;
            if (!InitialPosition.HasValue)
                InitialPosition = Target.Position;

            if (!TargetPosition.HasValue)
                TargetPosition = InitialPosition.Value.Clone();

            CurrentPosition = InitialPosition.Value.Clone();
        }

        public override void Update(GameTime Time)
        {
            if (!CurrentPosition.EqualsIsh(TargetPosition.Value))
            {
                double xDist = (TargetPosition.Value.X - InitialPosition.Value.X) / MoveDuration * Time.ElapsedGameTime.TotalMilliseconds;
                double yDist = (TargetPosition.Value.Y - InitialPosition.Value.Y) / MoveDuration * Time.ElapsedGameTime.TotalMilliseconds;

                CurrentPosition += new Vector2((float)xDist, (float)yDist);
                Target.Position = CurrentPosition;
            }
            // TODO: Limit position so that it can't go past target

            // IsFinished = CurrentPosition.EqualsIsh(TargetPosition.Value);
        }

        public override void Reset()
        {
            CurrentPosition = InitialPosition.Value;
        }

        public override SpriteAnimation Clone()
        {
            return new SpriteMoveAnimation(InitialPosition, TargetPosition, MoveDuration);
        }
    }
}
