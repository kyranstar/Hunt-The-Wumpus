using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.SharedCode.GUI.ParticleSystem
{
    class FogOfWar : ParticleSystem
    {
        const int RATE = 30;
        private Func<Vector2, bool> IsInsideCloud;
        private Vector2 viewSize;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cloudTextures">A list of textures to use for the cloud</param>
        /// <param name="IsInsideCloud">A function that takes a vector and determines whether the point is inside the cloud</param>
        /// <param name="viewSize"></param>
        public FogOfWar(List<Texture2D> cloudTextures, Vector2 viewSize, Func<Vector2, bool> IsInsideCloud) : base(cloudTextures, new Vector2(0,0), RATE)
        {
            this.IsInsideCloud = IsInsideCloud;
            this.viewSize = viewSize;
        }

        protected override Particle GenerateNewParticle()
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position;
            // TODO: figure out better way to calculate positions
            do
            {
                position = new Vector2((float)(random.NextDouble() * viewSize.X * 2 - viewSize.X), (float)(random.NextDouble() * viewSize.Y * 2 - viewSize.Y));
            } while (!IsInsideCloud(position));
            Vector2 velocity = new Vector2(
                                    .5f * (float)(random.NextDouble() * 2 - 1),
                                    .5f * (float)(random.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVelocity = 0.01f * (float)(random.NextDouble() * 2 - 1);
            Color color = new Color(100,100,100);
            float size = (float)random.NextDouble() * 2f;
            int ttl = 100 + random.Next(1000);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }
    }
}
