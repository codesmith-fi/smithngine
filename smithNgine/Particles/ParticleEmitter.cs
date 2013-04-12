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
        #endregion

        #region Constructors
        /// <summary>
        /// Intializes new instance to default values
        /// </summary>
        /// <param name="position"></param>
        public ParticleEmitter(Vector2 position)
        {
            Configuration = new ParticleGenerationParams();
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
        public List<Particle> Generate( int count = 1)
        {
            List<Particle> pl = new List<Particle>();
            for (int i = 0; i < count; i++)
            {
                Particle p = new Particle(Configuration);
                GenerateParticle(p);
                pl.Add(p);
            }
            return pl;
        }

        /// <summary>
        /// Generates one new particle, must be implemented by concrete classes
        /// </summary>
        /// <returns>new Particle</returns>
        protected abstract void GenerateParticle(Particle p);
        #endregion
    }
}
