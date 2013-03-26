﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Codesmith.SmithNgine.GameState;
using Codesmith.SmithNgine.Input;

namespace Codesmith.SmithTest
{
    class MainMenuState : GameState
    {
        private Texture2D image;
        private GameCanvas menuCanvas;

        public MainMenuState(String name)
            : base(name)
        {
            this.EnterStateInterval = TimeSpan.FromSeconds(2.0f);
            this.ExitStateInterval = TimeSpan.FromSeconds(2.0f);
        }

        public override void Initialize()
        {
            menuCanvas = new MenuCanvas();
            menuCanvas.TransitionSource = this;
            AddCanvas(menuCanvas);
            base.Initialize();
        }
        public override void LoadContent()
        {
            base.LoadContent();
            StateManager.Input.MousePositionChanged += Input_MousePositionChanged;
            image = StateManager.Game.Content.Load<Texture2D>("Images/desert");
            menuCanvas.Bounds = new Rectangle(20, 20, Bounds.Width - 40, 200);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = StateManager.SpriteBatch;
            Viewport viewport = StateManager.GraphicsDevice.Viewport;

            spriteBatch.Begin();
            spriteBatch.Draw(image, new Rectangle(0, 0, viewport.Width, viewport.Height), Color.White * this.TransitionValue);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void Input_MousePositionChanged(object sender, MousePositionEventArgs args)
        {
        }
    }
}
