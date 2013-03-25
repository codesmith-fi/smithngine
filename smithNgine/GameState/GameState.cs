// ***************************************************************************
// ** SmithNgine.GameState.GameState                                        **
// **                                                                       **
// ** Copyright (C) 2013 by Erno Pakarinen. All Rights Reserved.            **
// ** Contact: erno(at)codesmith(dot)fi                                     **
// ***************************************************************************

#region Using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Codesmith.SmithNgine.GameState
{
    public abstract class GameState : IEquatable<GameState>
    {
        #region Types
        public enum StateStatus
        {
            Hidden,
            Entering,
            Running,
            Paused,
            Exiting,
            Dead
        }
        #endregion

        #region Attributes/Fields
        private List<GameCanvas> canvasList = new List<GameCanvas>();
        private TimeSpan enterStateInterval = TimeSpan.Zero;
        private TimeSpan exitStateInterval = TimeSpan.Zero;
        private bool isInitialized = false;
        private Texture2D blankTexture;
        private float transitionValue = 1.0f;        
        protected Game game;
        protected StateStatus status = StateStatus.Hidden;
        #endregion

        #region Properties
        public float TransitionValue
        {
            get { return this.transitionValue; }
        }

        public TimeSpan EnterStateInterval
        {
            get { return this.enterStateInterval; }
            protected set { this.enterStateInterval = value; }
        }

        public TimeSpan ExitStateInterval
        {
            get { return this.exitStateInterval; }
            protected set { this.exitStateInterval = value; }
        }

        public bool IsActive
        {
            get
            {
                return (status == StateStatus.Running ||
                    status == StateStatus.Entering ||
                    status == StateStatus.Exiting);
            }
        }

        public StateStatus Status
        {
            get
            {
                return this.status;
            }
        }

        public GameStateManager StateManager
        {
            get;
            internal set;
        }

        public bool IsSlowLoadingState
        {
            get;
            protected set;
        }

        public String Name
        {
            get;
            private set;
        }
        #endregion

        #region Constructors
        public GameState(String name)
        {
            this.Name = name;
            this.isInitialized = false;
            this.IsSlowLoadingState = false;
            this.status = StateStatus.Hidden;
        }
        #endregion

        #region New methods - Virtual 
        public virtual void LoadContent()
        {
            this.blankTexture = StateManager.Game.Content.Load<Texture2D>("Images/blank");
            foreach (GameCanvas canvas in canvasList)
            {
                canvas.LoadContent();
            }
        }

        public virtual void UnloadContent()
        {
            foreach (GameCanvas canvas in canvasList)
            {
                canvas.UnloadContent();
            }
        }

        public virtual void Initialize()
        {
            foreach (GameCanvas canvas in canvasList)
            {
                canvas.Initialize();
            }
            this.isInitialized = true;
        }

        public virtual void Update(GameTime gameTime)
        {
            // If the state is exiting, update state transition
            if (this.status == StateStatus.Exiting)
            {
                if (!TransitionOut(gameTime, this.exitStateInterval))
                {
                    this.status = StateStatus.Hidden;
                }
            }
            else if (this.status == StateStatus.Entering)
            {
                if (!TransitionIn(gameTime, this.enterStateInterval))
                {
                    this.status = StateStatus.Running;
                }
            }

            // Do not update canvases if we are not active
            if (this.IsActive)
            {
                foreach (GameCanvas canvas in canvasList)
                {
                    canvas.Update(gameTime);
                }
            }
        }

        public virtual void Draw(GameTime gameTime)
        {
            if (this.status == StateStatus.Exiting)
            {
                // Fade out
            }
            // TODO: draw all canvases in correct order and such
            foreach (GameCanvas canvas in canvasList)
            {
                canvas.Draw(gameTime);
            }
        }

        public virtual void HandleInput()
        {
            // Allow each canvas to handle input, e.g. if the state 
            // has multiple player fields with separate controlling players
            foreach (GameCanvas canvas in canvasList)
            {
                canvas.HandleInput();
            }
        }
        #endregion

        #region New methods - non virtual      
        public void AddCanvas(GameCanvas canvas)
        {
            // Set owning state and state manager for new state
            canvas.StateManager = this.StateManager;
            canvas.State = this;
            this.canvasList.Add(canvas);

        }

        public virtual void EnterState()
        {
            this.status = StateStatus.Entering;
            this.transitionValue = 0.0f;
        }

        public virtual void ExitState()
        {
            this.status = StateStatus.Exiting;
            this.transitionValue = 1.0f;
        }

        public bool Equals(GameState other)
        {
            return this.Name.Equals(other.Name);
        }

        private bool TransitionIn(GameTime gameTime, TimeSpan transitionTime)
        {
            return UpdateStateTransition(gameTime, transitionTime, 1);
        }

        private bool TransitionOut(GameTime gameTime, TimeSpan transitionTime)
        {
            return UpdateStateTransition(gameTime, transitionTime, -1);
        }

        private bool UpdateStateTransition(GameTime gameTime, TimeSpan transitionTime, int direction)
        {
            // Ensure we have a valid direction of 1 or -1. 0 is assumed to be "down" (-1)
            direction = ( direction > 0 ) ? 1 : -1;
            double elapsedMs = gameTime.ElapsedGameTime.TotalMilliseconds;
            double transitionMs = transitionTime.TotalMilliseconds;

            float delta = (float)(elapsedMs / transitionMs);
            this.transitionValue += delta * direction;
            // Ensure that the transition value is kept in: 0.0f <= transitionValue <= 1.0f
            if (((direction > 0) && (this.transitionValue > 1.0f)) ||
                ((direction < 0) && (this.transitionValue < 0.0f)))
            {
                // We are done transitioning in or out, clamp the value and end transition
                this.transitionValue = MathHelper.Clamp(this.transitionValue, 0.0f, 1.0f);
                return false;
            }

            return true;
        }

        protected void DimWithAlpha(float alpha)
        {
            Viewport viewport = StateManager.GraphicsDevice.Viewport;
            StateManager.SpriteBatch.Begin();
            StateManager.SpriteBatch.Draw(this.blankTexture, new Rectangle(0, 0, viewport.Width, viewport.Height), Color.Black * alpha);
            StateManager.SpriteBatch.End();
        }

        #endregion
    }
}
