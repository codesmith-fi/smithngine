using System;
using System.Collections.Generic;
using Codesmith.SmithNgine.GameState;
using Codesmith.SmithNgine.Gfx;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Codesmith.SmithShooter.MainMenu
{
    internal enum MenuEntryId : int
    {
        MenuEntryExit = 1,
        MenuEntryPlay,
        MenuEntryOptions
    };

    public class MenuCanvas : GameCanvas
    {
        private GameState playState;
        private List<MenuEntry> menuEntries;
        private Texture2D menuTexture;
        public Vector2 Position
        {
            get;
            set;
        }

        public List<MenuEntry> MenuEntries
        {
            get
            { 
                return this.menuEntries;  
            }
        }

        public MenuCanvas(GameState playState)
        {
            Position = Vector2.Zero;
            this.playState = playState;
        }

        public override void UnloadContent()
        {
            menuTexture = null;
            foreach (MenuEntry entry in menuEntries)
            {
                RemoveComponent(entry);
            }
            base.UnloadContent();
        }

        public override void LoadContent()
        {
            SetupMenu();
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = StateManager.SpriteBatch;

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            foreach (MenuEntry m in menuEntries)
            {
                m.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        #region Private methods
        private void SetupMenu()
        {
            menuEntries = new List<MenuEntry>();

            menuTexture = StateManager.Content.Load<Texture2D>("Images/menubutton_back");
            Rectangle bounds = StateManager.GraphicsDevice.Viewport.Bounds;

            Vector2 position = new Vector2(bounds.Width - menuTexture.Width/2 - 10, bounds.Height - (3 * menuTexture.Height + 10));
            MenuEntry menuentry = CreateMenuEntry(menuTexture, "Play", position, Keys.F1);
            position.Y += menuTexture.Height + 10;
            menuentry = CreateMenuEntry(menuTexture, "Options", position, Keys.F2);
            position.Y += menuTexture.Height + 10;
            menuentry = CreateMenuEntry(menuTexture, "Quit", position, Keys.Escape);
        }

        private MenuEntry CreateMenuEntry(
            Texture2D t, String label, Vector2 position, Keys key = Keys.None)
        {
            MenuEntry entry = new MenuEntry(t, label, StateManager.Font);
            entry.Position = position;
            entry.Color = Color.White;
            entry.AnimState = 0.0f;
            entry.ClickBounceSpeed = TimeSpan.FromSeconds(0.1f);
            entry.InputEventSource = StateManager.Input;
            entry.MenuEntrySelected += menuentry_ExitMenuEntrySelected;
            if (key != Keys.None)
            {
                entry.BindKey(key);
            }
            entry.ButtonClickStyle = ButtonStyle.AnimateOnPress | ButtonStyle.AnimateIdle | ButtonStyle.Highlight;
            menuEntries.Add(entry);
            AddComponent(entry);
            return entry;
        }

        public void menuentry_PlayMenuEntrySelected(object sender, MenuEntryEventArgs e)
        {
            StateManager.SwitchToState(playState);
        }

        public void menuentry_OptionsMenuEntrySelected(object sender, MenuEntryEventArgs e)
        {
        }

        public void menuentry_ExitMenuEntrySelected(object sender, MenuEntryEventArgs e)
        {
            StateManager.ExitRequested = true;
            State.ExitState();
        }

        #endregion
    }
}
