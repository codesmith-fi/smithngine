// ***************************************************************************
// ** SmithNgine.GameState.GameCanvas                                       **
// **                                                                       **
// ** Copyright (C) 2013 by Erno Pakarinen. All Rights Reserved.            **
// ** Contact: erno(at)codesmith(dot)fi                                     **
// ***************************************************************************

#region Using statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Codesmith.SmithNgine.Input;
#endregion

namespace Codesmith.SmithNgine.GameState
{
    public abstract class GameCanvas : ObjectBase
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

        public Rectangle Bounds
        {
            get;
            set;
        }

        #endregion

        public GameCanvas()
        {
            this.isInitialized = false;
        }

        public virtual void LoadContent()
        {
            // Set bounds by default to the viewport bounds if it is not already set
            if (Bounds.IsEmpty)
            {
                Bounds = StateManager.GraphicsDevice.Viewport.Bounds;
            }
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

        public virtual void HandleInput(InputManager input)
        {
        }
    }
}
