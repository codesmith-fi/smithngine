using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Codesmith.SmithNgine.GameState;

namespace Codesmith.SmithShooter.Gameplay
{
    public class ShooterCanvas : GameCanvas
    {
        private GameController gameController;

        public GameController Controller
        {
            get { return gameController; }
            set { gameController = value; }
        }

        public ShooterCanvas()
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Rectangle bounds = StateManager.GraphicsDevice.Viewport.Bounds;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = StateManager.SpriteBatch;
            gameController.Layers.Draw(spriteBatch);
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
