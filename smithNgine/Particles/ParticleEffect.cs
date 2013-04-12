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
                int c=0;
                foreach (ParticleEmitter em in emitters)
                {
                    c += em.ParticleCount;
                }
                return c;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ParticleEffect()
        {
            emitters = new List<ParticleEmitter>();
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
                        em.Generate(em.Configuration.Quantity);
                    }
                    else if (timeLeft > TimeSpan.Zero)
                    {
                        em.Generate(em.Configuration.Quantity);
                        timeLeft -= gameTime.ElapsedGameTime;
                    }

                }
                em.Update(gameTime, GravityVector);
            }

            // Update existing particles

        }
        /// <summary>
        /// Draws all the particles in this effect
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        public void Draw(SpriteBatch spriteBatch, bool started = false)
        {
            if (!started)
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            }
            // Draw all existing particles on emitters
            foreach (ParticleEmitter em in emitters)
            {
                em.Draw(spriteBatch);
            }
            if (!started)
            {
                spriteBatch.End();
            }
        }

        public void Generate(TimeSpan duration)
        {
            timeLeft = duration;
        }

        public void Generate(int amount)
        {
            foreach (ParticleEmitter em in emitters)
            {             
                em.Generate(amount);
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
        }
    }
}
