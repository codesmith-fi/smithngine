using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Codesmith.SmithNgine.General;

namespace Codesmith.SmithNgine.Effects
{
    public static class EffectFactory
    {
        public enum EffectType
        {
            Fade = 1,
            GaussianBlur,
            Scale,
            Wiggle2d
        }

        public static Effect Load(EffectType type, Game game)
        {
            IContentManagerService service = (IContentManagerService)game.Services.GetService(typeof(IContentManagerService));            
            Effect e = null;
            switch (type)
            {
                case EffectType.Fade:
                    {
                        e = service.Content.Load<Effect>("Effects/Fade");
                        break;
                    }
                case EffectType.GaussianBlur:
                    {
                        e = service.Content.Load<Effect>("Effects/GaussianBlur");
                        break;
                    }
                case EffectType.Scale:
                    {
                        e = service.Content.Load<Effect>("Effects/Scale");
                        break;
                    }
                case EffectType.Wiggle2d:
                    {
                        e = service.Content.Load<Effect>("Effects/Wiggle2d");
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("Effect not implemented", type.ToString());
                    }
            }
            return e;
        }
    }
}
