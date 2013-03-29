using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Codesmith.SmithNgine.Input;
using Codesmith.SmithNgine.MathUtil;
using Microsoft.Xna.Framework.Input;

namespace Codesmith.SmithNgine.Gfx
{
    public class SpriteButton : Sprite
    {
        #region Fields
        TimeSpan clickTimeSpan;
        float clickAnimValue;
        float idleAnimValue;
        int direction;
        float hoverScale;
        float[] points = { 1.0f, 1.2f, 0.8f, 1.0f };
        float[] amounts = { 0.0f, 0.1f, 0.8f, 1.0f };
        Keys activationKey;
        #endregion

        #region Properties
        public float AnimState
        {
            get { return this.idleAnimValue; }
            set { this.idleAnimValue = value; }
        }

        #endregion

        #region Constructors
        public SpriteButton(Texture2D texture) 
            : base(texture)
        {
            clickTimeSpan = TimeSpan.FromSeconds(0.9f);
            idleAnimValue = 0.0f;
        }
        #endregion

        #region Events
        // Event which will trigger when this button was pressed
        public event EventHandler<EventArgs> ButtonClicked;
        #endregion

        #region New methods 
        // Causes this button to listen for the defined key. Acts like Mouse click
        public void BindKey(Keys key)
        {
            activationKey = key;
            InputEventSource.KeysPressed += keySource_KeysPressed;
        }

        void keySource_KeysPressed(object sender, KeyboardEventArgs e)
        {
            if (ObjectIsActive && e.keys.Length > 0)
            {
                foreach (Keys k in e.keys)
                {
                    if (k == activationKey)
                    {
                        GainFocus();
                    }
                }
            }
        }
        #endregion

        #region Methods overridden from base
        protected override void OnHover(Vector2 position)
        {
            base.OnHover(position);
        }

        public override void Update(GameTime gameTime)
        {
            if (IsHovered)
            {
                this.hoverScale = 1.1f;
            }
            else
            {
                this.hoverScale = 1.0f;
            }

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
                Scale = hoverScale * (1.0f + ( (float)Math.Sin(idleAnimValue) / 70) );
            }
        }

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
            this.direction = 0;
        }

        public override void Dismiss()
        {
            this.direction = 0;
            this.Scale = 1.0f;
        }

        public override void DeactivateObject()
        {
            base.DeactivateObject();
        }
        #endregion

        private void ResetAnimation()
        {
            direction = 1;
            idleAnimValue = -MathHelper.Pi;
            clickAnimValue = 0.0f;
        }

    }
}
