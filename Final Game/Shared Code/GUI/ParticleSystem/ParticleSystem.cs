using HuntTheWumpus.SharedCode.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HuntTheWumpus.SharedCode.GUI.ParticleSystem
{
    public class ParticleSystem
    {
        protected static Random random = new Random();

        public Vector2 EmitterLocation { get; set; }
        protected List<Particle> particles;
        protected Texture2D[] textures;
        private int particleCap;

        public int NumberParticles
        {
            get { return this.particles.Count; }
        }

        public ParticleSystem(Texture2D[] textures, Vector2 location, int particleCap)
        {
            EmitterLocation = location;
            this.textures = textures;
            this.particleCap = particleCap;

            this.particles = new List<Particle>();
        }

        public void Initialize()
        {
            this.particles.Clear();
        }

        public virtual void Update(GameTime time)
        {
            while (NumberParticles < particleCap)
            {
                Particle NewParticle = GenerateNewParticle();
                NewParticle.Initialize();
                particles.Add(NewParticle);
            }

            particles = particles.Where(p =>
            {
                p.Update(time);
                bool? FadeOutState = p.GetAnimationState(AnimationType.FadeOut);

                if (!p.IsAlive && !p.IsTransparent && FadeOutState.HasValue)
                {
                    if (FadeOutState == false)
                        p.StartAnimation(AnimationType.FadeOut);

                    return true;
                }
                return p.IsAlive;
            }).ToList<Particle>();
        }

        protected virtual Particle GenerateNewParticle()
        {
            Texture2D texture = textures[random.Next(textures.Length)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
                                    1f * (float)(random.NextDouble() * 2 - 1),
                                    1f * (float)(random.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            Color color = new Color(
                        (float)random.NextDouble(),
                        (float)random.NextDouble(),
                        (float)random.NextDouble());
            float size = (float)random.NextDouble();
            int ttl = 100 + random.Next(1000);

            return new Particle(
                texture,
                X: EmitterLocation.X.ToInt(),
                Y: EmitterLocation.Y.ToInt(),
                Velocity: velocity,
                Rotation: angle,
                AngularVelocity: angularVelocity,
                DrawColor: color,
                Scale: size,
                LifetimeMillis: ttl);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle p in particles)
                p.Draw(spriteBatch);
        }
    }
}
