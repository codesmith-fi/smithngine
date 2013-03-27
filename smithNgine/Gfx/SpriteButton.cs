﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Codesmith.SmithNgine.Input;
using Codesmith.SmithNgine.MathUtil;

namespace Codesmith.SmithNgine.Gfx
{
    public class SpriteButton : Sprite, IAnimatedObject
    {
        #region Fields
        TimeSpan clickTimeSpan;
        float clickAnimValue;
        float idleAnimValue;
        int direction;
        float[] points = { 1.0f, 1.2f, 0.8f, 1.0f };
        float[] amounts = { 0.0f, 0.1f, 0.8f, 1.0f };
        #endregion

        #region Constructors
        public SpriteButton(Texture2D texture) 
            : base(texture)
        {
            clickTimeSpan = TimeSpan.FromSeconds(0.9f);
        }
        #endregion

        #region Events
        // Event which will trigger when this button was pressed
        public event EventHandler<EventArgs> ButtonClicked;
        #endregion

        #region New public methods
        public void ResetAnimation()
        {
            direction = 1;
            idleAnimValue = -MathHelper.Pi;
            clickAnimValue = 0.0f;
        }

        public void Animate(GameTime gameTime)
        {
            if (this.direction != 0)
            {
                if (!TransitionMath.LinearTransition(
                    gameTime.ElapsedGameTime, clickTimeSpan,
                    direction, ref clickAnimValue))
                {
                    this.direction = 0;
                }
                Scale = TransitionMath.SmoothTransition(points, amounts, clickAnimValue);
            }
            else
            {
                idleAnimValue += 0.15f;
                idleAnimValue = MathHelper.WrapAngle(idleAnimValue);
                Scale = 1.0f + ( (float)Math.Sin(idleAnimValue) / 70) ;
            }
        }
        #endregion

        #region From IFocusableObject
        public override void GainFocus()
        {
            base.GainFocus();
            if (this.ButtonClicked != null)
            {
                ButtonClicked(this, EventArgs.Empty);
            }
            ResetAnimation();
        }

        public override void LooseFocus()
        {
            base.LooseFocus();
            this.direction = 1;
        }
        #endregion

    }
}