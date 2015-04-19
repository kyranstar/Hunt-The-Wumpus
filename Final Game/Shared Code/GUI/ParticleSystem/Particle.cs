using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.SharedCode.GUI.ParticleSystem
{
    class Particle
    {
        public Texture2D Texture;
        public Color Color;

        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Acceleration;

        public float AngularVelocity;
        public float Angle;

        public float Opacity;

        public float Scale;

        public int LifetimeMillis;

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, float angle, float angularVelocity, Color color, float size, int lifetimeMillis, float opacity)
        {
            this.Texture = texture;
            this.Position = position;
            this.Velocity = velocity;
            this.Angle = angle;
            this.AngularVelocity = angularVelocity;
            this.Color = color;
            this.Scale = size;
            this.LifetimeMillis = lifetimeMillis;
            this.Opacity = opacity;
        }

        public bool IsAlive
        {
            get { return LifetimeMillis > 0; }
        }
        public void Update(GameTime time)
        {
            Velocity += Acceleration * time.ElapsedGameTime.Milliseconds;
            Position += Velocity * time.ElapsedGameTime.Milliseconds;

            Angle += AngularVelocity * time.ElapsedGameTime.Milliseconds;

            LifetimeMillis -= time.ElapsedGameTime.Milliseconds;
        }
        public void Draw(SpriteBatch target)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            target.Draw(Texture, Position, sourceRectangle, Color * Opacity,
                Angle, origin, Scale, SpriteEffects.None, 0f);
        }
    }
}
