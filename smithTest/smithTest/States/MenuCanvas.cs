using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Codesmith.SmithNgine.GameState;
using Codesmith.SmithNgine.Gfx;
using Microsoft.Xna.Framework.Input;

namespace Codesmith.SmithTest
{
    class MenuCanvas : GameCanvas
    {
        Texture2D entryTexture;
        List<MenuEntry> menuEntries = new List<MenuEntry>();
        MenuEntry exitMenuEntry;

        public MenuCanvas()
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
            entryTexture = StateManager.Content.Load<Texture2D>("Images/button_clean");
            Vector2 pos = new Vector2(Bounds.Width / 2 - entryTexture.Bounds.Width / 2, 100);
            menuEntries.Add( CreateMenuEntry(entryTexture, "Play", pos, Keys.F1));
            pos.Y += entryTexture.Height + 10;
            menuEntries.Add( CreateMenuEntry(entryTexture, "Options", pos, Keys.F2));
            pos.Y += entryTexture.Height + 10;
            exitMenuEntry = CreateMenuEntry(entryTexture, "Exit", pos, Keys.Escape);
            menuEntries.Add( exitMenuEntry );
        }

        private MenuEntry CreateMenuEntry(Texture2D t, String label, Vector2 position, Keys key = Keys.None)
        {
            MenuEntry entry = new MenuEntry(t, label, StateManager.Font);
            entry.InputEventSource = StateManager.Input;
            entry.TransitionSource = State;
            entry.Position = position;
            if (key != Keys.None)
            {
                entry.ButtonClicked += button_ButtonClicked;
                entry.BindKey(key);
            }
            AddObject(entry);
            return entry;
        }

        private void button_ButtonClicked(object sender, EventArgs e)
        {
            if (sender == exitMenuEntry)
            {
                StateManager.ExitRequested = true;
                State.ExitState();
            }
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = StateManager.SpriteBatch;

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            foreach (MenuEntry m in menuEntries)
            {
                m.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
