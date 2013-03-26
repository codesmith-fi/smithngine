using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Codesmith.SmithNgine.Gfx
{
    public class Sprite : ObjectBase, IMovableObject2D, IOrderableObject, IRotatableObject
    {
        #region Fields
        private Texture2D texture;
        private Vector2 position;
        private float rotation = 0.0f;
        private float order = 1.0f;
        #endregion

        #region Properties
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

        #endregion

        #region Constructors
        public Sprite(Texture2D texture)
        {
            this.texture = texture;
            // By default, sprite origin is the center
            Origin = new Vector2(this.texture.Bounds.Width / 2, this.texture.Bounds.Height / 2);
            Scale = 1.0f;
        }
        #endregion

        #region Events
        public event EventHandler<PositionEventArgs> PositionChanged;
        public event EventHandler<OrderEventArgs> OrderChanged;
        public event EventHandler<RotationEventArgs> RotationChanged;
        #endregion

        #region Public virtual methods
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Rectangle pos = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            Color color = Color.White;
            color = Color.White * TransitionSource.TransitionValue;
            spriteBatch.Draw(this.texture, Position, null, color, Rotation, Origin, Scale, SpriteEffects.None, Order);
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
    }
}
