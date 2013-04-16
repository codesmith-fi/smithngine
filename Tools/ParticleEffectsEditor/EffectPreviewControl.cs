using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WinFormsExample;
using Codesmith.SmithNgine.Particles;
using Microsoft.Xna.Framework;

namespace Codesmith.smithNgine.ParticleEffectsEditor
{
    public class EffectPreviewControl : GraphicsDeviceControl
    {
        private ParticleSystem particleSystem;

        public ParticleEffect Effect
        {
            get;
            set;
        }

        protected override void Initialize()
        {
            particleSystem = new ParticleSystem();
            Effect = new ParticleEffect();
            Effect.Rotation = 0f;
            Effect.Position = Vector2.Zero;
            ParticleEmitter emitter = new PointEmitter(Vector2.Zero);
            ParticleGenerationParams config = new ParticleGenerationParams();
            config.Flags = EmitterCastStyle.None;
            emitter.Configuration = config;
            Effect.AddEmitter(emitter);
            particleSystem.AddEffect(Effect);            
        }

        protected override void Draw()
        {
        }
    }
}
