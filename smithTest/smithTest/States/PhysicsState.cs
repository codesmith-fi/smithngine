// ***************************************************************************
// ** SmithTest - demo app for smithNgine framework                         **
// **                                                                       **
// ** Copyright (C) 2013 by Erno Pakarinen. All Rights Reserved.            **
// ** Contact: erno@codesmith.fi                                            **
// ***************************************************************************

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Codesmith.SmithNgine.GameState;
using Codesmith.SmithNgine.Input;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Codesmith.SmithNgine.Gfx;

namespace Codesmith.SmithTest
{
    public class PhysicsState : GameState
    {
        Texture2D background;
        World physicsWorld;
        Body physicsGroundBody;
        Body physicsBallBody;
        Body physicsBallBody2;
        Vector2 screenCenter;
        Sprite groundSprite;
        Sprite ballSprite;
        Sprite ballSprite2;
        private const float MeterInPixels = 64.0f;

        public PhysicsState(String name)
            : base(name)
        {
            this.EnterStateInterval = TimeSpan.FromSeconds(1.0f);
            this.ExitStateInterval = TimeSpan.FromSeconds(1.0f);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            background = StateManager.Content.Load<Texture2D>("Images/snowmountain");
            groundSprite = new Sprite(StateManager.Content.Load<Texture2D>("Images/ground"));
            groundSprite.Scale = 1.0f;
            AddComponent(groundSprite);
            ballSprite = new Sprite(StateManager.Content.Load<Texture2D>("Images/ball"));
            ballSprite.Scale = 1.0f;
            ballSprite.InputEventSource = StateManager.Input;
            ballSprite.BeingDragged += sprite_BeingDragged;
            ballSprite.LostDrag += ballSprite_LostDrag;
            AddComponent(ballSprite);
            ballSprite2 = new Sprite(StateManager.Content.Load<Texture2D>("Images/ball"));
            ballSprite2.Scale = 0.5f;
            ballSprite2.InputEventSource = StateManager.Input;
            ballSprite2.BeingDragged += sprite_BeingDragged;
            ballSprite2.LostDrag += ballSprite_LostDrag;
            AddComponent(ballSprite2);

            screenCenter = new Vector2(
                StateManager.GraphicsDevice.Viewport.Width / 2.00f,
                StateManager.GraphicsDevice.Viewport.Height / 2.0f);

            physicsWorld = new World(new Vector2(0, 9.82f));

            /* Circle */
            // Convert screen center from pixels to meters
            Vector2 circlePosition = (screenCenter / MeterInPixels) + new Vector2(0, -1.5f);

            // Create the circle fixture
            physicsBallBody = BodyFactory.CreateCircle(physicsWorld, 96f / (2f * MeterInPixels), 1f, circlePosition);
            physicsBallBody.BodyType = BodyType.Dynamic;
            physicsBallBody.Restitution = 0.3f;
            physicsBallBody.Friction = 0.5f;

            physicsBallBody2 = BodyFactory.CreateCircle(physicsWorld, 48f / (2f * MeterInPixels), 1f, circlePosition + new Vector2(0.5f,-1.5f));
            physicsBallBody2.BodyType = BodyType.Dynamic;
            physicsBallBody2.Restitution = 0.3f;
            physicsBallBody2.Friction = 0.5f;

            Vector2 groundPosition = (screenCenter / MeterInPixels) + new Vector2(0, 1.25f);

            physicsGroundBody = BodyFactory.CreateRectangle(physicsWorld, 512f / MeterInPixels, 64f / MeterInPixels, 1f, groundPosition);
            physicsGroundBody.IsStatic = true;
            physicsGroundBody.Restitution = 0.3f;
            physicsGroundBody.Friction = 0.5f;

            int w=StateManager.GraphicsDevice.Viewport.Width;
            int h=StateManager.GraphicsDevice.Viewport.Height;

            Body fb = BodyFactory.CreateRectangle(
                physicsWorld, ConvertFromPixels(w), ConvertFromPixels(16), 1f, 
                new Vector2(ConvertFromPixels(w/2),ConvertFromPixels(h-8)) );
            fb.IsStatic = true;
            fb.Restitution = 0.3f;
            fb.Friction = 0.5f;

            Body fl = BodyFactory.CreateRectangle(
                physicsWorld, ConvertFromPixels(16), ConvertFromPixels(h), 1f,
                new Vector2(ConvertFromPixels(8), ConvertFromPixels(h/2)));
            fl.IsStatic = true;
            fl.Restitution = 0.3f;
            fl.Friction = 0.5f;

            Body fr = BodyFactory.CreateRectangle(
                physicsWorld, ConvertFromPixels(16), ConvertFromPixels(h), 1f,
                new Vector2(ConvertFromPixels(w-8), ConvertFromPixels(h / 2)));
            fr.IsStatic = true;
            fr.Restitution = 0.3f;
            fr.Friction = 0.5f;

        }

        private float ConvertFromPixels(float value)
        {
            return value / MeterInPixels;
        }

        void ballSprite_LostDrag(object sender, DragEventArgs e)
        {
            if (sender == ballSprite)
            {
                this.physicsBallBody.Awake = true;
                this.physicsBallBody.ApplyLinearImpulse(e.PositionDelta / 16);
            }
            if (sender == ballSprite2)
            {
                this.physicsBallBody2.Awake = true;
                this.physicsBallBody2.ApplyLinearImpulse(e.PositionDelta);
            }
                
        }

        private void sprite_BeingDragged(object sender, DragEventArgs e)
        {
            if (sender == ballSprite)
            {
                this.physicsBallBody.Awake= false;
                this.physicsBallBody.Position += (e.PositionDelta / MeterInPixels);
            }

            if (sender == ballSprite2)
            {
                this.physicsBallBody2.Awake = false;
                this.physicsBallBody2.Position += (e.PositionDelta / MeterInPixels);
            }
        }

        public override void Initialize()
        {
            base.Initialize();            
        }

        public override void EnterState()
        {
            Vector2 circlePosition = (screenCenter / MeterInPixels) + new Vector2(0, -1.5f);
            physicsBallBody.Position = circlePosition;
            physicsBallBody2.Position = circlePosition + new Vector2(0.5f, -1.5f);
            physicsBallBody.LinearVelocity = new Vector2(0f, 0f);
            physicsBallBody2.LinearVelocity = new Vector2(0f, 0f);

            base.EnterState();
        }

        public override void Update(GameTime gameTime)
        {
            physicsWorld.Step((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = StateManager.SpriteBatch;
            Viewport viewport = StateManager.GraphicsDevice.Viewport;
            Vector2 groundPos = physicsGroundBody.Position * MeterInPixels;

            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, viewport.Width, viewport.Height), Color.White);

            //Draw circle
            ballSprite.Position = physicsBallBody.Position * MeterInPixels;
            ballSprite.Rotation = physicsBallBody.Rotation;
            ballSprite.Draw(spriteBatch);

            ballSprite2.Position = physicsBallBody2.Position * MeterInPixels;
            ballSprite2.Rotation = physicsBallBody2.Rotation;
            ballSprite2.Draw(spriteBatch);

            //Draw ground
            groundSprite.Position = groundPos;
            groundSprite.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
