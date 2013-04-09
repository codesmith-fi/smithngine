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
        private List<ParticleEffect> effects;
        private ParticleSystemStatus status;
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
