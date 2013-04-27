namespace Codesmith.SmithNgine.Gfx
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// A sprite that was created from TextureAtlas.
    /// Shows one frame of the atlas.
    /// </summary>
    public class AtlasSprite : Sprite
    {
        private int row;
        private int column;

        public AtlasSprite(TextureAtlas atlas, int frame) :
            base(atlas.Texture)
        {
            FrameSize = atlas.FrameRectangle(frame);
            row = atlas.Row(frame);
            column = atlas.Column(frame);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Rectangle frameRect = FrameSize;
            frameRect.X = frameRect.Width * column;
            frameRect.Y = frameRect.Height * row;
            spriteBatch.Draw(this.Texture, Position, frameRect, 
                Color, Rotation, Origin, Scale, SpriteEffects.None, Order);
        }
    }
}
