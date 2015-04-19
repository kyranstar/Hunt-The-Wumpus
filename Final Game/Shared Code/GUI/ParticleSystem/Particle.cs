using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HuntTheWumpus.SharedCode.GUI.ParticleSystem
{
    class Particle : Sprite2D
    {

        public Color Color;

        public Vector2 Velocity;
        public Vector2 Acceleration;

        public float AngularVelocity;

        public float Opacity;

        public float Scale;

        public int LifetimeMillis;

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, float angle, float angularVelocity, Color color, float size, int lifetimeMillis, float opacity)
            : base((int)position.X, (int)position.Y, (int)(texture.Width * size), (int)(texture.Height * size), 0, texture)
        {
            this.Velocity = velocity;
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

            Rotation += AngularVelocity * time.ElapsedGameTime.Milliseconds;

            LifetimeMillis -= time.ElapsedGameTime.Milliseconds;
        }
        public void Draw(SpriteBatch target)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            target.Draw(Texture, new Vector2(RenderX, RenderY), sourceRectangle, Color * Opacity,
                Rotation, origin, Scale, SpriteEffects.None, 0f);
        }

    }
}
