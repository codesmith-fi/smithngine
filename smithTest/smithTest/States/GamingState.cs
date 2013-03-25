using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Codesmith.SmithNgine.GameState;

namespace Codesmith.SmithTest
{
    class GamingState : GameState
    {
        private Texture2D image;
        private GamingStatusCanvas canvas1;

        public GamingState(String name)
            : base(name)
        {
            this.EnterStateInterval = TimeSpan.FromSeconds(0.5f);
            this.ExitStateInterval = TimeSpan.FromSeconds(0.5f);
        }

        public override void LoadContent()
        {
            this.AddCanvas(new GamingStatusCanvas());
            image = StateManager.Game.Content.Load<Texture2D>("Images/snowmountain");

            base.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();
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
