﻿/**
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

        /// <summary>
        /// Gravity vector which gives a velocity modifier for every particle
        /// maintained by this ParticleEffect
        /// </summary>
        public Vector2 GravityVector
        {
            get;
            set;
        }

        public int EmitterCount
        {
            get
            {
                return emitters.Count;
            }
        }

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
            GravityVector = new Vector2(0.0f, 0.0f);
        }

        public void Update(GameTime gameTime)
        {
            // Create new particles with emitters
            foreach (ParticleEmitter em in emitters)
            {
                if (ParticleCount < MaxParticles && em.AutoGenerate)
                {
                    this.particles.AddRange( em.Generate(10) );
                }
            }

            // Update existing particles
            for( int i=0; i<particles.Count; i++)
            {
                Particle p = particles[i];
                p.Update(gameTime);
                p.Velocity += GravityVector;
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
