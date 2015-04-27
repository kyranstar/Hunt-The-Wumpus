﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace HuntTheWumpus.SharedCode.GUI.ParticleSystem
{
    class FogOfWar : ParticleSystem
    {
        readonly SpriteFadeAnimation SpriteFadeInAnimation = new SpriteFadeAnimation(0, OPACITY, 500),
            SpriteFadeOutAnimation = new SpriteFadeAnimation(OPACITY, 0, 500);

        const int PARTICLE_CAP = 600;

        const float CLOUD_SPEED = .001f;
        const int CLOUD_LIGHTNESS_MIN = 50;
        const int CLOUD_LIGHTNESS_MAX = 150;
        const float SPIN_SPEED = 0.0005f;
        const int MIN_LIFE = 5000;
        const int MAX_LIFE = 20000;

        const float OPACITY = 0.8f;

        const float MIN_SIZE = 1.5f;
        const float MAX_SIZE = 5.5f;

        private Camera2D Camera;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cloudTextures">A list of textures to use for the cloud</param>
        /// <param name="IsInsideCloud">A function that takes a vector and determines whether the point is inside the cloud</param>
        /// <param name="bounds">The bounds to try to generate particles within</param>
        public FogOfWar(Texture2D[] cloudTextures, Camera2D cam)
            : base(cloudTextures, new Vector2(0, 0), PARTICLE_CAP)
        {
            this.Camera = cam;
        }

        protected override Particle GenerateNewParticle()
        {
            Texture2D texture = textures[random.Next(textures.Length)];

            Vector2 position = new Vector2(
                -Camera.VirtualVisibleViewport.X + (float)(random.Next(Camera.VirtualVisibleViewport.Width)),
                -Camera.VirtualVisibleViewport.Y + (float)(random.Next(Camera.VirtualVisibleViewport.Height)));

            Vector2 velocity = new Vector2(
                                    CLOUD_SPEED * (float)(random.NextDouble() * 2 - 1),
                                    CLOUD_SPEED * (float)(random.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVelocity = SPIN_SPEED * (float)(random.NextDouble() * 2 - 1);

            int colorVal = (int)(random.NextDouble() * (CLOUD_LIGHTNESS_MAX - CLOUD_LIGHTNESS_MIN) + CLOUD_LIGHTNESS_MIN);

            Color color = new Color(colorVal, colorVal, colorVal);

            float size = (float)(random.NextDouble() * (MAX_SIZE - MIN_SIZE) + MIN_SIZE);
            int livingTime = MIN_LIFE + (int)(random.NextDouble() * (MAX_LIFE - MIN_LIFE));

            Particle NewParticle = new Particle(
                Texture: texture,
                X: (int)Math.Round(position.X),
                Y: (int)Math.Round(position.Y),
                Rotation: angle,
                AngularVelocity: angularVelocity,
                DrawColor: color,
                Scale: size,
                LifetimeMillis: livingTime,
                Opacity: OPACITY);

            NewParticle.AddAnimation(AnimationType.FadeIn, SpriteFadeInAnimation.Clone());
            NewParticle.AddAnimation(AnimationType.FadeOut, SpriteFadeOutAnimation.Clone());

            return NewParticle;
        }
    }
}
