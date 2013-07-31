// ***************************************************************************
// ** SmithTest - demo app for smithNgine framework                         **
// **                                                                       **
// ** Copyright (C) 2013 by Erno Pakarinen. All Rights Reserved.            **
// ** Contact: erno@codesmith.fi                                            **
// ***************************************************************************
using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Codesmith.SmithNgine.GameState;
using Codesmith.SmithNgine.General;

namespace Codesmith.SmithTest
{
    public class SmithGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        GameStateManager stateManager;
        GamingState state2;
        MainMenuState state1;
        PauseState pauseState;
        PhysicsState physicsState;
        FpsCounter fpsCounter;

        public SmithGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;            
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
//            graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;
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

        protected override void Initialize()
        {
            stateManager = new GameStateManager(this, "Fonts/defaultfont");
            state2 = new GamingState("Game");
            physicsState = new PhysicsState("Physics test");
            state1 = new MainMenuState("Main Menu", state2, physicsState);
            pauseState = new PauseState("Paused");
            stateManager.AddGameState(state1);
            stateManager.AddGameState(state2);
            stateManager.AddGameState(physicsState);
            stateManager.AddGameState(pauseState, true);
            state1.StatusChanged += this.TestStateStatusChanged;
            state2.StatusChanged += this.TestStateStatusChanged;
            pauseState.StatusChanged += this.TestStateStatusChanged;
            physicsState.StatusChanged += this.TestStateStatusChanged;
            Components.Add(stateManager);
            this.IsMouseVisible = true;
            base.Initialize();

            // Activate our first state, which surprisingly is the MainMenuState
            stateManager.SwitchToState(state1);
        }

        protected override void LoadContent()
        {
            fpsCounter = new FpsCounter();
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            HandleKeyboard(Keyboard.GetState(PlayerIndex.One));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            fpsCounter.Update(gameTime);
            Window.Title = "FPS: " + fpsCounter.Fps + " AvgTime: " + fpsCounter.AvgTimePerFrame;
            base.Draw(gameTime);
        }

        private void HandleKeyboard(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.LeftAlt) && keyState.IsKeyDown(Keys.Enter))
            {
                graphics.ToggleFullScreen();
            }

            if (keyState.IsKeyDown(Keys.Right))
            {
                if (stateManager.CurrentState != state1)
                {
                    stateManager.SwitchToState(state2);
                }
            }
            else if (keyState.IsKeyDown(Keys.Left))
            {
                if (stateManager.CurrentState != state1)
                {
                    stateManager.SwitchToState(state1);
                }
            }
        }

    }
}
