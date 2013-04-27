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
    public abstract class MovableObject : GameObjectBase
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
            set { position = value; }
        }

        /// Get the X position of the object
        /// <value>X position as a float</value>
        /// </summary>
        public virtual float X
        {
            get { return position.X; }
            set { position.X = value; }
        }

        /// <summary>
        /// Get the Y position of the object
        /// <value>Y position as a float</value>
        /// </summary>
        public virtual float Y
        {
            get { return position.Y; }
            set { position.Y = value; }
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
            position += delta;
        }

        /// <summary>
        /// Move the object on X axis
        /// </summary>
        /// <param name="deltaX">X coordinate delta as float</param>
        public virtual void MoveX(float deltaX)
        {
            position.X += deltaX;
        }

        /// <summary>
        /// Move the object on Y axis
        /// </summary>
        /// <param name="deltaX">Y coordinate delta as float</param>
        public virtual void MoveY(float deltaY)
        {
            position.Y += deltaY;
        }
        #endregion
    }
}
