using Microsoft.Xna.Framework;
using System;
using HuntTheWumpus.SharedCode.Helpers;

namespace HuntTheWumpus.SharedCode.GUI
{
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
}
