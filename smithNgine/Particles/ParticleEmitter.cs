/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Codesmith.SmithNgine.Particles
{
    /// <summary>
    /// Base class for a particle emitter class, for extension only
    /// </summary>
    public abstract class ParticleEmitter
    {
        #region Fields
        protected Random random;
        protected ParticleGenerationParams generationParams;
        protected List<Particle> particles;
        #endregion

        #region Properties
        public ParticleGenerationParams Configuration
        {
            get;
            set;
        }

        /// <summary>
        /// Get the position of this emitter
        /// </summary>
        public Vector2 Position
        {
            get;
            set;
        }

        public float Rotation
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AutoGenerate
        {
            get;
            set;
        }
        public int ParticleCount
        {
            get { return particles.Count; }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Intializes new instance to default values
        /// </summary>
        /// <param name="position"></param>
        public ParticleEmitter(Vector2 position)
        {
            Configuration = new ParticleGenerationParams();
            particles = new List<Particle>();
            random = new Random();
            Position = position;
            AutoGenerate = true;
            
        }
        #endregion

        #region New methods
        /// <summary>
        /// Generates one or more particles, particle is created in the 
        /// concrete emitter class by the method GenerateParticle()
        /// </summary>
        /// <param name="count">How many particles to generate at once</param>
        /// <returns>List of particles generated, these will be added to the ParticleSys</returns>
        public void Generate( int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                Particle p = new Particle(Configuration);
                GenerateParticle(p);
                particles.Add(p);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle p in particles)
            {
                p.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime, Vector2 globalGravity)
        {
            double elapsedMs = gameTime.ElapsedGameTime.TotalMilliseconds;
            for (int i = 0; i < particles.Count; i++)
            {
                Particle p = particles[i];

                p.TTLPercent += (float)elapsedMs / p.TTL;

                p.LinearVelocity *= p.VelocityDamping;
                p.Scale = MathHelper.Lerp(Configuration.ScaleRange.X, Configuration.ScaleRange.Y, p.TTLPercent);
                p.Opacity =  MathHelper.Lerp(Configuration.OpacityRange.X, Configuration.OpacityRange.Y, p.TTLPercent);
                p.Rotation = MathHelper.Lerp(Configuration.RotationRange.X, Configuration.RotationRange.Y, p.TTLPercent);
                p.Position += p.LinearVelocity;
//                p.Rotation += p.AngularVelocity;

                p.LinearVelocity += globalGravity;
                if (p.TTLPercent >= 1.0f)
                {
                    particles.RemoveAt(i);
                    i--;
                }
            }


        }


        /// <summary>
        /// Generates one new particle, must be implemented by concrete classes
        /// </summary>
        /// <returns>new Particle</returns>
        protected abstract void GenerateParticle(Particle p);
        #endregion
    }
}
