using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Codesmith.SmithNgine.Input;

namespace Codesmith.SmithNgine.Gfx
{
    public class SpriteButton : Sprite, IAnimatedObject
    {
        TimeSpan animTime;
        float targetScale;
        float animTransitionValue;
        int direction;

        public SpriteButton(Texture2D texture) 
            : base(texture)
        {
            animTime = TimeSpan.FromSeconds(1.0f);
        }

        public void ResetAnimation()
        {
            targetScale = 0.50f;
            animTransitionValue = 1.0f;
            direction = -1;
        }

        public void Animate(GameTime gameTime)
        {
            if (this.direction != 0)
            {
                if (!UpdateAnimation(gameTime))
                {
                    this.direction = 0;
                }
                Scale = animTransitionValue;
            }
        }

        private bool UpdateAnimation(GameTime gameTime)
        {
            direction = (direction > 0) ? 1 : -1;
            double elapsedMs = gameTime.ElapsedGameTime.TotalMilliseconds;
            double transitionMs = animTime.TotalMilliseconds;

            float delta = (float)(elapsedMs / transitionMs);
            this.animTransitionValue += delta * direction;

            // Ensure that the transition value is kept in: 0.0f <= transitionValue <= 1.0f
            if (((direction > 0) && (this.animTransitionValue > 1.0f)) ||
                ((direction < 0) && (this.animTransitionValue < this.targetScale)))
            {
                // We are done transitioning in or out, clamp the value and end transition
                this.animTransitionValue = MathHelper.Clamp(
                    this.animTransitionValue, this.targetScale, 1.0f);
                return false;
            }
            // Still animating
            return true;
        }

        public override void GainFocus()
        {
            base.GainFocus();
            this.direction = -1;
            ResetAnimation();
        }

        public override void LooseFocus()
        {
            base.LooseFocus();
            this.direction = 1;
            targetScale = 1.0f;
        }

    }
}
