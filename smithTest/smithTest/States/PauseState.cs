using System;
using System.Text;

using Microsoft.Xna.Framework;
using Codesmith.SmithNgine.GameState;
using Microsoft.Xna.Framework.Graphics;

namespace Codesmith.SmithTest
{
    public class PauseState : GameState
    {
        private Texture2D image;
        private Vector2 imagePos;
        public PauseState(String name) 
            : base(name)
        {
            this.EnterStateInterval = TimeSpan.FromSeconds(0.3f);
            this.ExitStateInterval = TimeSpan.FromSeconds(0.3f);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            image = StateManager.Game.Content.Load<Texture2D>("Images/paused");
            imagePos = new Vector2(Bounds.Width / 2 - image.Width / 2, Bounds.Height / 2 - image.Height / 2);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = StateManager.SpriteBatch;
            Viewport viewport = StateManager.GraphicsDevice.Viewport;

            spriteBatch.Begin();
            spriteBatch.Draw(image, imagePos, Color.White * this.TransitionValue);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
