/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 * 
 * For licensing terms, see License.txt which reflects to the current license
 * of this framework.
 */
namespace Codesmith.SmithNgine.Particles
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Codesmith.SmithNgine.Gfx;
    using Codesmith.SmithNgine.General;
    using Codesmith.SmithNgine.MathUtil;

    /// <summary>
    /// Base class for a particle emitter class, for extension only
    /// 
    /// Emitter manages particles during their lifetime. 
    /// AngularVelocity, Opacity and Scale are interpolated during lifetime.
    /// LinearVelocity/speed of the particle is affected by the damping
    /// Rotation and Position is modified by angular velocity and linear velocity
    /// </summary>
    public abstract class ParticleEmitter : DrawableGameObject, IRotatableObject
    {
        #region Fields
        // ParticleEffect which owns this emitter
        private ParticleEffect hostEffect;
        // Configuration for particle generation
        private ParticleGenerationParams configuration;
        // Random generator
        protected PseudoRandom random;
        // Managed particles
        protected List<Particle> particles;
        // Current rotation of the emitter
        private float rotation;
        // Name of the emitter
        private string name;
        // How many particles this emitter can still generate
        private int budget;
        #endregion

        #region Events
        /// <summary>
        /// Event which is triggered when Emitter rotation changes
        /// </summary>
        public event EventHandler<RotationEventArgs> RotationChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the name of this ParticleEmitter
        /// </summary>
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
        public override Vector2 Position
        {
            get
            {
                return (configuration.Flags.HasFlag(EmitterModes.PositionRelative) && hostEffect != null) ?
                    base.Position + hostEffect.Position : base.Position;
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
                if (value != rotation)
                {
                    OnRotationChanged(rotation, value);
                }
                rotation = value;
            }
        }

        /// <summary>
        /// Global "Gravity" which affects particles in this ParticleEmitter
        /// </summary>
        public Vector2 GlobalGravity
        {
            get;
            set;
        }

        /// <summary>
        /// Get the count of particles active in this emitter
        /// </summary>
        public int ParticleCount
        {
            get { return particles.Count; }
        }

        /// <summary>
        /// The host ParticleEffect
        /// </summary>
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
            random = new PseudoRandom();
            // by default, emitter is relative to the effect position
            Position = position;
            GlobalGravity = Vector2.Zero;
            name = "AbstractEmitter";            
        }
        #endregion

        #region New methods
        /// <summary>
        /// Immediately generates one or more particles, particle is created in the 
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
        #endregion

        #region From base class
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Particle p in particles)
            {
                p.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Called by the host ParticleEffects
        /// 
        /// Updates particles during their lifetime.
        /// 
        /// TODO! This will be refactored in future to use ParticleModifiers 
        /// instead of directly modifying the particle here.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            float elapsedMs = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            float elapsedSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            for (int i = 0; i < particles.Count; i++)
            {
                Particle p = particles[i];

                p.TTLPercent += elapsedMs / p.TTL;

                p.Scale = Interpolations.LinearInterpolate(
                    p.InitialScale, Configuration.ScaleRange.Y, p.TTLPercent);
                p.Opacity = Interpolations.LinearInterpolate(
                    p.InitialOpacity, Configuration.OpacityRange.Y, p.TTLPercent);
                p.AngularVelocity = Interpolations.LinearInterpolate(
                    p.InitialAngularVelocity, Configuration.AngularVelocityRange.Y, p.TTLPercent);
                p.Position += p.LinearVelocity * elapsedSeconds;
                p.Rotation += p.AngularVelocity * elapsedSeconds;
                p.LinearVelocity *= p.SpeedDamping;

                p.LinearVelocity += GlobalGravity;
                if (p.TTLPercent >= 1.0f)
                {
                    particles.RemoveAt(i);
                    i--;
                }
            }
        }
        #endregion

        #region Abstract methods
        /// <summary>
        /// Generates one new particle, must be implemented by concrete classes
        /// </summary>
        /// <returns>new Particle</returns>
        protected abstract void GenerateParticle(Particle p);
        #endregion

        #region Private methods
        private void OnRotationChanged(float oldRotation, float newRotation)
        {
            if (RotationChanged != null)
            {
                RotationEventArgs args = new RotationEventArgs(oldRotation, newRotation);
                RotationChanged(this, args);
            }
        }
        #endregion
    }
}
