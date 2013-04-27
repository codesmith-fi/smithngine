/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */

namespace Codesmith.SmithNgine.General
{
    using System;
    using Microsoft.Xna.Framework;
    using Codesmith.SmithNgine.Gfx;

    /// <summary>
    /// Base class for objects that have a 2D position and can move
    /// </summary>
    public abstract class MovableGameObject : GameObjectBase
    {
        #region Fields
        private Vector2 position;
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the position
        /// <value>Vector2 position</value>
        /// </summary>
        public virtual Vector2 Position
        {
            get { return position; }
            set 
            {
                if (value != position)
                {
                    OnPositionChanged(position, value);
                }
                position = value; 
            }
        }

        /// Get the X position of the object
        /// <value>X position as a float</value>
        /// </summary>
        public virtual float X
        {
            get { return position.X; }
            set 
            {
                if (value != position.X)
                {
                    OnPositionChanged(position, new Vector2(value, position.Y));
                }
                position.X = value; 
            }
        }

        /// <summary>
        /// Get the Y position of the object
        /// <value>Y position as a float</value>
        /// </summary>
        public virtual float Y
        {
            get { return position.Y; }
            set
            {
                if (value != position.Y)
                {
                    OnPositionChanged(position, new Vector2(position.X, value));
                }
                position.Y = value;
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Event triggered when the position changes
        /// </summary> 
        public event EventHandler<PositionEventArgs> PositionChanged;
        #endregion

        #region New methods
        /// <summary>
        /// Move the object with given delta position
        /// <example>
        /// Move camera 10 units right
        /// <code>
        ///     myObject.Move(new Vector2(10,0));
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="delta">Amount of units to move as a Vector2(X,Y)</param>
        public virtual void Move(Vector2 delta)
        {
            Position += delta;
        }

        /// <summary>
        /// Move the object on X axis
        /// </summary>
        /// <param name="deltaX">X coordinate delta as float</param>
        public virtual void MoveX(float deltaX)
        {
            X += deltaX;
        }

        /// <summary>
        /// Move the object on Y axis
        /// </summary>
        /// <param name="deltaX">Y coordinate delta as float</param>
        public virtual void MoveY(float deltaY)
        {
            Y += deltaY;
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
        #endregion
    }
}
