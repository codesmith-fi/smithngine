using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Codesmith.SmithNgine.Gfx
{
    public class Sprite : IMovableObject2D, IOrderableObject
    {
        #region Fields
        private Texture2D texture;
        private Vector2 position;
        private float order;
        #endregion

        #region Properties
        public Vector2 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                Vector2 oldPos = this.position;
                this.position = value;
                // Call event after changing the position
                this.OnPositionChanged(oldPos, this.position);
            }
        }

        public float Order
        {
            get;
            set;
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
        public event EventHandler<EventArgs> OrderChanged;
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

        private void OnOrderChanged(float oldOrder, float newOrder)
        {
        }
        #endregion
    }
}
