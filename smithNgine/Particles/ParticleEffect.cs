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
    using System.Diagnostics;
    using System.Collections.Generic;
    using Codesmith.SmithNgine.General;
    using Codesmith.SmithNgine.Gfx;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// ParticleEffect class
    /// Each effect can contain multiple emitters. 
    /// Particles are owned by the Effect
    /// </summary>
    public class ParticleEffect : DrawableGameObject, IRotatableObject
    {
        #region Fields
        const int MaxParticles = 10000;
        private List<ParticleEmitter> emitters;
        private TimeSpan timeLeft = TimeSpan.Zero;
        private float rotation;
        #endregion

        #region Events
        /// <summary>
        /// Event for rotation changes
        /// </summary>
        public event EventHandler<RotationEventArgs> RotationChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the name of this ParticleEffect
        /// <value>String name</value>
        /// </summary>
        public string Name
        {
            get;
            set;
        }

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
        /// Get or set the rotation for this ParticeEffect
        /// Triggers OnRotationChanged when rotation actually changes
        /// if someone has registered for the event
        /// </summary>
        public float Rotation
        {
            get { return rotation; }
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
        /// Get readonly list of emitters
        /// </summary>
        public IList<ParticleEmitter> Emitters
        {
            get { return this.emitters.AsReadOnly(); }
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
        /// The particle system owning this effect
        /// </summary>
        internal ParticleSystem ParticleSystem
        {
            set;
            get;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public ParticleEffect()
        {
            emitters = new List<ParticleEmitter>();
            GravityVector = new Vector2(0.0f, 0.0f);
            Name = "Default Effect";
        }
        #endregion

        #region New methods
        /// <summary>
        /// Generate particles for the given time period
        /// </summary>
        /// <param name="duration">Duration</param>
        public void Generate(TimeSpan duration)
        {
            timeLeft = duration;
        }

        /// <summary>
        /// Immediately generate the given amount of particles
        /// </summary>
        /// <param name="amount">How many to generate</param>
        public void Generate(int amount)
        {
            foreach (ParticleEmitter em in emitters)
            {             
                em.Generate(amount);
            }
        }

        /// <summary>
        /// Add a ParticleEmitter to this ParticleEffect
        /// <remarks>
        /// Emitter can not be added twice! Fails in debug builds.
        /// </remarks>
        /// </summary>
        /// <param name="emitter">The emitter to add</param>
        public void AddEmitter(ParticleEmitter emitter)
        {
            Debug.Assert(!emitters.Contains(emitter), "Can't add emitter twice");
            emitter.Effect = this;
            emitters.Add(emitter);
        }

        /// <summary>
        /// Remove the given emitter from this effect
        /// </summary>
        /// <param name="emitter">Emitter to remove</param>
        public void RemoveEmitter(ParticleEmitter emitter)
        {
            emitters.Remove(emitter);
        }

        /// <summary>
        /// Clear the whole Effect, removes all emitters
        /// </summary>
        public void Clear()
        {
            emitters.Clear();
        }
        #endregion

        #region From base class
        /// <summary>
        /// Update method, should be called periodically for active effects
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public override void Update(GameTime gameTime)
        {
            // Create new particles with emitters
            foreach (ParticleEmitter em in emitters)
            {
                if (ParticleCount < MaxParticles)
                {
                    if (em.Flags.HasFlag(EmitterModes.AutoGenerate))
                    {
                        em.Generate((float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                    else if (timeLeft > TimeSpan.Zero)
                    {
                        em.Generate((float)gameTime.ElapsedGameTime.TotalSeconds);
                        timeLeft -= gameTime.ElapsedGameTime;
                    }
                }
                em.Update(gameTime);
            }
        }

        /// <summary>
        /// Draws all the particles in this effect
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Draw all existing particles on emitters
            foreach (ParticleEmitter em in emitters)
            {
                em.Draw(spriteBatch, gameTime);
            }
        }
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
