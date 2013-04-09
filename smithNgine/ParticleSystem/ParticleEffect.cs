/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Codesmith.SmithNgine.Particles
{
    /// <summary>
    /// ParticleEffect class
    /// Each effect can contain multiple emitters. 
    /// Particles are owned by the Effect
    /// </summary>
    public class ParticleEffect
    {
        const int MaxParticles = 1000;

        private List<ParticleEmitter> emitters;
        private List<Particle> particles;

        public int ParticleCount
        {
            get
            {
                return particles.Count;
            }
        }

        public ParticleEffect()
        {
            emitters = new List<ParticleEmitter>();
            particles = new List<Particle>();
        }

        public void Update(GameTime gameTime)
        {
            // Create new particles with emitters
            foreach (ParticleEmitter em in emitters)
            {
                if (ParticleCount < MaxParticles)
                {
                    this.particles.AddRange( em.Generate(1) );
                }
            }

            // Update existing particles
            for( int i=0; i<particles.Count; i++)
            {
                Particle p = particles[i];
                p.Update(gameTime);
                p.TimeToLive -= gameTime.ElapsedGameTime;
                if (p.TimeToLive <= TimeSpan.Zero)
                {
                    particles.RemoveAt(i);
                    i--;
                }

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw all existing particles
            foreach (Particle particle in particles)
            {
                particle.Draw(spriteBatch);
            }
        }

        public void AddEmitter(ParticleEmitter emitter)
        {
            emitters.Add(emitter);
        }

        public void RemoveEmitter(ParticleEmitter emitter)
        {
            emitters.Remove(emitter);
        }

        public void Clear()
        {
            emitters.Clear();
            particles.Clear();
        }
    }
}
