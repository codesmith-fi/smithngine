using Microsoft.Xna.Framework;

namespace Codesmith.SmithNgine.Gfx
{
    interface IAnimatedObject
    {
        void ResetAnimation();
        void Animate(GameTime gameTime);
    }
}
