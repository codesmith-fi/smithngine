// ***************************************************************************
// ** SmithTest - demo app for smithNgine framework                         **
// **                                                                       **
// ** Copyright (C) 2013 by Erno Pakarinen. All Rights Reserved.            **
// ** Contact: erno@codesmith.fi                                            **
// ***************************************************************************

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Codesmith.SmithNgine.GameState;
using Codesmith.SmithNgine.Input;
using Codesmith.SmithNgine.Effects;
namespace Codesmith.SmithTest
{
    public class GamingState : GameState
    {
        private Texture2D image;
        private Effect postEffect;
        float effectTimer;

        public GamingState(String name)
            : base(name)
        {
            this.EnterStateInterval = TimeSpan.FromSeconds(1.0f);
            this.ExitStateInterval = TimeSpan.FromSeconds(1.0f);
            this.PauseStateInterval = TimeSpan.FromSeconds(0.25f);
        }

        public override void LoadContent()
        {
            image = StateManager.Content.Load<Texture2D>("Images/snowmountain");
            effectTimer = 0.0f;
            base.LoadContent();
        }

        public override void Initialize()
        {
            GameCanvas canvas = new GamingStatusCanvas();
            canvas.TransitionSource = this;
            this.AddCanvas(canvas);
            base.Initialize();            
        }

        public override void Update(GameTime gameTime)
        {
            this.effectTimer += (float)gameTime.ElapsedGameTime.Milliseconds / 500;
            if (Status == GameStateStatus.Running)
            {
                //StateManager.PostProcessingEffect = null;
            }
            base.Update(gameTime);
        }

        public override void HandleInput(InputManager input)
        {
            PlayerIndex source;
            if (input.IsKeyPressed(Keys.Pause,null,out source))
            {
                if (IsActive)
                {
                    StateManager.PauseCurrentState();
                }
                else if (IsPaused)
                {
                    StateManager.UnPauseCurrentState();
                }
            }
            base.HandleInput(input);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = StateManager.SpriteBatch;
            Viewport viewport = StateManager.GraphicsDevice.Viewport;

            spriteBatch.Begin();
            spriteBatch.Draw(image, new Rectangle(0, 0, viewport.Width, viewport.Height), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
