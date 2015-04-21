using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HuntTheWumpus.SharedCode.GUI.ParticleSystem
{
    class Particle : Sprite2D
    {
        public Vector2 Velocity;
        public Vector2 Acceleration;

        public float AngularVelocity;

        public int LifetimeMillis;

        public Particle(
            Texture2D Texture,
            Rectangle? Target = null,
            int X = 0, int Y = 0,
            int? Width = null, int? Height = null,
            float Rotation = 0,
            float Scale = 1,
            float Opacity = 1,
            Color? DrawColor = null,
            Dictionary<AnimationType, SpriteAnimation> Animations = null,
            Vector2? Acceleration = null,
            Vector2? Velocity = null,
            float AngularVelocity = 0,
            int LifetimeMillis = 0)
            : base(Texture, Target, X, Y, Width, Height, Rotation, Scale, Opacity, DrawColor, Animations)
        {
            this.Acceleration = Acceleration ?? Vector2.Zero;
            this.Velocity = Velocity ?? Vector2.Zero;
            this.AngularVelocity = AngularVelocity;
            this.LifetimeMillis = LifetimeMillis;
        }

        public override void Initialize()
        {
            this.StartAnimation(AnimationType.FadeIn);
        }

        public bool IsAlive
        {
            get { return LifetimeMillis > 0; }
        }

        new public void Update(GameTime time)
        {
            Velocity += Acceleration * time.ElapsedGameTime.Milliseconds;
            Position += Velocity * time.ElapsedGameTime.Milliseconds;
            Rotation += AngularVelocity * time.ElapsedGameTime.Milliseconds;

            LifetimeMillis -= time.ElapsedGameTime.Milliseconds;

            base.Update(time);
        }

    }
}
