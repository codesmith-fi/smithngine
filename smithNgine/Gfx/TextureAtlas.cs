namespace Codesmith.SmithNgine.Gfx
{
    using System;
    using System.Diagnostics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Implements a texture atlas which contains a single bitmap having multiple
    /// smaller images, can have one or more rows and colums
    /// </summary>
    public class TextureAtlas : DrawableGameObject
    {
        #region Fields
        private Texture2D texture;
        private int rowCount;
        private int columnCount;
        private int frameCount;
        private Rectangle frameSize;
        #endregion

        #region Properties
        public Texture2D Texture
        {
            get { return texture; }
        }

        /// <summary>
        /// Get the number of rows in the atlas
        /// </summary>
        public int Rows
        {
            get { return rowCount; }
        }

        /// <summary>
        /// Get the number of columns in the atlas
        /// </summary>
        public int Columns
        {
            get { return columnCount; }
        }

        /// <summary>
        /// Get the number of frames in the atlas
        /// 
        /// Not, it's not always frames*columns, last row might be partial
        /// </summary>
        public int Frames
        {
            get { return frameCount; }
        }

        public Rectangle FrameSize
        {
            get { return frameSize; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructs a Texture atlas with given texture (of multiple 
        /// smaller images as a grid)
        /// and number of rows and columns per rof
        /// </summary>
        /// <remarks>
        /// This constructor assumes that each row has the same number of 
        /// columns! Use the other constructor if the last row is not full!
        /// 
        /// Defaults to 1 column, 1 row (= single texture)
        /// </remarks>
        /// <param name="textureAtlas">Texture to be used as a atlas</param>
        /// <param name="rows">Row count, default is 1</param>
        /// <param name="columns">Column count, default is 1</param>
        public TextureAtlas(Texture2D textureAtlas, int columns, int rows) 
            : this(textureAtlas, columns, rows, 1)
        {
        }

        /// <summary>
        /// Constructs a TextureAtlas with given texture (of multiple smaller
        /// images as a grid) and number of frames, rows and columns. 
        /// 
        /// The "frames" parameter tells the constructor the exact framecount,
        /// this is useful if the last row is not full (meaning, does not have 
        /// all the columns).
        /// </summary>
        /// <param name="textureAtlas"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="frames"></param>
        public TextureAtlas(Texture2D textureAtlas, int columns, int rows, int frames)
        {
            Debug.Assert(columns > 0, "Parameter columns must be > 0");
            Debug.Assert(rows > 0, "Parameter rows must be > 0");
            texture = textureAtlas;
            rowCount = rows;
            columnCount = columns;

            frameSize = new Rectangle(0, 0, texture.Width / columnCount, texture.Height / rowCount);
            frameCount = frames > 1 ? frameCount : rows * columns;
        }
        #endregion

        #region New methods

        public int Row(int frameNum)
        {
            return frameNum / columnCount;
        }

        public int Column(int frameNum)
        {
            return frameNum % columnCount;
        }

        /// <summary>
        /// Construct a sprite from a specific frame in this atlas
        /// </summary>
        /// <param name="frameNum"></param>
        /// <returns></returns>
        public Sprite MakeSprite(int frameNum)
        {
            Debug.Assert((frameNum >= 0 && frameNum < frameCount),
                "Requested frame does not exist");
            Debug.Assert(frameNum < frameCount, "Requested frame does not exist");
            return new AtlasSprite(this, frameNum);
        }

        public Texture2D MakeTexture(int frameNum)
        {
            Debug.Assert((frameNum >= 0 && frameNum < frameCount),
                "Requested frame does not exist");
            Rectangle frameRect = FrameRectangle(frameNum);
            Texture2D output = null;
//            texture.GetData<Color>(
            //Color[] imagePiece = GetImageData(imageData, image.Width, sourceRectangle);
            return output;
        }

        public Rectangle FrameRectangle(int frameNum)
        {
            Debug.Assert((frameNum >= 0 && frameNum < frameCount),
                "Requested frame does not exist");
            int currentRow = frameNum / columnCount;
            int currentColumn = frameNum % columnCount;
            Rectangle frameRect = FrameSize;
            frameRect.X = frameRect.Width * currentColumn;
            frameRect.Y = frameRect.Height * currentRow;
            return frameRect;
        }
        #endregion

        #region From Base classes and interfaces
        public override void Dispose()
        {
            texture.Dispose();
        }
        #endregion

        #region Private methods
        #endregion
    }
}
