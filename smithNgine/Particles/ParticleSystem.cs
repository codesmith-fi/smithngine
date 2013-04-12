/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Codesmith.SmithNgine.Particles
{
    public enum ParticleSystemStatus
    {
        Idle,
        Running,
        Paused
    }

    public class ParticleSystem
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

        public int EffectCount
        {
            get
            {
                return effects.Count;
            }
        }

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

        public bool IsPaused
        {
            get
            {
                return status == ParticleSystemStatus.Paused;
            }
        }

        public bool IsRunning
        {
            get
            {
                return status == ParticleSystemStatus.Running;
            }
        }

        public ParticleSystem()
        {
            effects = new List<ParticleEffect>();
            this.status = ParticleSystemStatus.Idle;
        }

        public void AddEffect(ParticleEffect newEffect)
        {
            effects.Add(newEffect);
        }

        public void RemoveEffect(ParticleEffect effect)
        {
            effects.Remove(effect);
        }

        public void Pause()
        {
            this.status = ParticleSystemStatus.Paused;
        }

        public void Resume()
        {
            this.status = ParticleSystemStatus.Running;
        }

        public void Clear()
        {
            this.status = ParticleSystemStatus.Idle;
            effects.Clear();
        }

        public void Update(GameTime gameTime)
        {
            foreach (ParticleEffect eff in effects)
            {
                eff.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ParticleEffect eff in effects)
            {
                eff.Draw(spriteBatch);
            }
        }
    }
}
