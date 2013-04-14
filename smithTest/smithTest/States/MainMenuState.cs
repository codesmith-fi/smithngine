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
        GameState physicsState;

        public MainMenuState(String name, GameState state1, GameState state2)
            : base(name)
        {
            this.EnterStateInterval = TimeSpan.FromSeconds(1.0f);
            this.ExitStateInterval = TimeSpan.FromSeconds(1.0f);
            this.playState = state1;
            this.physicsState = state2;
        }

        public override void Initialize()
        {
            menuCanvas = new MenuCanvas(this.playState, this.physicsState);
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
//            postEffect = EffectFactory.Load(EffectFactory.EffectType.GaussianBlur, StateManager.Game);
//            PostProcessingEffect = postEffect;
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
//            postEffect.Parameters["intensity"].SetValue(1.0f - TransitionValue);
//            postEffect.Parameters["colorIntensity"].SetValue(TransitionValue);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = StateManager.SpriteBatch;
            Viewport viewport = StateManager.GraphicsDevice.Viewport;
/*
            spriteBatch.Begin();
            spriteBatch.Draw(image, 
                new Rectangle(0, 0, viewport.Width, viewport.Height), 
                Color.White);
            spriteBatch.End();
*/
            base.Draw(gameTime);
        }
    }
}
