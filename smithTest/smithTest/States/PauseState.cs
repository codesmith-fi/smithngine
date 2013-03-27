using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Codesmith.SmithNgine.GameState;
using Codesmith.SmithNgine.Gfx;

namespace Codesmith.SmithTest
{
    public class PauseState : GameState
    {
        private Texture2D image;
        private Vector2 imagePos;
        private SpriteButton button;

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

            button = new SpriteButton(StateManager.Game.Content.Load<Texture2D>("Images/j1"));
            button.MouseEventSource = StateManager.Input;
            button.TransitionSource = this;
            button.Position = new Vector2( Bounds.Width / 2, Bounds.Height / 2 + 50);
            AddChild(button);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            button.Animate(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = StateManager.SpriteBatch;
            Viewport viewport = StateManager.GraphicsDevice.Viewport;

            spriteBatch.Begin();
            spriteBatch.Draw(image, imagePos, Color.White * this.TransitionValue);
            button.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
