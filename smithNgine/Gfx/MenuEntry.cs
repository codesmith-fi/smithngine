/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Codesmith.SmithNgine.Gfx
{
    /// <summary>
    /// Simple menuentry which extends the spritebutton and gains it's behaviour.
    /// </summary>
    public class MenuEntry : SpriteButton
    {
        #region Fields
        private String label;
        private SpriteFont font;
        private Vector2 entryOrigin;
        private int id;
        #endregion

        #region Events
        public event EventHandler<MenuEntryEventArgs> MenuEntrySelected;
        #endregion

        #region Constructors
        public MenuEntry(int id, Texture2D texture, String label, SpriteFont font) 
            : base(texture)        
        {
            this.font = font;
            this.label = label;
            this.id = id;
            entryOrigin = font.MeasureString(label) / 2;
        }
        #endregion

        #region New methods
        /// <summary>
        /// Draws the MenuEntry
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to use, Begin() must've been called</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw the spritebutton stuff first
            base.Draw(spriteBatch);

            // Draw the text
            spriteBatch.DrawString(font, label, this.Position, Color.Black, Rotation, entryOrigin, Scale, SpriteEffects.None, Order);
        }
        #endregion

        /// <summary>
        /// Overridden, report focus gain as menuentry selection
        /// </summary>
        public override void GainFocus()
        {
            base.GainFocus();
            if (MenuEntrySelected != null)
            {
                MenuEntryEventArgs args = new MenuEntryEventArgs(this.id);
                MenuEntrySelected(this, args);
            }
        }
    }
}
