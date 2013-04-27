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
using Codesmith.SmithNgine.Primitives;

namespace Codesmith.SmithTest
{
    public class MenuCanvas : GameCanvas
    {
        Texture2D entryTexture;
        List<MenuEntry> menuEntries;
        MenuEntry exitMenuEntry;
        MenuEntry playMenuEntry;
        MenuEntry physicsMenuEntry;
        MenuEntry optionsMenuEntry;
        GameState playState;
        GameState physicsState;
        ParticleSystem particleSystem;
        ParticleEmitter emitter;
        ParticleEffect particleEffect;
        AnimatedSprite animSprite;
        Random random = new Random();

        public MenuCanvas(GameState playState, GameState physicState)
        {
            this.playState = playState;
            this.physicsState = physicState;
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            RemoveComponent(animSprite);
            foreach (MenuEntry entry in menuEntries)
            {
                RemoveComponent(entry);
            }
            animSprite = null;
            menuEntries.Clear();
            exitMenuEntry = null;
            playMenuEntry = null;
            physicsMenuEntry = null;
            optionsMenuEntry = null;
            particleSystem = null;
            particleEffect = null;
            entryTexture = null;
            emitter = null;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            menuEntries = new List<MenuEntry>();
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
            particleEffect = new ParticleEffect();
            particleEffect.Rotation = 0f;
            particleEffect.Position = Vector2.Zero;

            emitter = new LineEmitter(new Vector2(-50,0), new Vector2(50,0));
            ParticleGenerationParams ep = new ParticleGenerationParams();           
            ep.SpeedRange = new Vector2(30.0f, 30.0f);
            ep.InitialSpeedVariation = 1.0f;
            ep.ScaleRange = new Vector2(0.4f, 1.0f);
            ep.InitialScaleVariation = 1.0f;
            ep.RotationRange = new Vector2(0.0f, MathHelper.TwoPi);
            ep.InitialRotationVariation = 1.0f;
            ep.AngularVelocityRange = new Vector2(-1f, 1.0f);
            ep.InitialAngularVelocityVariation = 1.0f;
            ep.TTLRange = new Vector2(500f,2000f);         
            ep.SpeedDamping = 1.01f;
            ep.AddTexture(StateManager.Content.Load<Texture2D>("Images/smoke1"));
            ep.AddTexture(StateManager.Content.Load<Texture2D>("Images/smoke2"));
            ep.AddTexture(StateManager.Content.Load<Texture2D>("Images/smoke3"));
            ep.OpacityRange = new Vector2(0.5f, 0.0f);
            ep.InitialOpacityVariation = 1.0f;
            emitter.Configuration = ep;
            particleEffect.AddEmitter(emitter);
            particleSystem.AddEffect(particleEffect);
        }

        private MenuEntry CreateMenuEntry(Texture2D t, String label, Vector2 position, Keys key = Keys.None)
        {
            MenuEntry entry = new MenuEntry(t, label, StateManager.Font);
            entry.InputEventSource = StateManager.Input;
            entry.TransitionSource = State;
            entry.Position = position;
            entry.ButtonClickStyle = ButtonStyle.AnimateOnPress | ButtonStyle.AnimateIdle | ButtonStyle.Highlight;
            entry.ButtonClicked += button_ButtonClicked;
            if (key != Keys.None)
            {
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
            particleEffect.Position = new Vector2(StateManager.Input.MouseX, StateManager.Input.MouseY);
            if (StateManager.Input.IsMouseButtonHeld(SmithNgine.Input.MouseButton.Right))
            {
                particleEffect.Generate(10);
            }
            
            particleSystem.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = StateManager.SpriteBatch;

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            foreach (MenuEntry m in menuEntries)
            {
                m.Draw(spriteBatch, gameTime);
            }
            animSprite.Draw(spriteBatch, gameTime);

            ShowParticleStatus(spriteBatch);
            particleSystem.Draw(spriteBatch, gameTime);
            spriteBatch.End();
        }

        private void ShowParticleStatus(SpriteBatch batch)
        {
            Vector2 textPos = new Vector2(10, 10);
            String text1 = "Particle system status";
            String text2 = " Effects  : " + particleSystem.EffectCount;
            String text3 = " Emitters : " + particleSystem.EmitterCount;
            String text4 = " Particles: " + particleSystem.ParticleCount;
            batch.DrawString(StateManager.Font, text1, textPos, Color.Black);
            textPos.Y += StateManager.Font.LineSpacing;
            batch.DrawString(StateManager.Font, text2, textPos, Color.Black);
            textPos.Y += StateManager.Font.LineSpacing;
            batch.DrawString(StateManager.Font, text3, textPos, Color.Black);
            textPos.Y += StateManager.Font.LineSpacing;
            batch.DrawString(StateManager.Font, text4, textPos, Color.Black);
            
        }
    }
}
