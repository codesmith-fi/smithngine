using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Codesmith.SmithNgine.GameState;

namespace Codesmith.SmithTest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SmithGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameStateManager stateManager;
        GamingState state2;
        MainMenuState state1;
        PauseState pauseState;

        public SmithGame()
        {
            graphics = new GraphicsDeviceManager(this);
            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 768;
            Content.RootDirectory = "Content";
        }

        public void TestStateStatusChanged(object sender, GameStatusEventArgs args)
        {
            if (sender == state2 && args.newStatus == GameStateStatus.Paused)
            {
                Debug.WriteLine("Gaming state was paused");
            }
            String text = "State changed from " + args.oldStatus.ToString() + " to " + args.newStatus.ToString();
            Debug.WriteLine(sender, text);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            stateManager = new GameStateManager(this);
            state1 = new MainMenuState("Main Menu");
            state2 = new GamingState("Game");
            pauseState = new PauseState("Paused");
            stateManager.AddGameState(state1);
            stateManager.AddGameState(state2);
            stateManager.AddGameState(pauseState, true);
            state1.StatusChanged += this.TestStateStatusChanged;
            state2.StatusChanged += this.TestStateStatusChanged;
            pauseState.StatusChanged += this.TestStateStatusChanged;
            Components.Add(stateManager);
            this.IsMouseVisible = true;
            base.Initialize();

            stateManager.SwitchToState(state1);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            HandleKeyboard(Keyboard.GetState(PlayerIndex.One));

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void HandleKeyboard(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Right))
            {
                stateManager.SwitchToState(state2);   
            }
            else if (keyState.IsKeyDown(Keys.Left))
            {
                stateManager.SwitchToState(state1);
            }
        }

    }
}
