using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using Codesmith.SmithNgine.GameState;
using Codesmith.SmithNgine.Input;

namespace Codesmith.SmithTest
{
    public class GamingState : GameState
    {
        private Texture2D image;

        public GamingState(String name)
            : base(name)
        {
            this.EnterStateInterval = TimeSpan.FromSeconds(2.0f);
            this.ExitStateInterval = TimeSpan.FromSeconds(2.0f);
            this.PauseStateInterval = TimeSpan.FromSeconds(0.5f);
        }

        public override void LoadContent()
        {
            image = StateManager.Game.Content.Load<Texture2D>("Images/snowmountain");

            base.LoadContent();
        }

        public override void Initialize()
        {
            GameCanvas canvas = new GamingStatusCanvas();
            canvas.TransitionSource = this;
            this.AddCanvas(canvas);
            base.Initialize();            
        }

        public override void HandleInput(InputManager input)
        {
            PlayerIndex source;
            if (input.IsKeyPressed(Keys.Pause,null,out source))
            {
                if (IsActive)
                {
                    Pause();
                }
                else if (IsPaused)
                {
                    UnPause();
                }
            }
            base.HandleInput(input);
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
