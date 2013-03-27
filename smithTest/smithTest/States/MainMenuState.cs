using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Codesmith.SmithNgine.GameState;
using Codesmith.SmithNgine.Input;
using Microsoft.Xna.Framework.Input;

namespace Codesmith.SmithTest
{
    public class MainMenuState : GameState
    {
        private Texture2D image;
        private GameCanvas menuCanvas1;
        private GameCanvas menuCanvas2;

        public MainMenuState(String name)
            : base(name)
        {
            this.EnterStateInterval = TimeSpan.FromSeconds(1.0f);
            this.ExitStateInterval = TimeSpan.FromSeconds(1.0f);
        }

        public override void Initialize()
        {
            menuCanvas1 = new MenuCanvas();
            menuCanvas1.TransitionSource = this;
            menuCanvas2 = new MenuCanvas();
            menuCanvas2.TransitionSource = this;
            AddCanvas(menuCanvas1);
            AddCanvas(menuCanvas2);
            base.Initialize();
        }
        public override void LoadContent()
        {
            base.LoadContent();
            StateManager.Input.MousePositionChanged += Input_MousePositionChanged;
            image = StateManager.Game.Content.Load<Texture2D>("Images/desert");
            menuCanvas1.Bounds = new Rectangle(20, 20, Bounds.Width - 40, 200);
            menuCanvas2.Bounds = new Rectangle(20, menuCanvas1.Bounds.Y + menuCanvas1.Bounds.Height + 20, Bounds.Width - 40, 200);
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

        public override void HandleInput(InputManager input)
        {
            PlayerIndex source;
            if (input.IsKeyPressed(Keys.Escape, null, out source))
            {
                StateManager.Game.Exit();
            }
            base.HandleInput(input);
        }

        private void Input_MousePositionChanged(object sender, MousePositionEventArgs args)
        {
        }
    }
}
