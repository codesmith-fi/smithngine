using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Codesmith.SmithNgine.Gfx;

namespace Codesmith.SmithTest
{
    class MenuEntry : SpriteButton
    {
        private String label;
        private SpriteFont font;

        #region Constructors
        public MenuEntry(Texture2D texture, String label, SpriteFont font) 
            : base(texture)        
        {
            this.font = font;
            this.label = label;
        }
        #endregion

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(font, label, this.Position, Color.Black);
        }
    }
}
