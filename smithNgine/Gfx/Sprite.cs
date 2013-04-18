using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Codesmith.SmithNgine.Input;
using Codesmith.SmithNgine.Collision;
using Codesmith.SmithNgine.General;

namespace Codesmith.SmithNgine.Gfx
{
    public class Sprite : GameObjectBase, IMovableObject2D, IOrderableObject, IRotatableObject, IFocusableObject, IHoverableObject, ICollidableObject
    {
        #region Fields
        private IInputEventSource inputSource;
        protected Texture2D texture;
        private Vector2 position = Vector2.Zero;
        private float rotation = 0.0f;
        private float order = 1.0f;
        bool dragEnabled = false;
        private Rectangle frameSize;
        #endregion

        #region Events
        public event EventHandler<PositionEventArgs> PositionChanged;
        public event EventHandler<OrderEventArgs> OrderChanged;
        public event EventHandler<RotationEventArgs> RotationChanged;
        public event EventHandler<EventArgs> FocusGained;
        public event EventHandler<EventArgs> FocusLost;
        public event EventHandler<HoverEventArgs> BeingHovered;
        public event EventHandler<DragEventArgs> BeingDragged;
        public event EventHandler<DragEventArgs> LostDrag;
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

        /// <summary>
        /// Set or get the Order of the Sprite
        /// Setting triggers event OrderChanged
        /// </summary>
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

        public Rectangle CollisionBounds
        {
            get { return Bounds; }
        }

        /// <summary>
        /// Return Bounds of the sprite taking account of origin and scale 
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                Vector2 pos = Position - ( Origin * Scale );
                float width = (float)FrameSize.Width * Scale;
                float height = (float)FrameSize.Height * Scale;
                return new Rectangle((int)pos.X, (int)pos.Y, (int)width, (int)height);
            }
        }

        /// <summary>
        /// Set or get the input event source for this Sprite
        /// When set, Sprite starts to listen for mouse button press/release and mouse change
        /// so it can report activation, focus and dragging etc.
        /// </summary>
        public IInputEventSource InputEventSource
        {
            get { return inputSource; }
            set
            {
                inputSource = value;
                if (value == null)
                {
                    inputSource.MouseButtonPressed -= mouseSource_MouseButtonPressed;
                    inputSource.MouseButtonReleased -= inputSource_MouseButtonReleased;
                    inputSource.MousePositionChanged -= mouseSource_MousePositionChanged;
                }
                else
                {
                    inputSource.MouseButtonPressed += mouseSource_MouseButtonPressed;
                    inputSource.MouseButtonReleased += inputSource_MouseButtonReleased;
                    inputSource.MousePositionChanged += mouseSource_MousePositionChanged;
                }
            }
        }

        /// <summary>
        /// Return or set the unscaled size of the original texture
        /// </summary>
        public Rectangle FrameSize
        {
            get { return this.frameSize; }
            protected set
            {
                frameSize = value;
                // By default, sprite origin is the center
                Origin = new Vector2(FrameSize.Width / 2, FrameSize.Height / 2);
            }
        }

        public Texture2D Texture
        {
            get { return this.texture; }
            protected set 
            { 
                this.texture = value;
                if (this.texture != null)
                {
                    InitSprite(texture.Bounds);
                }
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiate the sprite and initialize a texture for it. 
        /// It also calculates bounds etc for the sprite
        /// </summary>
        /// <param name="texture">The texture to set for this sprite</param>
        public Sprite(Texture2D texture)
        {
            Texture = texture;            
        }

        /// <summary>
        /// Instantiate the sprite, does not initialize any texture for it.
        /// Sets the bounds and other parameters depending on the given param.
        /// </summary>
        /// <param name="spriteBounds">Bounds for the new sprite, size</param>
        public Sprite(Rectangle spriteBounds)
        {
            InitSprite(spriteBounds);
        }
        #endregion

        #region New methods - can be overridden 
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (this.texture != null)
            {
                spriteBatch.Draw(this.texture, Position, null, Color, Rotation, Origin, Scale, SpriteEffects.None, Order);
            }
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

        protected virtual void OnDragLost(Vector2 delta)
        {
            if (LostDrag != null)
            {
                LostDrag(this, new DragEventArgs(delta));
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

        public bool CheckCollision(ICollidableObject another)
        {
            return this.CollisionBounds.Intersects(another.CollisionBounds);
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

        void mouseSource_MousePositionChanged(object sender, MouseEventArgs e)
        {
            if (ObjectIsActive)
            {
                Point p = new Point(e.State.X, e.State.Y);
                bool contained = Bounds.Contains(p);
                // Is this sprite being dragged? 
                if (e.State.LeftButton && e.PreviousState.LeftButton && dragEnabled)
                {
                    OnDrag(e.State.Position - e.PreviousState.Position);
                }

                if (contained)
                {
                    // Handle hovering, coords are relative to the object
                    Vector2 innerPos = new Vector2(p.X - Bounds.X, p.Y - Bounds.Y);
                    OnHover(innerPos);
                    this.IsHovered = true;
                }
                else
                {
                    this.IsHovered = false;
                }
            }
        }

        void inputSource_MouseButtonReleased(object sender, MouseEventArgs e)
        {
            if (dragEnabled)
            {
                // Report loosing the drag status and report last delta of movement
                OnDragLost(e.State.Position - e.PreviousState.Position);
            }
        }

        private void mouseSource_MouseButtonPressed(object sender, MouseEventArgs e)
        {
            if (ObjectIsActive)
            {
                Point p = new Point(e.State.X, (int)e.State.Y);
                if (Bounds.Contains(p))
                {
                    HandleMouseInsideClick(e);
                }
                else if (HasFocus)
                {
                    HandleMouseOutsideClick(e);
                }
            }
        }

        private void InitSprite(Rectangle spriteBounds)
        {
            FrameSize = spriteBounds;
            Position = Vector2.Zero;
            Scale = 1.0f;
            this.Color = Color.White;
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
    }
}
