// ***************************************************************************
// ** GameStateManagement.GameStateCanvas                                   **
// **                                                                       **
// ** Copyright (C) 2013 by Erno Pakarinen. All Rights Reserved.            **
// ** Contact: erno(at)codesmith(dot)fi                                     **
// ***************************************************************************

#region Using statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
#endregion

namespace Codesmith.SmithNgine.GameState
{
    public abstract class GameCanvas
    {
        #region Fields/Attributes
        private bool isInitialized;
        #endregion

        #region Properties
        public GameStateManager StateManager
        {
            get;
            internal set;
        }

        public GameState State
        {
            get;
            internal set;
        }

        #endregion

        public GameCanvas()
        {
            this.isInitialized = false;
        }

        public virtual void LoadContent()
        {
        }

        public virtual void UnloadContent()
        {
        }

        public virtual void Initialize()
        {
            this.isInitialized = true;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(GameTime gameTime)
        {
        }

        public virtual void HandleInput()
        {
        }
    }
}
