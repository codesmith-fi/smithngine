﻿// ***************************************************************************
// ** SmithTest - demo app for smithNgine framework                         **
// **                                                                       **
// ** Copyright (C) 2013 by Erno Pakarinen. All Rights Reserved.            **
// ** Contact: erno@codesmith.fi                                            **
// ***************************************************************************
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
            Vector2 origin = font.MeasureString(label);
            spriteBatch.DrawString(font, label, this.Position, Color.Black, Rotation, origin/2, Scale, SpriteEffects.None, Order);
        }
    }
}
