using System;
using Microsoft.Xna.Framework.Graphics;

namespace Codesmith.SmithNgine.Gfx
{
    /// <summary>
    /// Implements a texture atlas which contains a single bitmap having multiple
    /// smaller images, can have one or more rows and colums
    /// </summary>
    public class TextureAtlas : IDisposable
    {
        #region Fields
        private Texture2D texture;
        private int rowCount;
        private int columnCount;
        private int frameCount;
        #endregion

        #region Properties
        /// <summary>
        /// Get the number of rows in the atlas
        /// </summary>
        public int Rows
        {
            get { return rowCount; }
        }

        public int Columns
        {
            get { return columnCount; }
        }

        public int Frames
        {
            get { return frameCount; }
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
        public TextureAtlas(Texture2D textureAtlas, int rows = 1, int columns = 1) 
            : this(textureAtlas, 0, rows, columns)
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
        /// <remarks>
        /// Defaults to 1 column, 1 row (= single texture)
        /// </remarks>
        /// <param name="textureAtlas"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="frames"></param>
        public TextureAtlas(Texture2D textureAtlas, int frames = 1, int rows = 1, int columns = 1)
        {
            texture = textureAtlas;
            rowCount = rows;
            columnCount = columns;
            if (frames == 0)
            {
                frameCount = rows * columns;
            }
            else
            {
                frameCount = frames;
            }
        }
        #endregion


        #region From Base classes and interfaces
        public void Dispose()
        {
            texture.Dispose();
        }
        #endregion
    }
}
