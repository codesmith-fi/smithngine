using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Codesmith.SmithNgine.GameState;

namespace Codesmith.SmithTest
{
    public class MainMenuState : GameState
    {
        private Texture2D image;
        private GameCanvas menuCanvas;
        private Effect postEffect;
        float effectTimer = 0.0f;
        public bool exitActivated = false;
        GameState playState;

        public MainMenuState(String name, GameState playState)
            : base(name)
        {
            this.EnterStateInterval = TimeSpan.FromSeconds(1.0f);
            this.ExitStateInterval = TimeSpan.FromSeconds(1.0f);
            this.playState = playState;
        }

        public override void Initialize()
        {
            menuCanvas = new MenuCanvas(this.playState);
            menuCanvas.TransitionSource = this;
            AddCanvas(menuCanvas);            
            base.Initialize();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            image = StateManager.Content.Load<Texture2D>("Images/desert");

            // Set a postprocess effect, this will blur the display with the 
            // state in/out transition value
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
            if (Status == GameStateStatus.Running)
            {
                this.PostProcessingEffect = null;
            }
            // Update parameters in the post processing effect. 
            this.effectTimer += (float)gameTime.ElapsedGameTime.Milliseconds / 500;
            postEffect.Parameters["intensity"].SetValue(1.0f - TransitionValue);
            postEffect.Parameters["colorIntensity"].SetValue(TransitionValue);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = StateManager.SpriteBatch;
            Viewport viewport = StateManager.GraphicsDevice.Viewport;

            spriteBatch.Begin();
            spriteBatch.Draw(image, 
                new Rectangle(0, 0, viewport.Width, viewport.Height), 
                Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
