using Microsoft.Xna.Framework;
using System;
using HuntTheWumpus.SharedCode.Helpers;

namespace HuntTheWumpus.SharedCode.GUI
{
    public class SpriteMoveAnimation : SpriteAnimation
    {
        Sprite2D Target;
        
        public Vector2? TargetPosition;
        Vector2? CurrentPosition = null;
        Func<float, float> EasingRateMs;

        public SpriteMoveAnimation(Vector2? FinalPosition, Func<float, float> EasingRateMs)
        {
            this.TargetPosition = FinalPosition;
            this.EasingRateMs = EasingRateMs;
        }

        public SpriteMoveAnimation(Vector2? FinalPosition, float RateMs)
            : this(FinalPosition, dist => RateMs)
        {

        }

        public SpriteMoveAnimation(float RateMs)
            : this(null, RateMs)
        {

        }

        public SpriteMoveAnimation(Func<float, float> EasingRateMs)
            : this(null, EasingRateMs)
        {

        }

        public override void Initialize(Sprite2D Target)
        {
            this.Target = Target;

            CurrentPosition = Target.Position.Clone();
        }

        public override void Update(GameTime Time)
        {
            if (TargetPosition.HasValue && !CurrentPosition.Value.EqualsIsh(TargetPosition.Value))
            {
                Vector2 TargetOffset = new Vector2(
                    TargetPosition.Value.X -  CurrentPosition.Value.X,
                    TargetPosition.Value.Y - CurrentPosition.Value.Y);

                float TargetDistance = (float)TargetOffset.Distance(Vector2.Zero);

                float RateDistance = EasingRateMs(TargetDistance) * (float)Time.ElapsedGameTime.TotalMilliseconds;

                Vector2 DistanceToTravel = new Vector2(
                    TargetOffset.X / TargetDistance * RateDistance,
                    TargetOffset.Y / TargetDistance * RateDistance);

                CurrentPosition += DistanceToTravel;
                Target.Position = CurrentPosition.Value;
            }
            // TODO: Limit position so that it can't go past target (instead of relying on easing)

            // IsFinished = CurrentPosition.EqualsIsh(TargetPosition.Value);
        }

        public override void Reset()
        {

        }

        public override SpriteAnimation Clone()
        {
            return new SpriteMoveAnimation(TargetPosition, EasingRateMs);
        }
    }
}
