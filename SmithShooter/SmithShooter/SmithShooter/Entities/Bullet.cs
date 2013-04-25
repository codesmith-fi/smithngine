using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codesmith.SmithShooter.Entities
{
    public class Bullet : Entity
    {
        private TimeSpan timeToLive;

        public Bullet(Texture2D texture, Rectangle bounds, World world, TimeSpan ttl, Entity owner)
            : base(texture, bounds, world)
        {
            timeToLive = ttl;
            Owner = owner;
            Body.Mass = 2.0f;
            Body.IsBullet = true;
            Body.IsSensor = true;
            Body.IgnoreCCD = false;
        }

        public override bool UpdateEntity(GameTime time)
        {
            timeToLive -= time.ElapsedGameTime;
            base.UpdateEntity(time);
            return (timeToLive <= TimeSpan.Zero) ? false : true;
        }

        public override bool CollideWith(Entity other)
        {
            Ship ship = other as Ship;
            if (ship != null)
            {
                return ship.CollideWith(this);
            }
            return base.CollideWith(other);
        }
    }
}
