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
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Simple menuentry which extends the spritebutton and gains it's behaviour.
    /// </summary>
    public class MenuEntry : SpriteButton
    {
        #region Fields
        private String label;
        private SpriteFont font;
        private Vector2 entryOrigin;
        #endregion

        #region Properties
        public Color TextColor
        {
            get;
            set;
        }
        #endregion

        #region Events
        public event EventHandler<MenuEntryEventArgs> MenuEntrySelected;
        #endregion

        #region Constructors
        public MenuEntry(Texture2D texture, String label, SpriteFont font) 
            : base(texture)        
        {
            this.font = font;
            this.label = label;
            TextColor = Color.Black;
            entryOrigin = font.MeasureString(label) / 2;
        }
        #endregion

        #region From base class
        /// <summary>
        /// Draws the MenuEntry
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to use, Begin() must've been called</param>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Draw the spritebutton stuff first
            base.Draw(spriteBatch, gameTime);

            // Draw the text
            spriteBatch.DrawString(font, label, Position, TextColor, Rotation, entryOrigin, Scale, SpriteEffects.None, Order);
        }

        /// <summary>
        /// Overridden, report focus gain as menuentry selection
        /// </summary>
        public override void GainFocus()
        {
            base.GainFocus();
            if (MenuEntrySelected != null)
            {
                MenuEntryEventArgs args = new MenuEntryEventArgs(activePlayerIndex);
                MenuEntrySelected(this, args);
            }
        }
        #endregion
    }
}
