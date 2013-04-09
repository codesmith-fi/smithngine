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
        protected List<Texture2D> textures;
        #endregion

        #region Properties
        /// <summary>
        /// Get the position of this emitter
        /// </summary>
        public Vector2 Position
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
            textures = new List<Texture2D>();
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
                pl.Add(GenerateParticle());
            }
            return pl;
        }

        /// <summary>
        /// Add a new texture to the emitter. These can be used by the actual emitter
        /// </summary>
        /// <param name="texture">Texture to be added</param>
        public void AddTexture(Texture2D texture)
        {
            textures.Add(texture);
        }

        /// <summary>
        /// Generates one new particle, must be implemented by concrete classes
        /// </summary>
        /// <returns>new Particle</returns>
        protected abstract Particle GenerateParticle();
        #endregion
    }
}
