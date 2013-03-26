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
        Rectangle area;

        public MenuCanvas()
        {
        }

        public override void LoadContent()
        {
            this.area = StateManager.GraphicsDevice.Viewport.Bounds;
            this.textPos = new Vector2(this.area.Width / 2, 10);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = StateManager.SpriteBatch;
            String text = "Hello world from " + this.GetType().ToString();
            Vector2 origin = StateManager.Font.MeasureString(text);
            textPos.X = this.area.Width / 2 - origin.X / 2;

            StateManager.DimWithAlpha(0.5f*TransitionSource.TransitionValue, Bounds);

            spriteBatch.Begin();
            spriteBatch.DrawString(StateManager.Font, text, textPos, Color.Green * TransitionSource.TransitionValue);
            spriteBatch.End();
        }
    }
}
