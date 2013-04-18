using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Codesmith.SmithNgine.Input;
using Codesmith.SmithNgine.MathUtil;

namespace Codesmith.SmithNgine.Gfx
{
    [Flags]
    public enum ButtonStyle : int
    {
        NoAnimation     = 1,
        Highlight       = NoAnimation << 1,
        AnimateOnPress  = NoAnimation << 2,
        AnimateIdle     = NoAnimation << 3
    }

    public class SpriteButton : Sprite
    {
        #region Fields
        float idleAnimAngle;
        int direction;
        float hoverScale;
        protected PlayerIndex? activePlayerIndex = null;
        Keys activationKey = Keys.None;

        float currentAngle = 0.0f;
        float currentAmplify = 1.0f;
        const float AngleMin = MathHelper.Pi / 2f;
        const float AngleMax = MathHelper.Pi;
        const int MaxIterations = 5;
        bool animatingIn = false;
        bool animatingOut = false;
        #endregion

        #region Properties
        public ButtonStyle ButtonClickStyle
        {
            get;
            set;
        }

        public TimeSpan ClickBounceSpeed
        {
            set;
            get;
        }

        public float AnimState
        {
            get { return this.idleAnimAngle; }
            set { this.idleAnimAngle = value; }
        }

        #endregion

        #region Constructors
        public SpriteButton(Texture2D texture) 
            : base(texture)
        {
            ClickBounceSpeed = TimeSpan.FromSeconds(0.15f);
            idleAnimAngle = 0.0f;
            ButtonClickStyle = ButtonStyle.NoAnimation;
            hoverScale = 1.0f;
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
            if (ObjectIsActive && e.keys.Length > 0 && activationKey != Keys.None )
            {
                foreach (Keys k in e.keys)
                {
                    if (k == activationKey)
                    {
                        activePlayerIndex = e.player;
                        GainFocus();
                    }
                }
            }
        }
        #endregion

        #region Methods overridden from base
        public override void Update(GameTime gameTime)
        {
            if (IsHovered && ( ( ButtonClickStyle & ButtonStyle.Highlight ) == ButtonStyle.Highlight))
            {
                this.hoverScale = 1.1f;
            }
            else
            {
                this.hoverScale = 1.0f;
            }

            float animScale = 1.0f;
            // Are we animating
            if ( (animatingIn || animatingOut ) && ((ButtonClickStyle & ButtonStyle.AnimateOnPress)==ButtonStyle.AnimateOnPress))
            {
                animScale = GetAnimationScale(gameTime);
            }

            float idleScale = 1.0f;
            if( (ButtonClickStyle & ButtonStyle.AnimateIdle) == ButtonStyle.AnimateIdle)
            {
                idleAnimAngle += 0.15f;
                idleAnimAngle = MathHelper.WrapAngle(idleAnimAngle);
                idleScale = 1.0f + ((float)Math.Sin(idleAnimAngle) / 70);
            }

            Scale = hoverScale * animScale * idleScale;
        }

        private float GetAnimationScale(GameTime gameTime)
        {
            if (!TransitionMath.LinearTransition2(gameTime.ElapsedGameTime, ClickBounceSpeed, direction, ref currentAngle, AngleMin, AngleMax))
            {
                if (direction > 0)
                {
                    currentAngle = AngleMax;
                    direction = -1;
                    currentAmplify *= 0.5f;
                    if (currentAmplify <= 0.05f)
                    {
                        currentAmplify = 0.0f;
                    }
                }
                else
                {
                    currentAngle = AngleMin;
                    direction = 1;
                }
            }
            float scale;
            if (animatingIn)
            {
                scale = (3.0f / 4f) + (float)Math.Sin(currentAngle) / 4f * currentAmplify;
            }
            else
            {
                scale = 1.0f - ((float)Math.Sin(currentAngle) / 4f * currentAmplify);
            }
            return scale;
        }

        public override void GainFocus()
        {
            base.GainFocus();
            LostDrag += SpriteButton_LostDrag;
            if (this.ButtonClicked != null)
            {
                ButtonClicked(this, EventArgs.Empty);
            }
            animatingIn = true;
            ResetAnimation();
        }

        public override void LooseFocus()
        {
            base.LooseFocus();
            LostDrag -= SpriteButton_LostDrag;
            this.direction = 0;
        }

        private void SpriteButton_LostDrag(object sender, DragEventArgs e)
        {
            LostDrag -= SpriteButton_LostDrag;
            animatingIn = false;
            animatingOut = true;
            ResetAnimation();
        }

        public override void Dismiss()
        {
            InputEventSource.KeysPressed -= keySource_KeysPressed; 
            this.direction = 0;
            this.Scale = 1.0f;
        }
        #endregion

        private void ResetAnimation()
        {
            currentAngle = AngleMin;
            currentAmplify = 1.0f;
            direction = 1;
        }
    }
}
