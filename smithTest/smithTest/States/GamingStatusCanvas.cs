using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Codesmith.SmithNgine.GameState;

namespace Codesmith.SmithTest
{
    class GamingStatusCanvas : GameCanvas
    {
        private Texture2D bitmap;
        Vector2 textPos;
        Rectangle area;
        Vector2 moveDelta;

        public GamingStatusCanvas()
        {
        }

        public override void LoadContent()
        {
            this.area = StateManager.GraphicsDevice.Viewport.Bounds;
            this.textPos = new Vector2(this.area.Width / 2, 10);
            this.moveDelta = new Vector2(0, 2);
            //            this.bitmap = StateManager.Game.Content.Load<Texture2D>("");
        }

        public override void Update(GameTime gameTime)
        {
            this.textPos += this.moveDelta;
            if (textPos.Y >= area.Height - 20)
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
            textPos.X = this.area.Width / 2 - origin.X / 2;
            spriteBatch.Begin();
            spriteBatch.DrawString(StateManager.Font, text, textPos, Color.Green * this.State.TransitionValue );
            spriteBatch.End();
        }
    }
}
