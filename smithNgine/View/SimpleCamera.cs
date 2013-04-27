/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
namespace Codesmith.SmithNgine.View
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Codesmith.SmithNgine.General;

    /// <summary>
    /// Very basic camera, only position and movement
    /// </summary>
    public class SimpleCamera : MovableObject
    {
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
    }
}

