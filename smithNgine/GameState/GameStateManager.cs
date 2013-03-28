// ***************************************************************************
// ** SmithNgine.GameState.GameStateManager                                 **
// **                                                                       **
// ** Copyright (C) 2013 by Erno Pakarinen. All Rights Reserved.            **
// ** Contact: erno(at)codesmith(dot)fi                                     **
// ***************************************************************************

#region Using statements
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Codesmith.SmithNgine.Input;
using Microsoft.Xna.Framework.Content;
#endregion

namespace Codesmith.SmithNgine.GameState
{
    public class GameStateManager : DrawableGameComponent
    {
        #region Attributes
        private InputManager input = new InputManager();
        private List<GameState> gameStates = new List<GameState>();
        private SpriteBatch spriteBatch;
        private Texture2D blankTexture;
        private Effect postEffect;
        private RenderTarget2D renderTarget;
        private bool isInitialized;
        #endregion

        #region Properties
        public InputManager Input
        {
            get 
            { 
                return this.input; 
            }
        }

        public ContentManager Content
        {
            get;
            internal set;

        }

        public SpriteBatch SpriteBatch
        {
            get
            {
                return this.spriteBatch;
            }
        }

        public SpriteFont Font
        {
            get;
            set;
        }

        // State which is currently exiting (ExitState() was called)
        // This will be drawn and updated before CurrentState
        public GameState ExitingState
        {
            get;
            private set;
        }

        // The current state which gets input and is drawn and updated last
        public GameState CurrentState
        {
            get;
            private set;
        }

        // Pause state is activated when a running state enters pause status
        // If left unset, no state is active when current state is paused
        public GameState PauseState
        {
            get;
            set;
        }

        public Effect PostProcessingEffect
        {
            get
            {
                return this.postEffect;
            }
            set
            {
                this.postEffect = value;
                MakeRenderTarget();
            }
        }
        #endregion

        #region Constructors
        public GameStateManager(Game game) 
            : base(game)
        {
            CurrentState = null;
            isInitialized = false;
        }
        #endregion

        #region From DrawableGameComponent
        public override void Initialize()
        {
            // Allow each state to initialize if needed
            foreach (GameState state in gameStates)
            {
                state.Initialize();
            }
            base.Initialize();
            this.isInitialized = true;
        }

        protected override void LoadContent()
        {
            if (Content == null)
            {
                Content = Game.Content;
            }

            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            // Unless set from outside, load the default font which is shared for all states
            if (this.Font == null)
            {
                this.Font = Game.Content.Load<SpriteFont>("defaultfont");
            }

            this.blankTexture = Game.Content.Load<Texture2D>("Images/blank");

            // Load each games state as well
            foreach (GameState state in gameStates)
            {
                state.LoadContent();
            }
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            foreach (GameState state in gameStates)
            {
                state.UnloadContent();
            }
        }

        public override void Update(GameTime gameTime)
        {
            // Update input to allow states get player control etc.
            Input.Update();

            // Update exiting state if it is exiting
            if (ExitingState != null)
            {
                if (ExitingState.Status == GameStateStatus.Exiting)
                {
                    ExitingState.Update(gameTime);
                }
                else
                {
                    ExitingState = null;
                }
            }

            // Update the current state
            if (CurrentState != null)
            {
                CurrentState.Update(gameTime);
                // Handle input on current state only when it's running 
                if (CurrentState.IsActive || CurrentState.IsPaused)
                {
                    CurrentState.HandleInput(this.Input);
                }

                if (PauseState != null && PauseState.IsActive)
                {
                    PauseState.Update(gameTime);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            // If we have a post processing effect, render everything to texture first
            if (PostProcessingEffect != null && renderTarget != null)
            {
                GraphicsDevice.SetRenderTarget(renderTarget);
            }

            GraphicsDevice.Clear(Color.Black);

            // Update exiting state if it is exiting
            if (ExitingState != null)
            {
                if (ExitingState.Status == GameStateStatus.Exiting)
                {
                    ExitingState.Draw(gameTime);
                }
                else
                {
                    ExitingState = null;
                }
            }

            if (CurrentState != null)
            {
                CurrentState.Draw(gameTime);
                if (PauseState != null && PauseState.IsActive)
                {
                    PauseState.Draw(gameTime);
                }
            }

            if (PostProcessingEffect != null && renderTarget != null)
            {
                // drop render target
                GraphicsDevice.SetRenderTarget(null);

                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                    SamplerState.LinearClamp, DepthStencilState.Default,
                    RasterizerState.CullNone, postEffect);
                spriteBatch.Draw(renderTarget, renderTarget.Bounds, Color.White);
                spriteBatch.End();
            }
        }
        #endregion

        #region New public methods
        public void AddGameState(GameState state, bool isPauseState=false)
        {            
            if (!gameStates.Contains(state))
            {
                state.StateManager = this;
                state.StatusChanged += StateStatusChanged;

                gameStates.Add(state);
                PauseState = state;
            }
        }

        public void SwitchToState(GameState nextState)
        {
            if (PauseState != null && PauseState.IsActive)
            {
                PauseState.ExitState();
            }

            if (nextState != CurrentState && gameStates.Contains(nextState))
            {
                // switch to next state
                if (CurrentState != null)
                {
                    CurrentState.ExitState();
                }
                nextState.EnterState();
                if (nextState.IsSlowLoadingState)
                {
                    // TODO: Should bump up a loading state of some kind
                }
                ExitingState = CurrentState;
                CurrentState = nextState;
            }
        }

        public void PauseCurrentState()
        {
            if (PauseState != null)
            {
                PauseState.EnterState();
            }
            CurrentState.Pause();
        }

        public void UnPauseCurrentState()
        {
            if (PauseState != null)
            {
                PauseState.ExitState();
            }
            CurrentState.UnPause();
        }

        public void DimWithAlpha(float alpha, Rectangle area)
        {
            Viewport viewport = GraphicsDevice.Viewport;
            SpriteBatch.Begin();
            SpriteBatch.Draw(this.blankTexture, area, Color.Black * alpha);
            SpriteBatch.End();
        }
        #endregion

        #region Private new methods
        private void StateStatusChanged(object sender, GameStatusEventArgs args)
        {
            if (args.newStatus == GameStateStatus.Hidden)
            {
                GameState state = (GameState)sender;
                state.Dismiss();
            }
        }

        private void MakeRenderTarget()
        {
            if (PostProcessingEffect != null)
            {
                PresentationParameters pp = GraphicsDevice.PresentationParameters;
                this.renderTarget = new RenderTarget2D(
                    GraphicsDevice,
                    pp.BackBufferWidth, pp.BackBufferHeight,
                    false, pp.BackBufferFormat, DepthFormat.Depth24);
            }
            else
            {
                this.renderTarget = null;
            }
        }      
        #endregion
    }
}
