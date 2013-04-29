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
    using System.Collections.Generic;
    using System.Diagnostics;
    using Codesmith.SmithNgine.Gfx;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Statuses of the ParticleSystem
    /// </summary>
    public enum ParticleSystemStatus
    {
        Idle,
        Running,
        Paused
    }

    /// <summary>
    /// Implements a particle system.
    /// 
    /// Particle system manages a set of ParticleEffects.
    /// 
    /// Call Update() in gameloop.
    /// 
    /// </summary>
    public class ParticleSystem : DrawableGameObject
    {
        #region Fields
        private List<ParticleEffect> effects;
        private ParticleSystemStatus status;
        #endregion

        /// <summary>
        /// Get the count of all particles in this system
        /// </summary>
        public int ParticleCount
        {
            get
            {
                int c = 0;
                foreach (ParticleEffect eff in effects)
                {
                    c += eff.ParticleCount;
                }
                return c;
            }
        }

        /// <summary>
        /// Get the number of effects managed by this system
        /// </summary>
        public int EffectCount
        {
            get
            {
                return effects.Count;
            }
        }

        /// <summary>
        /// Get the number of particle emitters in the system
        /// </summary>
        public int EmitterCount
        {
            get
            {
                int c = 0;
                foreach (ParticleEffect eff in effects)
                {
                    c += eff.EmitterCount;
                }
                return c;
            }
        }

        /// <summary>
        /// Is this particle system paused
        /// <value>true if paused, false if running</value> 
        /// </summary>
        public bool IsPaused
        {
            get
            {
                return status == ParticleSystemStatus.Paused;
            }
        }

        /// <summary>
        /// Is this particle system running (means: Update() is called for subsystems)
        /// <value>true if running, false if paused</value>
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return status == ParticleSystemStatus.Running;
            }
        }

        /// <summary>
        /// The particle bool/cache
        /// </summary>
        internal ParticlePool Pool
        {
            set;
            get;
        }

        #region Constructor
        /// <summary>
        /// Constructs a new particle system
        /// </summary>
        public ParticleSystem() 
        {
            effects = new List<ParticleEffect>();
            this.status = ParticleSystemStatus.Idle;
            Resume();
        }
        #endregion

        #region New methods
        /// <summary>
        /// Add a particle effect to this system
        /// </summary>
        /// <param name="newEffect">ParticleEffect to be added</param>
        public void AddEffect(ParticleEffect newEffect)
        {
            Debug.Assert(!effects.Contains(newEffect), "Can't add same effect twice");
            newEffect.ParticleSystem = this;
            effects.Add(newEffect);
        }

        /// <summary>
        /// Remove particle effect from the system
        /// </summary>
        /// <param name="effect"></param>
        public void RemoveEffect(ParticleEffect effect)
        {
            effects.Remove(effect);
        }

        /// <summary>
        /// Pause the system
        /// </summary>
        public void Pause()
        {
            this.status = ParticleSystemStatus.Paused;
        }

        /// <summary>
        /// Resume the system
        /// </summary>
        public void Resume()
        {
            this.status = ParticleSystemStatus.Running;
        }

        /// <summary>
        /// Clear the system, removes all effects
        /// </summary>
        public void Clear()
        {
            this.status = ParticleSystemStatus.Idle;
            effects.Clear();
        }
        #endregion

        /// <summary>
        /// Enable particle pool/cache 
        /// </summary>
        /// <param name="amount"></param>
        public void EnableCache(int amount, int maxsize)
        {
            Pool = new ParticlePool(amount, maxsize);
        }

        #region From Base class
        public override void Update(GameTime gameTime)
        {
            // Update particles unless the system is paused
            if (IsRunning)
            {
                foreach (ParticleEffect eff in effects)
                {
                    eff.Update(gameTime);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (ParticleEffect eff in effects)
            {
                eff.Draw(spriteBatch, gameTime);
            }
        }
        #endregion
    }
}
