using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Codesmith.SmithNgine.Input;

namespace Codesmith.SmithNgine.Gfx
{
    public class Sprite : ObjectBase, IMovableObject2D, IOrderableObject, IRotatableObject, IFocusableObject, IHoverableObject
    {
        #region Fields
        private IInputEventSource inputSource;
        private Texture2D texture;
        private Vector2 position;
        private float rotation = 0.0f;
        private float order = 1.0f;
        bool dragEnabled = false;
        #endregion

        #region Events
        public event EventHandler<PositionEventArgs> PositionChanged;
        public event EventHandler<OrderEventArgs> OrderChanged;
        public event EventHandler<RotationEventArgs> RotationChanged;
        public event EventHandler<EventArgs> FocusGained;
        public event EventHandler<EventArgs> FocusLost;
        public event EventHandler<HoverEventArgs> BeingHovered;
        public event EventHandler<DragEventArgs> BeingDragged;
        #endregion

        #region Properties
        public bool HasFocus
        {
            get;
            protected set;
        }

        public Vector2 Origin
        {
            get;
            set;
        }

        public float Scale
        {
            get;
            set;
        }

        public Color Color
        {
            get;
            set;
        }

        public Vector2 Position
        {
            get { return this.position; }
            set
            {
                Vector2 oldPos = this.position;
                if (oldPos != value)
                {
                    this.position = value;
                    // Call event after changing the position
                    this.OnPositionChanged(oldPos, this.position);
                }
            }
        }

        public float Rotation
        {
            get { return this.rotation; }
            set
            {
                float oldRotation = this.rotation;
                if (oldRotation != value)
                {
                    this.rotation = value;
                    this.OnRotationChanged(oldRotation, this.rotation);
                }
            }
        }

        public float Order
        {
            get { return this.order; }
            set
            {
                float oldOrder = this.order;
                if (oldOrder != value)
                {
                    this.order = value;
                    this.OnOrderChanged(oldOrder, this.order);
                }
            }
        }

        public bool IsHovered
        {
            get;
            private set;
        }

        // Return rectangular boundingbox of the sprite, taking account of origin and scale
        public Rectangle BoundingBox
        {
            get
            {
                Vector2 pos = Position - ( Origin * Scale );
                float width = texture != null ? (float)texture.Bounds.Width : 0.0f;
                float height = texture != null ? (float)texture.Bounds.Height : 0.0f;
                width *= Scale;
                height *= Scale;
                return new Rectangle((int)pos.X, (int)pos.Y, (int)width, (int)height);
            }
        }

        public IInputEventSource InputEventSource
        {
            get { return inputSource; }
            set
            {
                inputSource = value;
                if (value == null)
                {
                    inputSource.MouseButtonPressed -= mouseSource_MouseButtonPressed;
                    inputSource.MousePositionChanged -= mouseSource_MousePositionChanged;
                }
                else
                {
                    inputSource.MouseButtonPressed += mouseSource_MouseButtonPressed;
                    inputSource.MousePositionChanged += mouseSource_MousePositionChanged;
                }
            }
        }
        void mouseSource_MousePositionChanged(object sender, MouseEventArgs e)
        {
            if (ObjectIsActive)
            {
                // Is this sprite being dragged? 
                if (e.State.LeftButton && e.PreviousState.LeftButton && dragEnabled)
                {
                    OnDrag(e.State.Position - e.PreviousState.Position);
                }

                Point p = new Point(e.State.X, e.State.Y);
                if (BoundingBox.Contains(p))
                {
                    // Handle hovering, coords are relative to the object
                    Vector2 innerPos = new Vector2(p.X - BoundingBox.X, p.Y - BoundingBox.Y);
                    OnHover(innerPos);
                    this.IsHovered = true;
                }
                else
                {
                    dragEnabled = false;
                    this.IsHovered = false;
                }
            }
        }

        private void mouseSource_MouseButtonPressed(object sender, MouseEventArgs e)
        {
            if (ObjectIsActive)
            {
                Point p = new Point(e.State.X, (int)e.State.Y);
                if(BoundingBox.Contains(p))
                {
                    HandleMouseInsideClick(e);
                }
                else if (HasFocus)
                {
                    HandleMouseOutsideClick(e);
                }
            }
        }

        // Handles mouseclick on this button
        protected virtual void HandleMouseInsideClick(MouseEventArgs args) 
        {
            if (args.State.LeftButton)
            {
                dragEnabled = true;
                GainFocus();
            }
        }

        protected virtual void HandleMouseOutsideClick(MouseEventArgs args)
        {
            if (args.State.LeftButton && HasFocus)
            {
                dragEnabled = false;
                LooseFocus();
            }
        }
        #endregion

        #region Constructors
        public Sprite(Texture2D texture)
        {
            this.texture = texture;
            // By default, sprite origin is the center
            Position = Vector2.Zero;
            Origin = new Vector2(this.texture.Bounds.Width / 2, this.texture.Bounds.Height / 2);
            Scale = 1.0f;
            this.Color = Color.White;
        }
        #endregion

        #region Public virtual methods
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Rectangle pos = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            Color color = this.Color * TransitionSource.TransitionValue;
            spriteBatch.Draw(this.texture, Position, null, color, Rotation, Origin, Scale, SpriteEffects.None, Order);
        }

        public virtual void GainFocus()
        {
            if (FocusGained != null && !HasFocus)
            {
                FocusGained(this, EventArgs.Empty);
            }
            HasFocus = true;
        }

        public virtual void LooseFocus()
        {
            if (FocusLost != null && HasFocus)
            {
                FocusLost(this, EventArgs.Empty);
            }
            HasFocus = false;
        }

        protected virtual void OnDrag(Vector2 delta)
        { 
            if( BeingDragged != null)
            {
                BeingDragged(this, new DragEventArgs(delta));
            }
        }

        protected virtual void OnHover(Vector2 position)
        {
            if (this.BeingHovered != null)
            {
                HoverEventArgs args = new HoverEventArgs();
                args.position = position;
                BeingHovered(this, args);
            }
        }

        #endregion

        #region Private methods
        private void OnPositionChanged(Vector2 oldPosition, Vector2 newPosition)
        {
            if (PositionChanged != null)
            {
                PositionEventArgs args = new PositionEventArgs(oldPosition, newPosition);
                PositionChanged(this, args);
            }
        }

        private void OnRotationChanged(float oldRotation, float newRotation)
        {
            if (RotationChanged != null)
            {
                RotationEventArgs args = new RotationEventArgs(oldRotation, newRotation);
                RotationChanged(this, args);
            }
        }

        private void OnOrderChanged(float oldOrder, float newOrder)
        {
            if (OrderChanged != null)
            {
                OrderEventArgs args = new OrderEventArgs(oldOrder, newOrder);
                OrderChanged(this, args);
            }
        }
        #endregion

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
