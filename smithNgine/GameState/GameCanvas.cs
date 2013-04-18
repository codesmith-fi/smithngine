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
using Codesmith.SmithNgine.General;
#endregion

namespace Codesmith.SmithNgine.GameState
{
    public abstract class GameCanvas : GameObjectBase
    {
        #region Fields/Attributes
        private List<IActivatableObject> children = new List<IActivatableObject>();
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

        #region Constructor
        public GameCanvas()
        {
            this.isInitialized = false;
        }
        #endregion

        #region Methods from ObjectBase
        public override void ActivateObject()
        {
            base.ActivateObject();
            foreach (IActivatableObject obj in children)
            {
                obj.ActivateObject();
            }
        }

        public override void DeactivateObject()
        {
            base.DeactivateObject();
            foreach (IActivatableObject obj in children)
            {
                obj.DeactivateObject();
            }
        }
        #endregion

        #region New methods
        public virtual void AddComponent(IActivatableObject obj)
        {
            this.children.Add(obj);
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

        public virtual void HandleInput(InputManager input)
        {
        }

        public override void Dismiss()
        {
            foreach (IActivatableObject obj in children)
            {
                obj.Dismiss();
            }
            base.Dismiss();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (IActivatableObject obj in children)
            {
                obj.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime)
        {
        }

        #endregion
    }
}
