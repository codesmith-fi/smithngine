﻿// ***************************************************************************
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
    public class MenuCanvas : GameCanvas
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
        ParticleEmitter emitter;
        ParticleEffect particleEffect;

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
            particleEffect = new ParticleEffect();
            particleEffect.GravityVector = new Vector2(0.0f, 0.04f);

//            emitter = new ConeEmitter(animSprite.Position, MathHelper.ToRadians(90), MathHelper.ToRadians(90));
            emitter = new PointEmitter(animSprite.Position);
            ParticleGenerationParams emitterparams1 = new ParticleGenerationParams();
            emitterparams1.AddTexture(StateManager.Content.Load<Texture2D>("Images/flower"));
            emitterparams1.AddTexture(StateManager.Content.Load<Texture2D>("Images/circle"));
            emitterparams1.QuantityRange = new Vector2(10, 10);
            emitterparams1.ScaleRange = new Vector2(0.1f, 1.0f);
            emitterparams1.OpacityRange = new Vector2(0.1f, 1.0f);
            emitterparams1.SpeedRange = new Vector2(0.1f, 2.0f);
            emitterparams1.ScaleDamping = 1.01f;
            emitterparams1.Color = Color.Green;
            emitterparams1.SpeedDamping = 0.99f;
            emitterparams1.TTLRange = new Vector2(500.0f, 1500.0f);
            emitter.Configuration = emitterparams1;
            emitter.AutoGenerate = false;
            particleEffect.AddEmitter(emitter);

            ParticleEmitter lineEmitter = new LineEmitter(Vector2.Zero, new Vector2(Bounds.Width, 0f));
            ParticleGenerationParams emitterparams2 = new ParticleGenerationParams();
            emitterparams2.AddTexture(StateManager.Content.Load<Texture2D>("Images/flower"));
            emitterparams2.QuantityRange = new Vector2(10, 10);
            emitterparams2.SpeedRange = new Vector2(0.1f, 1.0f);
            emitterparams2.ScaleRange = new Vector2(0.1f, 1.0f);
            emitterparams2.OpacityRange = new Vector2(0.1f, 1.0f);
            emitterparams2.TTLRange = new Vector2(1500.0f, 2000.0f);
            lineEmitter.Configuration = emitterparams2;
            particleEffect.AddEmitter(lineEmitter);

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
            emitter.Position = new Vector2(StateManager.Input.MouseX, StateManager.Input.MouseY);
            if (StateManager.Input.IsMouseButtonHeld(SmithNgine.Input.MouseButton.Right))
            {
                particleEffect.AddParticles( emitter.Generate(10) );
                emitter.Rotation += 0.1f;
//                particleEffect.Generate(TimeSpan.FromSeconds(0.5f));
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
                m.Draw(spriteBatch);
            }
            animSprite.Draw(spriteBatch);

            ShowParticleStatus(spriteBatch);

            spriteBatch.End();

            particleSystem.Draw(spriteBatch);
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
