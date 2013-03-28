// ***************************************************************************
// ** SmithNgine.GameState.GameState                                        **
// **                                                                       **
// ** Copyright (C) 2013 by Erno Pakarinen. All Rights Reserved.            **
// ** Contact: erno(at)codesmith(dot)fi                                     **
// ***************************************************************************

#region Using statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Codesmith.SmithNgine.Input;
#endregion

namespace Codesmith.SmithNgine.GameState
{
    #region Types
    public enum GameStateStatus
    {
        Hidden,
        Entering,
        Running,
        EnteringPause,
        Paused,
        ExitingPause,
        Exiting,
        Dead
    }

    #endregion

    public abstract class GameState : ObjectBase, IEquatable<GameState>, ITransitionSource
    {
        #region Attributes/Fields
        private List<GameCanvas> canvasList = new List<GameCanvas>();
        private List<IActivatableObject> children = new List<IActivatableObject>();
        private TimeSpan enterStateInterval = TimeSpan.Zero;
        private TimeSpan exitStateInterval = TimeSpan.Zero;
        private TimeSpan pauseStateInterval = TimeSpan.Zero;
        private bool isInitialized = false;
        private float transitionValue = 1.0f;
        private float transitionBottomLimit = 0.0f;
        private float transitionUpperLimit = 1.0f;
        private GameStateStatus status;
        protected Game game;
        #endregion

        #region Properties
        public Rectangle Bounds
        {
            get;
            set;
        }

        public float TransitionValue
        {
            get { return this.transitionValue; }
        }

        // Time interval for how long the pause transition takes
        public TimeSpan PauseStateInterval
        {
            get { return this.pauseStateInterval; }
            protected set { this.pauseStateInterval = value; }
        }

        // Time interval for how long state takes to enter
        public TimeSpan EnterStateInterval
        {
            get { return this.enterStateInterval; }
            protected set { this.enterStateInterval = value; }
        }

        // Time interval for how long the state takes to exit
        public TimeSpan ExitStateInterval
        {
            get { return this.exitStateInterval; }
            protected set { this.exitStateInterval = value; }
        }

        public bool IsActive
        {
            get
            {
                return (Status == GameStateStatus.Running ||
                    Status == GameStateStatus.Entering ||
                    Status == GameStateStatus.Exiting);
            }
        }

        public bool IsPaused
        {
            get
            {
                return (Status == GameStateStatus.Paused ||
                    Status == GameStateStatus.EnteringPause ||
                    Status == GameStateStatus.ExitingPause );
            }
        }

        public GameStateStatus Status
        {
            get { return status; }
            private set
            {
                GameStateStatus oldStatus = status;
                if (oldStatus != value)
                {
                    status = value;
                    OnStatusChanged(new GameStatusEventArgs(oldStatus, value));
                }
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

        public Effect PostProcessingEffect
        {
            get;
            set;
        }

        public RenderTarget2D RenderTarget
        {
            get;
            internal set;
        }

        #endregion

        #region Events
        public event EventHandler<GameStatusEventArgs> StatusChanged;
        #endregion

        #region Constructors
        public GameState(String name)
        {
            this.Name = name;
            this.isInitialized = false;
            this.IsSlowLoadingState = false;
            this.Status = GameStateStatus.Hidden;
            this.transitionUpperLimit = 1.0f;
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
            // If the state is exiting, update state transition
            if (Status == GameStateStatus.Exiting)
            {
                if (!TransitionOut(gameTime, this.exitStateInterval))
                {
                    Status = GameStateStatus.Hidden;
                }
            }
            else if (Status == GameStateStatus.Entering)
            {
                if (!TransitionIn(gameTime, this.enterStateInterval))
                {
                    Status = GameStateStatus.Running;
                }
            }
            else if (Status == GameStateStatus.EnteringPause)
            {
                if (!TransitionOut(gameTime, this.pauseStateInterval))
                {
                    Status = GameStateStatus.Paused;
                }
            }
            else if (Status == GameStateStatus.ExitingPause)
            {
                if (!TransitionIn(gameTime, this.pauseStateInterval))
                {
                    Status = GameStateStatus.Running;
                }
            }

            // Do not update canvases if we are not active
            if (this.IsActive)
            {
                foreach (IActivatableObject child in children)
                {
                    child.Update(gameTime);
                }
            }
        }
        #endregion

        #region New methods - Virtual
        public virtual void LoadContent()
        {
            PresentationParameters pp = StateManager.GraphicsDevice.PresentationParameters;
            this.RenderTarget = new RenderTarget2D(
                StateManager.GraphicsDevice,
                pp.BackBufferWidth, pp.BackBufferHeight,
                false, pp.BackBufferFormat, DepthFormat.Depth24, 0, RenderTargetUsage.PreserveContents);

            // Set bounds by default to the viewport bounds if it is not already set
            if (Bounds.IsEmpty)
            {
                Bounds = StateManager.GraphicsDevice.Viewport.Bounds;
            }

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

        public virtual void Draw(GameTime gameTime)
        {
            foreach (GameCanvas canvas in canvasList)
            {
                canvas.Draw(gameTime);
            }
        }

        public virtual void HandleInput(InputManager input)
        {
            // Allow each canvas to handle input, e.g. if the state 
            // has multiple player fields with separate controlling players
            if (IsActive)
            {
                foreach (GameCanvas canvas in canvasList)
                {
                    canvas.HandleInput(input);
                }
            }
        }

        public virtual void EnterState()
        {
            this.transitionValue = 0.0f;
            this.Status = GameStateStatus.Entering;
            ActivateObject();
        }

        public virtual void ExitState()
        {
            this.Status = GameStateStatus.Exiting;
            this.transitionValue = 1.0f;
            this.transitionBottomLimit = 0.0f;
            DeactivateObject();
        }

        // Causes this state to transition into paused state, by default transition 50% off
        public virtual void Pause( float limit = 0.5f )
        {
            if (IsActive)
            {
                this.Status = GameStateStatus.EnteringPause;
                this.transitionBottomLimit = limit;
                DeactivateObject();
            }
        }

        // Causes this state to transition into running state from paused state
        public virtual void UnPause()
        {
            if (IsPaused)
            {
                this.Status = GameStateStatus.ExitingPause;
                ActivateObject();
            }
        }
        #endregion

        #region New public methods 
        public void AddCanvas(GameCanvas canvas)
        {
            // Set owning state and state manager for new state
            canvas.StateManager = this.StateManager;
            canvas.State = this;
            this.canvasList.Add(canvas);
            AddChild(canvas);
        }

        public bool Equals(GameState other)
        {
            return this.Name.Equals(other.Name);
        }
        #endregion

        #region Private and protected methods
        private void OnStatusChanged(GameStatusEventArgs args)
        {
            if (StatusChanged != null)
            {
                StatusChanged(this, args);
            }
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
            if (((direction > 0) && (this.transitionValue > this.transitionUpperLimit)) ||
                ((direction < 0) && (this.transitionValue < this.transitionBottomLimit)))
            {
                // We are done transitioning in or out, clamp the value and end transition
                this.transitionValue = MathHelper.Clamp(
                    this.transitionValue, this.transitionBottomLimit, this.transitionUpperLimit);
                return false;
            }

            return true;
        }

        protected void AddChild(IActivatableObject obj)
        {
            children.Add(obj);
        }

        #endregion
    }
}
