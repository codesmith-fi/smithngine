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

        public GameState ExitingState
        {
            get;
            private set;
        }

        public GameState CurrentState
        {
            get;
            private set;
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
            }
        }

        public override void Draw(GameTime gameTime)
        {
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
            }
        }
        #endregion

        #region New public methods
        public void AddGameState(GameState state)
        {            
            if (!gameStates.Contains(state))
            {
                state.StateManager = this;
                gameStates.Add(state);
                if (CurrentState == null)
                {
                    CurrentState = state;
                    CurrentState.EnterState();
                }
            }
        }

        public void SwitchToState(GameState nextState)
        {
            if (nextState != CurrentState && gameStates.Contains(nextState))
            {
                // switch to next state
                CurrentState.ExitState();
                nextState.EnterState();
                if (nextState.IsSlowLoadingState)
                {
                    // TODO: Should bump up a loading state of some kind
                }
                ExitingState = CurrentState;
                CurrentState = nextState;
            }
        }

        protected void DimWithAlpha(float alpha)
        {
            Viewport viewport = GraphicsDevice.Viewport;
            SpriteBatch.Begin();
            SpriteBatch.Draw(this.blankTexture, new Rectangle(0, 0, viewport.Width, viewport.Height), Color.Black * alpha);
            SpriteBatch.End();
        }
        #endregion

        #region Private new methods
        #endregion
    }
}
