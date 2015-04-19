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
        const float CLOUD_SPEED = .5f;
        const int CLOUD_LIGHTNESS_MIN = 50;
        const int CLOUD_LIGHTNESS_MAX = 150;
        const float SPIN_SPEED = 0.01f;
        const int MIN_LIFE = 100;
        const int MAX_LIFE = 1000;

        const float MIN_SIZE = 0.5f;
        const float MAX_SIZE = 1.5f;

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
                                    CLOUD_SPEED * (float)(random.NextDouble() * 2 - 1),
                                    CLOUD_SPEED * (float)(random.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVelocity = SPIN_SPEED * (float)(random.NextDouble() * 2 - 1);

            int colorVal = (int)(random.NextDouble() * (CLOUD_LIGHTNESS_MAX - CLOUD_LIGHTNESS_MIN) + CLOUD_LIGHTNESS_MIN);

            Color color = new Color(colorVal, colorVal, colorVal); 

            float size = (float)(random.NextDouble() * (MAX_SIZE - MIN_SIZE) + MIN_SIZE);
            int livingTime = MIN_LIFE + (int)(random.NextDouble() * (MAX_LIFE - MIN_LIFE));

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, livingTime);
        }
    }
}
