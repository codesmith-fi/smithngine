using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Codesmith.SmithNgine.GameState;
using Codesmith.SmithNgine.Gfx;

namespace Codesmith.SmithTest
{
    class MenuCanvas : GameCanvas
    {
        Vector2 textPos;
        Vector2 moveDelta;

        public MenuCanvas()
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
            this.textPos = new Vector2(Bounds.Width / 2, 10);
            moveDelta.Y = 6;
        }

        public override void Update(GameTime gameTime)
        {
            this.textPos += this.moveDelta;
            if (textPos.Y >= Bounds.Height - 20)
            {
                this.moveDelta.Y = -4;
            }
            if (textPos.Y <= 20)
            {
                this.moveDelta.Y = 4;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = StateManager.SpriteBatch;
            String text = "Hello world from " + this.GetType().ToString();
            Vector2 origin = StateManager.Font.MeasureString(text);
            textPos.X = Bounds.Width / 2 - origin.X / 2;

            StateManager.DimWithAlpha(0.5f*TransitionSource.TransitionValue, Bounds);

            spriteBatch.Begin();
            spriteBatch.DrawString(StateManager.Font, text, textPos, Color.Green * TransitionSource.TransitionValue);
            spriteBatch.End();
        }
    }
}
