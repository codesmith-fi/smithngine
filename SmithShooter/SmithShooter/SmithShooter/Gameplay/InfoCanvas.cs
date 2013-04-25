using Codesmith.SmithNgine.GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codesmith.SmithShooter.Gameplay
{
    public class InfoCanvas : GameCanvas
    {
        public GameController Controller
        {
            get;
            set;
        }

        public InfoCanvas()
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = StateManager.SpriteBatch;
            Vector2 textPos = new Vector2(10, 10);

            string text = "Gametime: " + gameTime.TotalGameTime.TotalSeconds;
            string coords = "Player @" + Controller.CurrentLevel.Player.Position.X + ":" + Controller.CurrentLevel.Player.Position.Y;
            string speed = String.Format("Player velocity = {0},{1}", Controller.CurrentLevel.Player.Body.LinearVelocity.X, Controller.CurrentLevel.Player.Body.LinearVelocity.Y);
            string torque = String.Format("Player angular velocity = {0}", Controller.CurrentLevel.Player.Body.AngularVelocity);
            string heat = "Heat: " + Controller.CurrentLevel.Player.Weapon.Heat;
            base.Draw(gameTime);
            spriteBatch.Begin();
/*
            spriteBatch.DrawString(StateManager.Font, text, textPos, Color.White);
            textPos.Y += StateManager.Font.LineSpacing;
            spriteBatch.DrawString(StateManager.Font, coords, textPos, Color.White);
            textPos.Y += StateManager.Font.LineSpacing;
            spriteBatch.DrawString(StateManager.Font, speed, textPos, Color.White);
            textPos.Y += StateManager.Font.LineSpacing;
            spriteBatch.DrawString(StateManager.Font, torque, textPos, Color.White);
            textPos.Y += StateManager.Font.LineSpacing;
            spriteBatch.DrawString(StateManager.Font, heat, textPos, Color.White);
*/
            string enemySpeed = String.Format("Velocity: {0},{1}", Controller.CurrentEnemy.Body.LinearVelocity.X, Controller.CurrentEnemy.Body.LinearVelocity.Y);
            string enemyRotation = String.Format("Rotation: {0}", Controller.CurrentEnemy.Body.Rotation);
            string enemyRotationSpeed = String.Format("Angular Vel: {0}", Controller.CurrentEnemy.Body.AngularVelocity);
            string ai1 = String.Format("AI Action: {0}", Controller.CurrentEnemy.AI.ActionAmount);
            spriteBatch.DrawString(StateManager.Font, enemySpeed, textPos, Color.Red);
            textPos.Y += StateManager.Font.LineSpacing;
            spriteBatch.DrawString(StateManager.Font, enemyRotation, textPos, Color.Red);
            textPos.Y += StateManager.Font.LineSpacing;
            spriteBatch.DrawString(StateManager.Font, enemyRotationSpeed, textPos, Color.Red);
            textPos.Y += StateManager.Font.LineSpacing;
            spriteBatch.DrawString(StateManager.Font, ai1, textPos, Color.Red);
            spriteBatch.End();

        }
    }
}
