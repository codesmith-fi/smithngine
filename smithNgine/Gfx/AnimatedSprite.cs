/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */

namespace Codesmith.SmithNgine.Gfx
{
    using System;
    using System.Diagnostics;
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
//        private int countRows = 0;
//        private int countColumns = 0;
        private float timePerFrame = 0;
        private float totalElapsed = 0;
        private int fpsValue = 0;
        private int currentFrame = 0;
        private int animFirstFrame = 0;
        private int animLastFrame = 0;
        private TextureAtlas atlas = null;
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
                return 1 + animLastFrame - animFirstFrame;
            }
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Constructs an AnimatedSprite using a TextureAtlas allowing to have multiple separate
        /// animations in the same TextureAtlas. 
        /// </summary>
        /// <param name="frameAtlas">TextureAtlas to be used</param>
        /// <param name="firstFrame">Number of the first the animation</param>
        /// <param name="lastFrame">Number of the last frame in the animation</param>
        /// <param name="fps">The speed of the animation (FPS), default is 25</param>
        public AnimatedSprite(TextureAtlas frameAtlas, int firstFrame, int lastFrame, int fps = 25)
            : base(Rectangle.Empty)
        {
            Debug.Assert(lastFrame >= firstFrame, "Arguments: lastFrame cannot exist before firstFrame!");
            Texture = frameAtlas.Texture;
            FrameSize = frameAtlas.FrameSize;
            atlas = frameAtlas;
            animFirstFrame = firstFrame;
            animLastFrame = lastFrame;
            currentFrame = firstFrame;
            Fps = fps;
            Continue(true);
        }

        /// <summary>
        /// Instantiate a new AnimatedSprite using a Texture2D. Uses all the subimages in the texture
        /// for the animation
        /// </summary>
        /// <param name="frameAtlas">The texture to be used for frames</param>
        /// <param name="columnCount">How many columns of frames there are in the atlas</param>
        /// <param name="rowCount">How many rows of frames there are in the atlas</param>
        /// <param name="frameCount">Framecount, use this in case bottom row is full. If not specified, framecount is calculated using rows*cols</param>
        public AnimatedSprite(Texture2D frameAtlas, int columnCount = 1, int rowCount = 1, int frameCount = 0)
            : base(Rectangle.Empty)
        {
            Debug.Assert(columnCount > 0, "Argument: No columns for the animation in the texture");
            Debug.Assert(rowCount > 0, "Argument: No rows for the animation in the texture");

            atlas = new TextureAtlas(frameAtlas, columnCount, rowCount, frameCount);
            Texture = atlas.Texture;
            FrameSize = atlas.FrameSize;
            animFirstFrame = 0;
            int frames = frameCount > 0 ? frameCount : rowCount * columnCount;
            animLastFrame = frames - 1;
            currentFrame = animFirstFrame;
            Fps = 25;
            Continue(true);
        }
        #endregion 

        #region New methods
        public void Continue(bool reset = false)
        {
            if (reset)
            {
                currentFrame = animFirstFrame;
                totalElapsed = 0;
            }
            IsAnimating = true;
        }

        public void Pause()
        {
            IsAnimating = false;
        }

        public void Restart()
        {
            Continue(true);
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

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(atlas.Texture, Position, atlas.FrameRectangle(currentFrame), 
                Color, Rotation, Origin, Scale, SpriteEffects.None, Order);
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
                // Keep the Frame between firstFrame and the lastFrame.
                if (currentFrame >= animLastFrame)
                {
                    if (Style.HasFlag(AnimationStyle.Loop))
                    {
                        currentFrame = animFirstFrame;
                    }
                    else
                    {
                        animating = false;
                        currentFrame = animLastFrame;
                    }
                }
                totalElapsed -= timePerFrame;
            }
            return animating;
        }
        #endregion
    }
}
