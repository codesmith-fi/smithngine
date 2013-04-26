/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */

namespace Codesmith.SmithNgine.Particles
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// Base class for a particle emitter class, for extension only
    /// </summary>
    public abstract class ParticleEmitter
    {
        #region Fields
        private ParticleEffect hostEffect;
        private ParticleGenerationParams configuration;
        protected Random random;
        protected List<Particle> particles;
        private Vector2 position;
        private float rotation;
        private string name;
        private int budget;
        #endregion

        #region Properties
        public string Name
        {
            get { return name; }
            internal set { name = value; }
        }
      
        /// <summary>
        /// Configuration for particle generation
        /// </summary>
        [Browsable(false)]
        public ParticleGenerationParams Configuration
        {
            get { return configuration; }
            set 
            {
                configuration = value;
                budget = configuration.ParticleBudget;
            }
        }

        /// <summary>
        /// Get the position of this emitter
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return (configuration.Flags.HasFlag(EmitterModes.PositionRelative) && hostEffect != null) ?
                    position + hostEffect.Position : position;
            }
            set
            {
                position = value;
            }
        }

        public float Rotation
        {
            get
            {
                return (Configuration.Flags.HasFlag(EmitterModes.RotationRelative) && hostEffect != null) ? 
                    rotation + hostEffect.Rotation : rotation;
            }
            set
            {
                rotation = value;
            }
        }

        public int ParticleCount
        {
            get { return particles.Count; }
        }

        [Browsable(false)]
        public ParticleEffect Effect
        {
            get { return hostEffect; }
            internal set { hostEffect = value; }
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
            // by default, emitter is relative to the effect position
            Position = position;
            name = "AbstractEmitter";            
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
                if (Configuration.Flags.HasFlag(EmitterModes.UseBudgetOnly))
                {
                    budget--;
                    if (budget < 0) return;
                }
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
            float elapsedMs = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            float elapsedSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            for (int i = 0; i < particles.Count; i++)
            {
                Particle p = particles[i];

                p.TTLPercent += elapsedMs / p.TTL;

                p.Scale = MathHelper.Lerp(p.InitialScale, Configuration.ScaleRange.Y, p.TTLPercent);
                p.Opacity =  MathHelper.Lerp(p.InitialOpacity, Configuration.OpacityRange.Y, p.TTLPercent);
                p.Rotation = MathHelper.Lerp(p.InitialRotation, Configuration.RotationRange.Y, p.TTLPercent);
                p.AngularVelocity = MathHelper.Lerp(p.InitialAngularVelocity, Configuration.AngularVelocityRange.Y, p.TTLPercent);
                p.Position += p.LinearVelocity * elapsedSeconds;
                p.Rotation += p.AngularVelocity * elapsedSeconds;
                p.LinearVelocity *= p.SpeedDamping;

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
