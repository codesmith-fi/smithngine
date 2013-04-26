/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */

namespace Codesmith.SmithNgine.Gfx
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Extends Sprite and brings in animation options
    /// </summary>
    public class AnimatedSprite : Sprite
    {
        #region Enumerations
        [Flags]
        public enum AnimationStyle : int
        {
            None    = 0,
            Loop    = 1,
            Manual  = Loop << 1
        }
        #endregion

        #region Fields
        private int countRows = 0;
        private int countColumns = 0;
        private int countFrames = 0;
        private float timePerFrame = 0;
        private float totalElapsed = 0;
        private int fpsValue = 0;
        private int currentFrame = 0;
        #endregion

        #region Properties
        public bool IsAnimating
        {
            get;
            protected set;
        }

        public AnimationStyle Style
        {
            get;
            set;
        }

        public int Fps
        {
            set
            {
                this.timePerFrame = 1.0f / value;
                this.fpsValue = value;
            }
            get { return this.fpsValue; }
        }

        public int FrameCount
        {
            get
            {
                return this.countFrames;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiate a new AnimatedSprite using a Texture2D
        /// </summary>
        /// <param name="frameAtlas">The texture to be used for frames</param>
        /// <param name="columnCount">How many columns of frames there are in the atlas</param>
        /// <param name="rowCount">How many rows of frames there are in the atlas</param>
        /// <param name="frameCount">Framecount, use this in case bottom row is full. If not specified, framecount is calculated using rows*cols</param>
        public AnimatedSprite(Texture2D frameAtlas, int columnCount = 1, int rowCount = 1, int frameCount = -1)
            : base(Rectangle.Empty)
        {
            FrameSize = new Rectangle(0, 0, frameAtlas.Bounds.Width / columnCount, frameAtlas.Bounds.Height / rowCount);
            countFrames = frameCount >= 0 ? frameCount : rowCount * columnCount;
            countRows = rowCount;
            countColumns = columnCount;
            currentFrame = 0;
            Fps = 60;
            Texture = frameAtlas;
            Continue(true);
        }

        /// <summary>
        /// Instantiate new AnimatedSprite from content asset
        /// </summary>
        /// <param name="content">ContentManager instance</param>
        /// <param name="assetName">Name of the texture asset to load</param>
        /// <param name="columnCount">How many columns of frames there are in the atlas</param>
        /// <param name="rowCount">How many rows of frames there are in the atlas</param>
        /// <param name="frameCount">Framecount, use this in case bottom row is full. If not specified, framecount is calculated using rows*cols</param>
        public AnimatedSprite(ContentManager content, string assetName, int columnCount = 1, int rowCount = 1, int frameCount = -1) 
            : base(Rectangle.Empty)
        {
            Texture = content.Load<Texture2D>(assetName);
            FrameSize = new Rectangle(0, 0, Texture.Width / columnCount, Texture.Height / rowCount);
            countFrames = frameCount >= 0 ? frameCount : rowCount * columnCount;
            countRows = rowCount;
            countColumns = columnCount;
            Fps = 10;
            Continue(true);
        }
        #endregion 

        #region New methods
        public void Continue(bool reset = false)
        {
            if (reset)
            {
                currentFrame = 0;
                totalElapsed = 0;
            }
            IsAnimating = true;
        }

        public void Pause()
        {
            IsAnimating = false;
        }
        #endregion

        #region Methods from base class
        public override void Update(GameTime gameTime)
        {
            if (IsAnimating && !UpdateAnimation((float)gameTime.ElapsedGameTime.TotalSeconds))
            {
                IsAnimating = false;
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Texture != null)
            {
                int currentRow = (int)((float)this.currentFrame / (float)countColumns);
                int currentColumn = this.currentFrame % countColumns;
                Rectangle frameRect = FrameSize;
                frameRect.X = frameRect.Width * currentColumn;
                frameRect.Y = frameRect.Height * currentRow;
                spriteBatch.Draw(this.Texture, Position, frameRect, Color, Rotation, Origin, Scale, SpriteEffects.None, Order);
            }
        }

        public override void GainFocus()
        {
            if (Style.HasFlag(AnimationStyle.Manual))
            {
                Continue(true);
            }
            base.GainFocus();
        }
        #endregion

        #region Private methods
        private bool UpdateAnimation(float elapsed)
        {
            bool animating = true;
            totalElapsed += elapsed;
            if (totalElapsed > timePerFrame)
            {
                currentFrame++;
                // Keep the Frame between 0 and the total frames, minus one.
                if (currentFrame >= countFrames)
                {
                    if (Style.HasFlag(AnimationStyle.Loop))
                    {
                        currentFrame = 0;
                    }
                    else
                    {
                        animating = false;
                        currentFrame = countFrames - 1;
                    }
                }
                totalElapsed -= timePerFrame;
            }
            return animating;
        }
        #endregion
    }
}
