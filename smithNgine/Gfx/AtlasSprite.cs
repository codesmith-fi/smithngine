namespace Codesmith.SmithNgine.Gfx
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// A sprite that was created from TextureAtlas.
    /// 
    /// Shows one frame of the atlas.
    /// 
    /// Btw, this basically shows how simple it is to extend the Sprite 
    /// class for multiple purposes.
    /// </summary>
    public class AtlasSprite : Sprite
    {
        #region Fields
        // Row number of this AtlasSprite 
        private int row;
        // Column number of this AtlasSprite
        private int column;
        #endregion

        #region Constructors
        /// <summary>
        /// Construct AtlasSprite with given TextureAtlas and a frame number
        /// </summary>
        /// <param name="atlas">TextureAtlas to be used</param>
        /// <param name="frame">Frame number in texture atlas</param>
        public AtlasSprite(TextureAtlas atlas, int frame) :
            base(atlas.Texture)
        {
            FrameSize = atlas.FrameRectangle(frame);
            row = atlas.Row(frame);
            column = atlas.Column(frame);
        }
        #endregion

        #region Methods from base classes
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Rectangle frameRect = FrameSize;
            frameRect.X = frameRect.Width * column;
            frameRect.Y = frameRect.Height * row;
            spriteBatch.Draw(this.Texture, Position, frameRect, 
                Color, Rotation, Origin, Scale, SpriteEffects.None, Order);
        }
        #endregion
    }
}
