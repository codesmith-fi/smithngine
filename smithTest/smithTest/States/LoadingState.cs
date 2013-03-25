﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Codesmith.SmithNgine.GameState;

namespace Codesmith.SmithTest
{
    class LoadingState : GameState
    {
        private Texture2D image;

        public LoadingState(String name)
            : base(name)
        {
            this.EnterStateInterval = TimeSpan.FromSeconds(0.5f);
            this.ExitStateInterval = TimeSpan.FromSeconds(0.5f);
        }

        public override void LoadContent()
        {
            image = StateManager.Game.Content.Load<Texture2D>("Images/desert");
            base.LoadContent();
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
    }
}
