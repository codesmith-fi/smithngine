/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */

namespace Codesmith.SmithNgine.Gfx
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Codesmith.SmithNgine.Input;
    using Codesmith.SmithNgine.Collision;
    using Codesmith.SmithNgine.General;

    /// <summary>
    /// A base Sprite class. Draws a texture with no animation but can report events.
    /// 
    /// Sprite is a graphics entity which has several properties like:
    /// <list type="bullet">
    /// <item>
    /// <term>Texture</term>
    /// <description>Texture to be drawn when drawing the Sprite</description>
    /// </item>
    /// <item>
    /// <term>Position</term>
    /// <description>Position of the sprite (Sprite origin affects how position is used)</description>
    /// </item>
    /// <item>
    /// <term>Origin</term>
    /// <description>Sprite origin, default origin is the centre of the sprite bounds</description>
    /// </item>
    /// <item>
    /// <term>Rotation</term>
    /// <description>Rotation of the sprite, 0.0f is no rotation</description>
    /// </item>
    /// <item>
    /// <term>Scale</term>
    /// <description>Scale factor of the Sprite, 1.0f is normal scale</description>
    /// </item>
    /// <item>
    /// <term>Color</term>
    /// <description>Default is Color.White, sprite color is tinted towards the set color</description>
    /// </item>
    /// <item>
    /// <term>Order</term>
    /// <description>Order of sprite entity in the Sprite base, can be used to control in which order the sprites are drawn</description>
    /// </item>
    /// </list>
    /// 
    /// Sprite can report several events for example:
    /// - Dragging (Mouse is dragged with mouse)
    /// - Focusing (Mouse was clicked on it)
    /// - Loosing focus (Mouse was clicked outside the sprite)
    /// - Changing of position, rotation, order
    /// - Hovering (Mouse is moving on top of the sprite)
    /// 
    /// </summary>
    public class Sprite : DrawableGameObject, IOrderableObject, IRotatableObject, IFocusableObject, IHoverableObject
    {
        #region Fields
        // Input source for this Sprite
        private IInputEventSource inputSource;
        // Rotation of the sprite
        private float rotation = 0.0f;
        // Scale factor of the sprite
        private float scale = 1.0f;
        // Order of the sprite
        private float order = 1.0f;
        // Original frame size of the sprite
        private Rectangle frameSize;
        // Texture to be used
        protected Texture2D texture;
        // Set when drag is enabled 
        protected bool dragEnabled = false;
        #endregion

        #region Events
        public event EventHandler<PositionEventArgs> PositionChanged;
        public event EventHandler<OrderEventArgs> OrderChanged;
        public event EventHandler<RotationEventArgs> RotationChanged;
        public event EventHandler<ScaleEventArgs> ScaleChanged;
        public event EventHandler<EventArgs> FocusGained;
        public event EventHandler<EventArgs> FocusLost;
        public event EventHandler<HoverEventArgs> BeingHovered;
        public event EventHandler<DragEventArgs> BeingDragged;
        public event EventHandler<DragEventArgs> LostDrag;
        #endregion

        #region Properties
        /// <summary>
        /// <value>True if Sprite has focus (mouse was clicked inside the Sprite)</value>
        /// </summary>
        public bool HasFocus
        {
            get;
            protected set;
        }

        /// <summary>
        /// Default origin is set to the center of the sprite. 
        /// Origin is drawn to the Sprites position. 
        /// Origin affects also to the rotation, Sprite is rotated around the Origin point
        /// <value>Sprite origin</value>
        /// </summary>
        public Vector2 Origin
        {
            get;
            set;
        }
        
        /// <summary>
        /// Color of the sprite, default is Color.White
        /// Sprite texture is tinted towards the given color when drawn. White = no effect
        /// <value>Color</value>
        /// </summary>
        public Color Color
        {
            get;
            set;
        }

        /// <summary>
        /// Set or Get sprite position.
        /// <value>Position of the sprite</value>
        /// </summary>
        /// <remarks>
        /// Setting a new position causes PositionChanged event
        /// </remarks>
        public override Vector2 Position
        {
            get { return base.Position; }
            set
            {
                Vector2 oldPos = base.Position;
                if (oldPos != value)
                {
                    base.Position = value;
                    // Call event after changing the position
                    this.OnPositionChanged(oldPos, base.Position);
                }
            }
        }

        /// <summary>
        /// Set or Get sprite rotation/orientation. Default is 0.0f (no rotation)
        /// <value>Rotation of the sprite</value>
        /// </summary>
        /// <remarks>
        /// Setting a new rotation causes RotationChanged event
        /// </remarks>        
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
        /// Set or Get sprite scale.
        /// <value>Scale of the sprite, Default is 1.0f (=no scaling)</value>
        /// </summary>
        /// <remarks>
        /// Setting a new value for scale, causes ScaleChanged event
        /// </remarks>
        public float Scale
        {
            get { return this.scale; }
            set
            {
                float oldScale = this.scale;
                if (oldScale != value)
                {
                    this.scale = value;
                    this.OnScaleChanged(oldScale, this.scale);
                }
            }
        }

        /// <summary>
        /// Set or Get the order value of the sprite. 
        /// <value>Order, between 0.0f and 1.0f</value>
        /// </summary>
        /// <remarks>
        /// Setting a new value for Order causes OrderChanged event
        /// </remarks>
        public float Order
        {
            get { return this.order; }
            set
            {
                float oldOrder = this.order;
                if (oldOrder != value)
                {
                    this.order = MathHelper.Clamp(value, 0.0f, 1.0f);
                    this.OnOrderChanged(oldOrder, this.order);
                }
            }
        }

        /// <summary>
        /// <value>True if sprite is being hovered</value>
        /// </summary>
        public bool IsHovered
        {
            get;
            private set;
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
        /// </summary>
        /// <remarks>
        /// When set, Sprite starts to listen for mouse button press/release and mouse change
        /// so it can report activation, focus and dragging etc.
        /// If not set, sprite can not report mouse/touch related events
        /// </remarks>
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
                Origin = new Vector2(frameSize.Width / 2, frameSize.Height / 2);
            }
        }

        /// <summary>
        /// Set or get the texture of the sprite
        /// </summary>
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
        #endregion

        #region From Base class
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (this.texture != null)
            {
                spriteBatch.Draw(this.texture, Position, null, Color, Rotation, Origin, Scale, SpriteEffects.None, Order);
            }
        }

        public override void Dispose()
        {
            //commented out for now
            //this.texture.Dispose();
            base.Dispose();
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

        private void OnScaleChanged(float oldScale, float newScale)
        {
            if (ScaleChanged != null)
            {
                ScaleEventArgs args = new ScaleEventArgs(oldScale, newScale);
                ScaleChanged(this, args);
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
