using Codesmith.SmithNgine.Particles;
using Codesmith.SmithShooter.Gameplay;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Codesmith.SmithShooter.Entities
{
    public class PlayerShip : Ship
    {
        public PlayerShip(Texture2D texture, Rectangle bounds, World world, Body body)
            : base(texture, bounds, world, body)
        {
            Body.UserData = this;
        }

        public override bool UpdateEntity(GameTime time)
        {
            return base.UpdateEntity(time);
        }

        public override bool Accelerate(float amount)
        {
 	         return base.Accelerate(amount);
        }

        public override bool Deaccelerate(float amount)
        {
            return base.Deaccelerate(amount);
        }

        public override bool CollideWith(Entity other)
        {
            EnemyShip enemy = other as EnemyShip;
            if (enemy != null)
            {
                return enemy.CollideWith(this);
            }
            return base.CollideWith(other);
        }
    }
}
