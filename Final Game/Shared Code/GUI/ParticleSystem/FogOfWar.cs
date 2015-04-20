using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace HuntTheWumpus.SharedCode.GUI.ParticleSystem
{
    class FogOfWar : ParticleSystem
    {
        const int PARTICLE_CAP = 200;

        const int RATE = 10;
        const float CLOUD_SPEED = .05f;
        const int CLOUD_LIGHTNESS_MIN = 50;
        const int CLOUD_LIGHTNESS_MAX = 150;
        const float SPIN_SPEED = 0.001f;
        const int MIN_LIFE = 5000;
        const int MAX_LIFE = 20000;

        const float OPACITY = 0.5f;

        const float MIN_SIZE = 1.5f;
        const float MAX_SIZE = 3.5f;

        private Func<Rectangle, bool> IsInsideCloud;
        private Camera2D Camera;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cloudTextures">A list of textures to use for the cloud</param>
        /// <param name="IsInsideCloud">A function that takes a vector and determines whether the point is inside the cloud</param>
        /// <param name="bounds">The bounds to try to generate particles within</param>
        public FogOfWar(List<Texture2D> cloudTextures, Camera2D cam, Func<Rectangle, bool> IsInsideCloud)
            : base(cloudTextures, new Vector2(0, 0), RATE, PARTICLE_CAP)
        {
            this.IsInsideCloud = IsInsideCloud;
            this.Camera = cam;
        }

        protected override Particle GenerateNewParticle()
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position;
            // TODO: figure out better way to calculate positions
            int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

            do
            {
                position = new Vector2(
                    -Camera.VirtualVisibleViewport.X + (float)(random.NextDouble() * Camera.VirtualVisibleViewport.Width),
                    -Camera.VirtualVisibleViewport.Y + (float)(random.NextDouble() * Camera.VirtualVisibleViewport.Height));
            } while (!IsInsideCloud(new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height)));

            Vector2 velocity = new Vector2(
                                    CLOUD_SPEED * (float)(random.NextDouble() * 2 - 1),
                                    CLOUD_SPEED * (float)(random.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVelocity = SPIN_SPEED * (float)(random.NextDouble() * 2 - 1);

            int colorVal = (int)(random.NextDouble() * (CLOUD_LIGHTNESS_MAX - CLOUD_LIGHTNESS_MIN) + CLOUD_LIGHTNESS_MIN);

            Color color = new Color(colorVal, colorVal, colorVal);

            float size = (float)(random.NextDouble() * (MAX_SIZE - MIN_SIZE) + MIN_SIZE);
            int livingTime = MIN_LIFE + (int)(random.NextDouble() * (MAX_LIFE - MIN_LIFE));

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, livingTime, OPACITY);
        }
    }
}
