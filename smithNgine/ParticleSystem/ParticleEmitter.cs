using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Codesmith.SmithNgine.Particles
{
    /// <summary>
    /// Base class for a particle emitter class
    /// </summary>
    public class ParticleEmitter
    {
        protected Random random;
        private List<Texture2D> textures;
        
        public Vector2 Position
        {
            get;
            set;
        }

        public ParticleEmitter(Vector2 position)
        {
            textures = new List<Texture2D>();
            random = new Random();
            Position = position;
        }

        public List<Particle> Generate( int count = 1)
        {
            List<Particle> pl = new List<Particle>();
            for (int i = 0; i < count; i++)
            {
                pl.Add(GenerateParticle());
            }
            return pl;
        }

        public void AddTexture(Texture2D texture)
        {
            textures.Add(texture);
        }

        protected virtual Particle GenerateParticle()
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Particle p = new Particle(texture);

            p.Position = this.Position;
            p.Velocity = new Vector2(
                    1f * (float)(random.NextDouble() * 2 - 1),
                    1f * (float)(random.NextDouble() * 2 - 1));
            p.Rotation = 0;
            p.AngularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            p.Color = new Color(
                    (float)random.NextDouble(),
                    (float)random.NextDouble(),
                    (float)random.NextDouble());
            p.Scale = (float)random.NextDouble();
            p.TimeToLive = TimeSpan.FromSeconds(1.0f);
 
            return p;
        }

    }
}
