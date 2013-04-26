namespace Codesmith.SmithShooter
{
    #region Using Statements
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Storage;
    using Microsoft.Xna.Framework.GamerServices;

    using Codesmith.SmithNgine.GameState;
    using Codesmith.SmithShooter.MainMenu;
    using Codesmith.SmithShooter.Gameplay;
    #endregion

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SmithShooter : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameStateManager stateManager;
        GameState mainMenuState;
        GameState playingState;

        public SmithShooter()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";

            IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            // Create GameStateManager and add it to components
            stateManager = new GameStateManager(this, "Fonts/defaultfont");
            Components.Add(stateManager);

            // Create game states
            playingState = new PlayingState();
            mainMenuState = new MainMenuState(playingState);
            mainMenuState.StatusChanged += mainMenuState_StatusChanged;
            stateManager.AddGameState(mainMenuState);
            stateManager.AddGameState(playingState);

            this.IsMouseVisible = true;
            base.Initialize();
            stateManager.SwitchToState(mainMenuState);
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        void mainMenuState_StatusChanged(object sender, GameStatusEventArgs e)
        {
            if (sender == mainMenuState && e.newStatus == GameStateStatus.Paused)
            {
                Debug.WriteLine("Gaming state was paused");
            }
            String text = "State changed from " + e.oldStatus.ToString() + " to " + e.newStatus.ToString();
            Debug.WriteLine(sender, text);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.F10))
            {
                graphics.ToggleFullScreen();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Nothing to do here
            base.Draw(gameTime);
        }
    }
}
