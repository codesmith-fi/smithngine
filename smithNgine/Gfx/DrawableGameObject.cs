/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */

namespace Codesmith.SmithNgine.Gfx
{
    using System;
    using Codesmith.SmithNgine.General;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Base abstract class for drawable game objects
    /// </summary>
    public abstract class DrawableGameObject : MovableGameObject
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public DrawableGameObject()
        {
        }
        #endregion

        #region New methods
        /// <summary>
        /// Draws the component. 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }
        #endregion
    }
}
