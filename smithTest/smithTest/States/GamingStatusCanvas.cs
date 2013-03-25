using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Codesmith.SmithNgine.GameState;
using Codesmith.SmithNgine.Gfx;

namespace Codesmith.SmithTest
{
    class GamingStatusCanvas : GameCanvas
    {
        private List<Sprite> sprites = new List<Sprite>();
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
            sprites.Add(new Sprite(StateManager.Content.Load<Texture2D>("Images/j1")));
            sprites.Add(new Sprite(StateManager.Content.Load<Texture2D>("Images/j2")));
            sprites.Add(new Sprite(StateManager.Content.Load<Texture2D>("Images/j3")));

            int i = 20;
            foreach (Sprite s in sprites)
            {
                s.TransitionSource = this.State;
                s.Position = new Vector2(i, 80);
                i += 60;

            }
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
            foreach (Sprite sprite in this.sprites)
            {
                sprite.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
