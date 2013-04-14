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
using Codesmith.SmithNgine.GameState;
using Codesmith.SmithNgine.Gfx;
using Codesmith.SmithNgine.Input;
using Codesmith.SmithNgine.Collision;

namespace Codesmith.SmithTest
{
    class GamingStatusCanvas : GameCanvas
    {
        private CollisionManager collisionManager;
        private List<Sprite> sprites = new List<Sprite>();
        Vector2 textPos;
        Rectangle area;
        Vector2 moveDelta;
        Point mouseLoc;

        public GamingStatusCanvas()
        {
        }

        public override void HandleInput(InputManager input)
        {
            if (input.IsMouseButtonPressed(MouseButton.Left))
            {
                mouseLoc = new Point(input.MouseX, input.MouseY);
            }
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            collisionManager = new CollisionManager();
            AddComponent(collisionManager);

            this.area = StateManager.GraphicsDevice.Viewport.Bounds;
            this.textPos = new Vector2(this.area.Width / 2, 10);
            this.moveDelta = new Vector2(0, 2);
            sprites.Add(new Sprite(StateManager.Content.Load<Texture2D>("Images/j1")));
            sprites.Add(new Sprite(StateManager.Content.Load<Texture2D>("Images/j2")));
            sprites.Add(new Sprite(StateManager.Content.Load<Texture2D>("Images/j3")));
            
            int i = 100;
            float scale = 1.0f;
            foreach (Sprite s in sprites)
            {
                s.InputEventSource = StateManager.Input;
                s.BeingDragged += sprite_BeingDragged;
                AddComponent(s);
                s.TransitionSource = this.State;
                s.Position = new Vector2(i, 80);
                s.Scale = scale;
                this.collisionManager.AddCollidable(s);
                i += 200;
            }
            this.collisionManager.ObjectsCollided += collisionManager_ObjectsCollided;
            base.LoadContent();
        }

        void collisionManager_ObjectsCollided(object sender, CollisionEventArgs e)
        {
        }

        private void sprite_BeingDragged(object sender, DragEventArgs e)
        {
            Sprite sprite = (Sprite)sender;
            sprite.Position += e.PositionDelta;
        }

        public override void Update(GameTime gameTime)
        {
            if (State.Status != GameStateStatus.Running)
            {
                return;
            }

            this.textPos += this.moveDelta;
            if (textPos.Y >= area.Height - 20)
            {
                this.moveDelta.Y = -4;
            }
            if (textPos.Y <= 20)
            {
                this.moveDelta.Y = 4;
            }

            int i=0;
            foreach (Sprite s in sprites)
            {
                s.Rotation += 0.05f;
                s.Rotation = MathHelper.WrapAngle(s.Rotation);
                s.Scale = (MathHelper.Pi + (float)Math.Sin(s.Rotation + i * MathHelper.Pi / 6));
                s.Scale = s.Scale / ( MathHelper.Pi * 2 ) ;
                i++;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = StateManager.SpriteBatch;
            String text = "Hello world from " + this.GetType().ToString();
            Vector2 origin = StateManager.Font.MeasureString(text);
            textPos.X = this.area.Width / 2 - origin.X / 2;
            spriteBatch.Begin();
            spriteBatch.DrawString(StateManager.Font, text, textPos, Color.Green);

            String mouseText = "X:" + mouseLoc.X + " Y:" + mouseLoc.Y;
            if (sprites[1].Bounds.Contains(mouseLoc))
            {
                mouseText += " - " + sprites[1].ToString();
            }
            spriteBatch.DrawString(StateManager.Font, mouseText, new Vector2(10, 10), Color.Red);

            foreach (Sprite sprite in this.sprites)
            {
                sprite.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
