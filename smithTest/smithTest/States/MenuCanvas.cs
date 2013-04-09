// ***************************************************************************
// ** SmithTest - demo app for smithNgine framework                         **
// **                                                                       **
// ** Copyright (C) 2013 by Erno Pakarinen. All Rights Reserved.            **
// ** Contact: erno@codesmith.fi                                            **
// ***************************************************************************

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Codesmith.SmithNgine.GameState;
using Codesmith.SmithNgine.Gfx;
using Codesmith.SmithNgine.Particles;

namespace Codesmith.SmithTest
{
    class MenuCanvas : GameCanvas
    {
        Texture2D entryTexture;
        List<MenuEntry> menuEntries = new List<MenuEntry>();
        MenuEntry exitMenuEntry;
        MenuEntry playMenuEntry;
        MenuEntry physicsMenuEntry;
        MenuEntry optionsMenuEntry;
        GameState playState;
        GameState physicsState;
        ParticleSystem particleSystem;

        AnimatedSprite animSprite;

        public MenuCanvas(GameState playState, GameState physicState)
        {
            this.playState = playState;
            this.physicsState = physicState;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            entryTexture = StateManager.Content.Load<Texture2D>("Images/button_clean");
            Vector2 pos = new Vector2(Bounds.Width / 2, 100);
            playMenuEntry = CreateMenuEntry(entryTexture, "Play", pos, Keys.F1);
            playMenuEntry.AnimState = -0.5f;
            pos.Y += entryTexture.Height + 10;
            optionsMenuEntry = CreateMenuEntry(entryTexture, "Options", pos, Keys.F2);
            optionsMenuEntry.AnimState = 0.0f;
            pos.Y += entryTexture.Height + 10;
            physicsMenuEntry = CreateMenuEntry(entryTexture, "Physics", pos, Keys.F3);
            physicsMenuEntry.AnimState = 1.0f;
            pos.Y += entryTexture.Height + 10;
            exitMenuEntry = CreateMenuEntry(entryTexture, "Exit", pos, Keys.Escape);
            exitMenuEntry.AnimState = 0.5f;

            animSprite = new AnimatedSprite(StateManager.Content, "Images/soniccd", 11, 1);
            animSprite.Position = new Vector2(Bounds.Width / 2, 400);
            animSprite.Style = AnimatedSprite.AnimationStyle.Manual;
            animSprite.InputEventSource = StateManager.Input;
            AddComponent(animSprite);

            particleSystem = new ParticleSystem();
            ParticleEffect effect1 = new ParticleEffect();
            ParticleEmitter emitter1 = new ParticleEmitter(animSprite.Position);
            emitter1.AddTexture(StateManager.Content.Load<Texture2D>("Images/flower"));
            emitter1.AddTexture(StateManager.Content.Load<Texture2D>("Images/circle"));
            effect1.AddEmitter(emitter1);
            particleSystem.AddEffect(effect1);
        }

        private MenuEntry CreateMenuEntry(Texture2D t, String label, Vector2 position, Keys key = Keys.None)
        {
            MenuEntry entry = new MenuEntry(t, label, StateManager.Font);
            entry.InputEventSource = StateManager.Input;
            entry.TransitionSource = State;
            entry.Position = position;
            entry.ButtonClickStyle = ButtonStyle.AnimateOnPress | ButtonStyle.AnimateIdle | ButtonStyle.Highlight;
            if (key != Keys.None)
            {
                entry.ButtonClicked += button_ButtonClicked;
                entry.BindKey(key);
            }
            AddComponent(entry);
            menuEntries.Add(entry);
            return entry;
        }

        private void button_ButtonClicked(object sender, EventArgs e)
        {
            if (sender == exitMenuEntry)
            {
                StateManager.ExitRequested = true;
                State.ExitState();
            }
            else if (sender == playMenuEntry)
            {
                StateManager.SwitchToState(playState);
            }
            else if (sender == physicsMenuEntry)
            {
                StateManager.SwitchToState(physicsState);
            }

        }
        
        public override void Update(GameTime gameTime)
        {
            particleSystem.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = StateManager.SpriteBatch;

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            foreach (MenuEntry m in menuEntries)
            {
                m.Draw(spriteBatch);
            }

            particleSystem.Draw(spriteBatch);
            animSprite.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
