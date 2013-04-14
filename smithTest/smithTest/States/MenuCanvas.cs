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
        List<MenuEntry> menuEntries = new List<MenuEntry>();
        MenuEntry exitMenuEntry;
        MenuEntry playMenuEntry;
        MenuEntry physicsMenuEntry;
        MenuEntry optionsMenuEntry;
        GameState playState;
        GameState physicsState;
        ParticleSystem particleSystem;
        ParticleEmitter smokeEmitter1;
        ParticleEmitter smokeEmitter2;
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
            particleEffect.Rotation = 0f;
            particleEffect.Position = Vector2.Zero;
//            particleEffect.GravityVector = new Vector2(0.0f, 0.04f);

            Circle circle = new Circle(100.0f, Vector2.Zero);
            smokeEmitter1 = new PointEmitter(Vector2.Zero);
            smokeEmitter1 = new CircleEmitter(circle);
            smokeEmitter2 = new PointEmitter(Vector2.Zero);
            //            emitter = new PointEmitter(animSprite.Position);
            ParticleGenerationParams smokeEmitterParams1 = new ParticleGenerationParams();
            ParticleGenerationParams smokeEmitterParams2 = new ParticleGenerationParams();

            smokeEmitterParams1.AddTexture(StateManager.Content.Load<Texture2D>("Images/smoke1"));
            smokeEmitterParams2.AddTexture(StateManager.Content.Load<Texture2D>("Images/smoke2"));
            smokeEmitterParams2.AddTexture(StateManager.Content.Load<Texture2D>("Images/smoke3"));
            smokeEmitterParams1.QuantityRange = new Vector2(10, 100);
            smokeEmitterParams1.Flags = EmitterCastStyle.None;
            smokeEmitterParams1.ScaleRange = new Vector2(0.1f, 1.1f);
            smokeEmitterParams1.DepthRange = new Vector2(0.0f, 1.0f);
            smokeEmitterParams1.OpacityRange = new Vector2(0.6f, 0f);
            smokeEmitterParams1.InitialSpeedRange = new Vector2(0.1f, 2.0f);
            smokeEmitterParams1.SpeedDamping = 0.99f;
            smokeEmitterParams1.RotationRange = new Vector2(-1.0f, 1.0f);
            smokeEmitterParams1.InitialRotationVariation = 1.0f;
            smokeEmitterParams1.Color = Color.White;
            smokeEmitterParams1.TTLRange = new Vector2(500.0f, 4000.0f);
            smokeEmitterParams2.QuantityRange = new Vector2(5, 100);
            smokeEmitterParams2.ScaleRange = new Vector2(2.0f, 0.2f);
            smokeEmitterParams2.InitialScaleVariation = 0.2f;
            smokeEmitterParams2.DepthRange = new Vector2(0.0f, 1.0f);
            smokeEmitterParams2.OpacityRange = new Vector2(0f, 0.5f);
            smokeEmitterParams2.InitialSpeedRange = new Vector2(0.1f, 2.0f);
            smokeEmitterParams2.RotationRange = new Vector2(0.0f, -4f);
            smokeEmitterParams1.InitialRotationVariation = 1.0f;
            smokeEmitterParams2.Color = Color.White;
            smokeEmitterParams2.TTLRange = new Vector2(500.0f, 5000.0f);
            smokeEmitterParams2.SpeedDamping = 0.99f;

            smokeEmitter1.Configuration = smokeEmitterParams1;
            smokeEmitter1.AutoGenerate = false;
            smokeEmitter2.Configuration = smokeEmitterParams2;
            smokeEmitter2.AutoGenerate = false;
            particleEffect.AddEmitter(smokeEmitter1);
            particleEffect.AddEmitter(smokeEmitter2);
            /*
                        ParticleEmitter lineEmitter = new LineEmitter(Vector2.Zero, new Vector2(Bounds.Width, 0f));
                        ParticleGenerationParams emitterparams2 = new ParticleGenerationParams();
                        emitterparams2.AddTexture(StateManager.Content.Load<Texture2D>("Images/flower"));
                        emitterparams2.QuantityRange = new Vector2(10, 10);
                        emitterparams2.SpeedRange = new Vector2(0.1f, 1.0f);
                        emitterparams2.ScaleRange = new Vector2(2.0f, 0.5f);
                        emitterparams2.OpacityRange = new Vector2(1.0f, 0.0f);
                        emitterparams2.TTLRange = new Vector2(1500.0f, 2000.0f);
                        lineEmitter.Configuration = emitterparams2;
                        particleEffect.AddEmitter(lineEmitter);
            */
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
//                smokeEmitter1.Rotation += 0.1f;
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
            //animSprite.Draw(spriteBatch);

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
