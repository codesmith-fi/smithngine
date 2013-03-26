using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Codesmith.SmithNgine.Gfx
{
    public class Sprite : IMovableObject2D, IOrderableObject, IRotatableObject
    {
        #region Fields
        private Texture2D texture;
        private Vector2 position;
        private float rotation = 0.0f;
        private float order = 1.0f;
        #endregion

        #region Properties
        public Vector2 Position
        {
            get { return this.position; }
            set
            {
                Vector2 oldPos = this.position;
                this.position = value;
                // Call event after changing the position
                this.OnPositionChanged(oldPos, this.position);
            }
        }

        public float Rotation
        {
            get { return this.rotation; }
            set
            {
                float oldRotation = this.rotation;
                this.rotation = value;
                this.OnRotationChanged(oldRotation, this.rotation);
            }
        }

        public float Order
        {
            get { return this.order; }
            set
            {
                float oldOrder = this.order;
                this.order = value;
                this.OnOrderChanged(oldOrder, this.order);
            }
        }

        public ITransitionSource TransitionSource
        {
            get;
            set;
        }

        #endregion

        #region Constructors
        public Sprite(Texture2D texture)
        {
            this.texture = texture;
            TransitionSource = null;
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
            if (TransitionSource != null)
            {
                color = Color.White * TransitionSource.TransitionValue;
            }
            spriteBatch.Draw(this.texture, pos, color);
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
