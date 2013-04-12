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
        const int MaxParticles = 10000;

        private List<ParticleEmitter> emitters;
        private List<Particle> particles;
        private TimeSpan timeLeft = TimeSpan.Zero;

        /// <summary>
        /// Gravity vector which gives a velocity modifier for every particle
        /// maintained by this ParticleEffect
        /// </summary>
        public Vector2 GravityVector
        {
            get;
            set;
        }

        /// <summary>
        /// Return the count of emitters in this effect
        /// </summary>
        public int EmitterCount
        {
            get
            {
                return emitters.Count;
            }
        }

        /// <summary>
        /// Return the count of particles in this effect
        /// </summary>
        public int ParticleCount
        {
            get
            {
                return particles.Count;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ParticleEffect()
        {
            emitters = new List<ParticleEmitter>();
            particles = new List<Particle>();
            GravityVector = new Vector2(0.0f, 0.0f);
        }

        /// <summary>
        /// Update method, should be called periodically for active effects
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            // Create new particles with emitters
            foreach (ParticleEmitter em in emitters)
            {
                if (ParticleCount < MaxParticles)
                {
                    if (em.AutoGenerate)
                    {
                        particles.AddRange(em.Generate(em.Configuration.Quantity));
                    }
                    else if (timeLeft > TimeSpan.Zero)
                    {
                        particles.AddRange(em.Generate(em.Configuration.Quantity));
                        timeLeft -= gameTime.ElapsedGameTime;
                    }

                }
            }

            // Update existing particles
            for( int i=0; i<particles.Count; i++)
            {
                Particle p = particles[i];
                p.Update(gameTime);
                p.LinearVelocity += GravityVector;
                p.TTL -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (p.TTL <= 0.0f)
                {
                    particles.RemoveAt(i);
                    i--;
                }

            }
        }

        /// <summary>
        /// Draws all the particles in this effect
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            // Draw all existing particles
            foreach (Particle particle in particles)
            {
                particle.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public void Generate(TimeSpan duration)
        {
            timeLeft = duration;
        }

        /// <summary>
        /// Add particles to this effect from outside.
        /// </summary>
        /// <param name="particles"></param>
        public void AddParticles(List<Particle> particles)
        {
            if (ParticleCount < MaxParticles )
            {
                this.particles.AddRange(particles);
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
