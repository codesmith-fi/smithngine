/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
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
    /// <summary>
    /// GameStateManager is a class responsible of all game states
    /// States are activated and deactivated through this.
    /// 
    /// GameStateManager itself is a DrawableGameComponent so same
    /// rules apply to it. Especially referring to methods like
    /// Initialize, LoadContent, UnloadContent, Draw, Update etc.
    /// 
    /// StateManager calls Initialize on all States when StateManager
    /// itself is initialized
    /// LoadContent is called on a state when State is brought active (using SwitchToState)
    /// UnloadContent is called when state becomes hidden. As is Dismiss.
    /// Draw and Update are called in game loop during each frame.
    /// 
    /// 
    /// </summary>
    public class GameStateManager : DrawableGameComponent
    {
        #region Attributes
        private InputManager input = new InputManager();
        private List<GameState> gameStates = new List<GameState>();
        private SpriteBatch spriteBatch;
        private string fontAsset = "";
        private bool isInitialized;
        #endregion

        #region Properties
        /// <summary>
        /// Get the InputManager for querying mouse, keyboard and gamepad 
        /// related events/properties.
        /// </summary>
        public InputManager Input
        {
            get 
            { 
                return this.input; 
            }
        }

        /// <summary>
        /// Get the content manager responsible for content handling
        /// Use this to load content in LoadContent.
        /// </summary>
        public ContentManager Content
        {
            get;
            internal set;

        }

        /// <summary>
        /// Get a SpriteBatch. This can be used to draw stuff
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get
            {
                return this.spriteBatch;
            }
        }

        /// <summary>
        /// A default font, can be used to draw text.
        /// </summary>
        public SpriteFont Font
        {
            get;
            set;
        }

        /// <summary>
        /// State which is currently exiting (e.g. when SwitchToState() was called)
        /// This will be drawn and updated before CurrentState and the result
        /// will be mixed (means fade in, fade out)
        /// </summary>
        public GameState ExitingState
        {
            get;
            private set;
        }

        /// <summary>
        /// Contains the previous active state
        /// </summary>
        public GameState PreviousState
        {
            get;
            set;
        }

        /// <summary>
        /// The current state which gets input and is drawn and updated last
        /// in the game loop
        /// </summary>
        public GameState CurrentState
        {
            get;
            private set;
        }

        /// <summary>
        /// Pause state is activated when a running state enters pause status
        /// If left unset, no state is active when current state is paused
        /// </summary>
        public GameState PauseState
        {
            get;
            set;
        }

        /// <summary>
        /// Flag indicating that Exit was requested by some state.
        /// Concrete states can use this to flag that Game must exit when current state exits.
        /// To accomplis this, in concrete state, set this to true and then call ExitState()
        /// </summary>
        public bool ExitRequested
        {
            set;
            get;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Construct a StateManager with the given font asset
        /// </summary>
        /// <param name="game">Game instance</param>
        /// <param name="fontAssetName">Name of the font asset to be used as a default font</param>
        public GameStateManager(Game game, string fontAssetName) 
            : base(game)
        {
            fontAsset = fontAssetName;
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
            Content = Game.Content;
            this.spriteBatch = new SpriteBatch(GraphicsDevice);

            // Unless set from outside, load the default font which is shared for all states
            if (this.Font == null)
            {
                this.Font = Content.Load<SpriteFont>(this.fontAsset);
            }
            // TODO: This might be better do somehow else
            if (PauseState != null)
            {
                PauseState.LoadContent();
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

        /// <summary>
        /// StateManager draws each active state on a RenderTarget2D (texture2d).
        /// This is done to enable smooth transition between states.
        /// The entering state and the exiting state are drawn on top of 
        /// eachother. This means that when state is entering, two states might
        /// be active at the same time.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            // A list of gamestates to draw
            List<GameState> statesToDraw = new List<GameState>();

            // Draw the exiting state when it is exiting
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

            // Draw the current state
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
                spriteBatch.Draw((Texture2D)state.RenderTarget, t, Color.White * state.TransitionValue);
                spriteBatch.End();
            }
        }

        /// <summary>
        /// Draw a GameState to it's RenderTarget
        /// The rendertarget will be used later to actually make the state visible.
        /// </summary>
        /// <param name="state">State to draw</param>
        /// <param name="gameTime">The GameTime</param>
        private void DrawGameState(GameState state, GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(state.RenderTarget);
            GraphicsDevice.Clear(Color.Transparent);
            state.Draw(gameTime);
            GraphicsDevice.SetRenderTarget(null);
        }
        #endregion

        #region New public methods
        /// <summary>
        /// Add a state to be managed
        /// </summary>
        /// <param name="state">State to be added</param>
        /// <param name="isPauseState">If true, the added state is considered
        /// as a Pause State, meaning when state enters Pause() the pause state will be activated</param>
        public void AddGameState(GameState state, bool isPauseState=false)
        {            
            if (!gameStates.Contains(state))
            {
                state.StateManager = this;
                state.StatusChanged += StateStatusChanged;
                gameStates.Add(state);
                if (isPauseState)
                {
                    PauseState = state;
                }
            }
        }

        /// <summary>
        /// Switch to another state. 
        /// State to be activated must be added first using AddGameState()
        /// When this is called
        ///     - current state goes first from Running status to Exiting and finally to Hidden.
        ///     - nextState goes from Hidden status to Entering and finally Running.
        /// How long this takes, is controlled by each states' EnterStateInterval and ExitStateInterval
        /// </summary>
        /// <param name="nextState">The state to be activated</param>
        public void SwitchToState(GameState nextState)
        {
            if (PauseState != null && PauseState.IsActive)
            {
                PauseState.ExitState();
            }

            if (nextState != CurrentState && gameStates.Contains(nextState))
            {
                nextState.LoadContent();
                PreviousState = CurrentState;
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

        /// <summary>
        /// Pauses the current state. PauseState will become active.
        /// </summary>
        public void PauseCurrentState()
        {
            if (PauseState != null)
            {
                PauseState.EnterState();
            }
            CurrentState.Pause();
        }

        /// <summary>
        /// Unpause the current state, PauseState will become inactive
        /// </summary>
        public void UnPauseCurrentState()
        {
            if (PauseState != null)
            {
                PauseState.ExitState();
            }
            CurrentState.UnPause();
        }
        #endregion

        #region Private new methods
        /// <summary>
        /// Observes state status changes. Will call Dismiss on each state 
        /// which becomes hidden. Also calls UnloadContent().
        /// TODO: Dismiss can be perhaps refactored totally away (use UnloadContent).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void StateStatusChanged(object sender, GameStatusEventArgs args)
        {
            if (args.newStatus == GameStateStatus.Hidden)
            {
                GameState state = (GameState)sender;
                state.Dismiss();
                if( state != PauseState ) state.UnloadContent();
            }
        }
        #endregion
    }
}
