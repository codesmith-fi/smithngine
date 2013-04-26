/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */

namespace Codesmith.SmithNgine.GameState
{
    #region Using statements
    using System.Collections.Generic;
    using System.Diagnostics;
    using Codesmith.SmithNgine.General;
    using Codesmith.SmithNgine.Input;
    using Microsoft.Xna.Framework;
    #endregion

    /// <summary>
    /// Implements a GameCanvas class
    /// 
    /// GameCanvas can be used inside a GameState class as a collection of drawable
    /// components. 
    /// </summary>
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
        /// <summary>
        /// Add a component to this Canvas
        /// All added components have their Update() called by the framework
        /// during each frame. The call order is the addition order
        /// </summary>
        /// <remarks>
        /// Component can be added only once.
        /// </remarks>
        /// <param name="obj">Component to add</param>
        public virtual void AddComponent(IActivatableObject obj)
        {
            Debug.Assert(!children.Contains(obj), "Trying to add same component twice!");
            this.children.Add(obj);
        }

        /// <summary>
        /// Remove a component from this canvas
        /// </summary>
        /// <param name="obj">Component to remove</param>
        public virtual void RemoveComponent(IActivatableObject obj)
        {
            children.Remove(obj);
        }

        /// <summary>
        /// Loads content for this canvas. This will be called by the 
        /// GameState for all canvases owned by it. 
        /// </summary>
        public virtual void LoadContent()
        {
            // Set bounds by default to the viewport bounds if it is not already set
            if (Bounds.IsEmpty)
            {
                Bounds = StateManager.GraphicsDevice.Viewport.Bounds;
            }
        }

        /// <summary>
        /// Unload content for canvas (free resources)
        /// This will be called when a State becomes hidden.
        /// </summary>
        public virtual void UnloadContent()
        {
        }

        /// <summary>
        /// Initializes the Canvas, load/initialize any non-graphic content
        /// Called by GameState
        /// </summary>
        public virtual void Initialize()
        {
            this.isInitialized = true;
        }

        /// <summary>
        /// Handle input on this canvas, called by gamestate for each canvas
        /// </summary>
        /// <param name="input">The input manager</param>
        public virtual void HandleInput(InputManager input)
        {
        }

        /// <summary>
        /// Draw the canvas
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public virtual void Draw(GameTime gameTime)
        {
        }
        #endregion

        #region Methods from base classes
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

        #endregion
    }
}
