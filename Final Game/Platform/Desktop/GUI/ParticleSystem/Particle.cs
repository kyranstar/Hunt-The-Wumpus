using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.SharedCode.GUI.ParticleSystem
{
    class Particle
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Acceleration;


        public float RotationSpeed;
        public float Rotation;
        public float Scale;
        public int LifetimeMillis;

        public bool IsAlive
        {
            get { return LifetimeMillis > 0; }
        }
        public void Update(GameTime time)
        {
            Velocity += Acceleration * time.ElapsedGameTime.Milliseconds;
            Position += Velocity * time.ElapsedGameTime.Milliseconds;

            Rotation += RotationSpeed * time.ElapsedGameTime.Milliseconds;

            LifetimeMillis -= time.ElapsedGameTime.Milliseconds;
        }
    }
}
