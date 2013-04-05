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
using Microsoft.Xna.Framework.Content;
using Codesmith.SmithNgine.Input;
using Codesmith.SmithNgine.General;
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
        private FrameworkContentService frameworkContentService;
        //        private Effect postEffect;
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

        // Framework content
        public ContentManager Content
        {
            get;
            internal set;

        }

        public ContentManager FrameworkContent
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

        public bool ExitRequested
        {
            set;
            get;
        }
        #endregion

        #region Constructors
        public GameStateManager(Game game) 
            : base(game)
        {
            CurrentState = null;
            isInitialized = false;
            ExitRequested = false;
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
            FrameworkContent = new ContentManager(Game.Services, "FrameworkContent");
            Content = Game.Content;

            // Register service for content
            frameworkContentService = new FrameworkContentService(FrameworkContent);
            Game.Services.AddService(typeof(IContentManagerService), frameworkContentService);

            this.spriteBatch = new SpriteBatch(GraphicsDevice);

            this.blankTexture = FrameworkContent.Load<Texture2D>("Images/blank");

            // Unless set from outside, load the default font which is shared for all states
            if (this.Font == null)
            {
                this.Font = FrameworkContent.Load<SpriteFont>("Fonts/defaultfont");
            }

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
            // Update exiting state if it is exiting
            List<GameState> statesToDraw = new List<GameState>();
            if (ExitingState != null)
            {
                if (ExitingState.Status == GameStateStatus.Exiting)
                {
                    DrawGameState(ExitingState, gameTime);
                    statesToDraw.Add(ExitingState);
                }
                else
                {
                    ExitingState = null;
                }
            }

            if (CurrentState != null)
            {
                DrawGameState(CurrentState, gameTime);
                statesToDraw.Add(CurrentState);
                if (PauseState != null && PauseState.IsActive)
                {
                    DrawGameState(PauseState, gameTime);
                    statesToDraw.Add(PauseState);
                }
            }

            GraphicsDevice.Clear(Color.Transparent);
            foreach (GameState state in statesToDraw)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                    SamplerState.AnisotropicClamp, DepthStencilState.Default,
                    RasterizerState.CullNone, state.PostProcessingEffect);
                Color color = new Color( 1.0f, 1.0f, 1.0f, state.TransitionValue );
                Rectangle t = state.RenderTarget.Bounds;
//                t.Inflate(-100, -100);
                spriteBatch.Draw((Texture2D)state.RenderTarget, t, Color.White * state.TransitionValue);
                spriteBatch.End();
            }
        }

        private void DrawGameState(GameState state, GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(state.RenderTarget);
            GraphicsDevice.Clear(Color.Transparent);
            state.Draw(gameTime);
            GraphicsDevice.SetRenderTarget(null);
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
        #endregion
    }
}
