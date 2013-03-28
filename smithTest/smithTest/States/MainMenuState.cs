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
        private GameCanvas menuCanvas;
        private Effect postEffect;
        float effectTimer = 0.0f;
        public bool exitActivated = false;

        public MainMenuState(String name)
            : base(name)
        {
            this.EnterStateInterval = TimeSpan.FromSeconds(1.0f);
            this.ExitStateInterval = TimeSpan.FromSeconds(1.0f);
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
            image = StateManager.Content.Load<Texture2D>("Images/desert");
            menuCanvas.Bounds = new Rectangle(20, 20, Bounds.Width - 40, 200);

            postEffect = StateManager.FrameworkContent.Load<Effect>("Effects/GaussianBlur");
            PostProcessingEffect = postEffect;
        }

        public override void Dismiss()
        {
            base.Dismiss();
            if (StateManager.ExitRequested)
            {
                StateManager.Game.Exit();
            }
        }

        public override void Update(GameTime gameTime)
        {
            this.effectTimer += (float)gameTime.ElapsedGameTime.Milliseconds / 500;
            postEffect.Parameters["intensity"].SetValue(1.0f - TransitionValue);
            postEffect.Parameters["colorIntensity"].SetValue(TransitionValue);
            if (Status == GameStateStatus.Running)
            {
                PostProcessingEffect = null;
            }
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
            base.HandleInput(input);
        }

        private void Input_MousePositionChanged(object sender, MousePositionEventArgs args)
        {
        }
    }
}
