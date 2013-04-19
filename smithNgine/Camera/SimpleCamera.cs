/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Codesmith.SmithNgine.Camera
{
    /// <summary>
    /// Very basic camera, only position and movement
    /// </summary>
    public class SimpleCamera
    {
        #region Fields
        // Position of the camera
        private Vector2 position;
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the position of the camera
        /// <value>Position as a Vector2</value>
        /// </summary>
        public Vector2 Position 
        { 
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Get the X position of the camera
        /// <value>X position as a float</value>
        /// </summary>
        public float X
        {
            get { return position.X; }
            set { position.X = value; }
        }

        /// <summary>
        /// Get the Y position of the camera
        /// <value>Y position as a float</value>
        /// </summary>
        public float Y
        {
            get { return position.Y; }
            set { position.Y = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Construct a camera with given position
        /// </summary>
        /// <param name="position">Position as a Vector2</param>
        public SimpleCamera(Vector2 position)
        {
            Position = position;
        }

        /// <summary>
        /// Construct a camera with position Vector2.Zero
        /// </summary>
        public SimpleCamera()
            : this(Vector2.Zero)
        {
        }
        #endregion

        #region New methods
        /// <summary>
        /// Move the camera with given delta position
        /// <example>
        /// Move camera 10 units right
        /// <code>
        ///     myCamera.Move(new Vector2(10,0));
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="delta">Amount of units to move as a Vector2</param>
        public virtual void Move(Vector2 delta)
        {
            position += delta;
        }

        /// <summary>
        /// Move the camera on X axis
        /// </summary>
        /// <param name="deltaX">X coordinate delta as float</param>
        public virtual void MoveX(float deltaX)
        {
            position.X += deltaX;
        }

        /// <summary>
        /// Move the camera on Y axis
        /// </summary>
        /// <param name="deltaX">Y coordinate delta as float</param>
        public virtual void MoveY(float deltaY)
        {
            position.Y += deltaY;
        }
        #endregion
    }
}

