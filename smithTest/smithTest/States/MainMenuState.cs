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
        private Effect postEffect;
        private RenderTarget2D renderTarget;
        float effectTimer = 0.0f;
        bool exitActivated = false;

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

            postEffect = StateManager.Game.Content.Load<Effect>("Effects/GaussianBlur");
            PostProcessingEffect = postEffect;
        }

        public override void Dismiss()
        {
            base.Dismiss();
            if (this.exitActivated)
            {
                StateManager.Game.Exit();
            }
        }

        public override void Update(GameTime gameTime)
        {
            this.effectTimer += (float)gameTime.ElapsedGameTime.Milliseconds / 500;
            postEffect.Parameters["intensity"].SetValue(1.0f - TransitionValue);
            postEffect.Parameters["colorIntensity"].SetValue(TransitionValue);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = StateManager.SpriteBatch;
            GraphicsDevice graphicsDevice = StateManager.GraphicsDevice;
            Viewport viewport = StateManager.GraphicsDevice.Viewport;

            spriteBatch.Begin();
            spriteBatch.Draw(image, 
                new Rectangle(0, 0, viewport.Width, viewport.Height), 
                Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void HandleInput(InputManager input)
        {
            PlayerIndex source;
            if (input.IsKeyPressed(Keys.Escape, null, out source))
            {
                exitActivated = true;
                ExitState();
            }
            base.HandleInput(input);
        }

        private void Input_MousePositionChanged(object sender, MousePositionEventArgs args)
        {
        }
    }
}
