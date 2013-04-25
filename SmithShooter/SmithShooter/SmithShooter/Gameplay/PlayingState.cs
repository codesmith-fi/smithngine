using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Codesmith.SmithNgine.GameState;
using Codesmith.SmithNgine.Input;
using Codesmith.SmithNgine.View;


namespace Codesmith.SmithShooter.Gameplay
{
    class PlayingState : GameState
    {
        private GameController controller;
        ShooterCanvas playCanvas;
        InfoCanvas infoCanvas;

        public PlayingState()
            : base("Playstate")
        {
            this.EnterStateInterval = TimeSpan.FromSeconds(1.0f);
            this.ExitStateInterval = TimeSpan.FromSeconds(1.0f);
        }

        public override void Initialize()
        {
            playCanvas = new ShooterCanvas();
            playCanvas.TransitionSource = this;
            infoCanvas = new InfoCanvas();

            AddCanvas(playCanvas);
            AddCanvas(infoCanvas);

            StateManager.Input.GamepadConnected += Input_GamepadConnected;
            StateManager.Input.GamepadDisconnected += Input_GamepadDisconnected;
            base.Initialize();            

        }

        public override void UnloadContent()
        {
            controller = null;
            base.UnloadContent();
        }

        public override void LoadContent()
        {
            controller = new GameController(StateManager.Content, 
                new Camera(Vector2.Zero, StateManager.GraphicsDevice.Viewport.Bounds));
            int levelWidth = StateManager.GraphicsDevice.Viewport.Bounds.Width * 10;
            controller.ControllingPlayer = StateManager.Input.GetConnectedGamePad();
            controller.InitializeLevel(new Rectangle(-levelWidth / 2, -levelWidth / 2, levelWidth, levelWidth));

            playCanvas.Controller = controller;
            infoCanvas.Controller = controller;
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            PlayerIndex plr;
            if (StateManager.Input.IsKeyPressed(Keys.End, null, out plr))
            {
                ExitState();
                StateManager.SwitchToState(StateManager.PreviousState);
            }

            controller.HandleInput(StateManager.Input);
            controller.Update(gameTime);
            base.Update(gameTime);
        }

        void Input_GamepadDisconnected(object sender, GamepadEventArgs e)
        {
            if (e.Player.HasValue)
            {
                if (controller.ControllingPlayer.HasValue &&
                    controller.ControllingPlayer.Value == e.Player.Value)
                {
                    controller.ControllingPlayer = null;
                }
            }
        }

        void Input_GamepadConnected(object sender, GamepadEventArgs e)
        {
            controller.ControllingPlayer = e.Player;
        }
        
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Dismiss()
        {
            base.Dismiss();
            if (StateManager.ExitRequested)
            {
                StateManager.Game.Exit();
            }
        }
    }
}
