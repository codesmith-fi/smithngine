using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Codesmith.SmithNgine.Gfx;
using Codesmith.SmithNgine.MathUtil;
using Codesmith.SmithNgine.GameState;
using Codesmith.SmithNgine.View;
using Codesmith.SmithShooter.Gameplay;

namespace Codesmith.SmithShooter.MainMenu
{
    public class MainMenuState : GameState
    {
        private Camera camera;
        private MenuCanvas menuCanvas;
        private CreditsCanvas creditsCanvas;
        private Background background;
        private GameState playState;
        private float idleAngle = 0.0f;
        private Rectangle idleStateArea;

        public MainMenuState(GameState playState) : base("MainMenu")
        {
            this.EnterStateInterval = TimeSpan.FromSeconds(1.0f);
            this.ExitStateInterval = TimeSpan.FromSeconds(1.0f);
            this.playState = playState;
        }

        public override void Initialize()
        {
            menuCanvas = new MenuCanvas(playState);
            creditsCanvas = new CreditsCanvas();
            creditsCanvas.TransitionSource = this;
            menuCanvas.TransitionSource = this;
            AddCanvas(menuCanvas);
            AddCanvas(creditsCanvas);
            base.Initialize();
        }

        public override void UnloadContent()
        {
            background = null;
            base.UnloadContent();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Viewport v = StateManager.GraphicsDevice.Viewport;
            idleStateArea = new Rectangle(0, 0, v.Width*2, v.Height*2);
            camera = new Camera(Vector2.Zero, v.Bounds);
            background = CreateBackground(10);
        }

        private Background CreateBackground(int layerCount)
        {
            Background background = new Background(camera);

            Sprite stars = new Sprite(StateManager.Content.Load<Texture2D>("Images/nebula1"));
            stars.Color = Color.White * 0.2f;
            stars.Scale = 3.0f;
            background.AddLayer(stars, new Vector2(0.02f, 0.02f));

            List<Texture2D> nebulas = new List<Texture2D>();
            nebulas.Add( StateManager.Content.Load<Texture2D>("Images/nebula1") );
            nebulas.Add( StateManager.Content.Load<Texture2D>("Images/nebula2") );

            Random r = new Random();
            for (int i = 0; i < layerCount; i++)
            {
                int si = r.Next(100) % nebulas.Count;
                Sprite neb = new Sprite(nebulas[si]);
                neb.Position = Interpolations.GetRandomRectanglePoint(idleStateArea, r); 
                neb.Scale = (float)r.NextDouble() + 0.1f;
                neb.Rotation = (float)r.NextDouble() * MathHelper.PiOver2;
                float r2 = (float)r.NextDouble() * neb.Scale;
                background.AddLayer(neb, new Vector2(r2,r2));
            }

            return background;
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            idleAngle += 0.0001f;
            idleAngle = MathHelper.WrapAngle(idleAngle);

            background.Camera.Position = new Vector2(
                (float)( Math.Sin(idleAngle) * idleStateArea.Width),
                (float)(-Math.Cos(idleAngle) * idleStateArea.Height));
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            background.Draw(StateManager.SpriteBatch);
            base.Draw(gameTime);
        }

        public override void Dismiss()
        {
            base.Dismiss();
        }
    }
}
