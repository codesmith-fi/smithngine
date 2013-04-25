using Codesmith.SmithShooter.Gameplay;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codesmith.SmithShooter.Items;
using Codesmith.SmithNgine.Particles;
using Codesmith.SmithShooter.Entities.AI;

namespace Codesmith.SmithShooter.Entities
{
    public abstract class Ship : Entity
    {
        private Engine rearThruster;
        private Engine frontThruster;
        private Engine leftThruster;
        private Engine rightThruster;
        private Weapon weapon;
        private AIBase myIntelligence;

        public AIBase AI
        {
            get { return myIntelligence; }
            set
            {
                myIntelligence = value;
                myIntelligence.Owner = this;
            }
        }

        public Weapon Weapon 
        { 
            get { return weapon; } 
            set 
            {
                weapon = value;
                weapon.Owner = this;
            }
        }
        public float AccelerationForce { get; set; }
        // Engines
        public Engine RearThruster
        {
            get { return rearThruster; }
            set
            {
                rearThruster = value;
                rearThruster.Owner = this;
            }
        }

        public Engine FrontThruster
        {
            get { return frontThruster; }
            set
            {
                frontThruster = value;
                frontThruster.Owner = this;
            }
        }

        public Engine LeftSideThruster
        {
            get { return rearThruster; }
            set
            {
                leftThruster = value;
                leftThruster.Owner = this;
            }
        }

        public Engine RightSideThruster
        {
            get { return rightThruster; }
            set
            {
                rightThruster = value;
                rightThruster.Owner = this;
            }
        }


        public Ship(Texture2D texture, Rectangle bounds, World world, Body body)
            : base(texture, bounds, world, body, WorldConversions.ShipDensity)
        {
            AccelerationForce = WorldConversions.BaseEnginePower;
            MaxLinearVelocity = 6.0f;
        }

        public virtual void Stop()
        {
            Body.ResetDynamics();
        }

        /// <summary>
        /// Accelerate the ship
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>false if max speed limit achieved</returns>
        public virtual bool Accelerate(float amount = 1.0f)
        {
            amount = MathHelper.Clamp(amount, 0.0f, 1.0f);
            bool result = false;
            if (RearThruster != null)
            {
                result = Thrust(RearThruster, amount);
                if (RearThruster.Effect != null)
                {
                    Vector2 posLocal = Body.GetWorldPoint(
                        new Vector2(0, WorldConversions.ConvertFromPixels(Texture.Height / 2)));
                    RearThruster.Effect.Position = WorldConversions.ConvertFromWorld(posLocal);
                    RearThruster.Effect.Rotation = Rotation + (float)Math.PI;
                    if (amount != 0)
                    {
                        RearThruster.Effect.Generate(10);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Deaccelerate the ship
        /// </summary>
        /// <param name="amount">1.0 is maximun engine thrust, 0.0 is minimum</param>
        /// <returns>false if max speed limit achieved</returns>
        public virtual bool Deaccelerate(float amount = 1.0f)
        {
            amount = MathHelper.Clamp(amount, -1.0f, 1.0f);
            bool result = false;
            if (FrontThruster != null)
            {
                result = Thrust(FrontThruster, amount);
                if (FrontThruster.Effect != null)
                {
                    Vector2 posLocal = Body.GetWorldPoint(
                        new Vector2(0, -WorldConversions.ConvertFromPixels(Texture.Height / 2)));
                    FrontThruster.Effect.Position = WorldConversions.ConvertFromWorld(posLocal);
                    FrontThruster.Effect.Rotation = Rotation;
                    if (amount != 0)
                    {
                        FrontThruster.Effect.Generate(10);
                    }
                }
            }
            return result;
        }

        public virtual void Steer(float amount)
        {
            if (entityBody.AngularVelocity < MaxAngularVelocity)
            {
                entityBody.ApplyTorque(TorqueForce * amount);
            } 
        }

        public virtual Bullet Shoot()
        {
            Bullet bullet = Weapon.Shoot();
            return bullet;
        }

        public override bool UpdateEntity(GameTime time)
        {
            if (Weapon != null)
            {
                Weapon.UpdateItem(time);
            }

            return base.UpdateEntity(time);
        }

        public override bool CollideWith(Entity other)
        {
            Bullet bullet = other as Bullet;
            if (bullet != null)
            {
                // Bullet does not collide with the owner
                if (bullet.Owner == this)
                {
                    return false;
                }
                TakeDamage(bullet.Damage);
                if (this.AI != null)
                {
                    this.AI.Hate(bullet.Owner, 10f);
                }
                return true;
            }

            // Ship always collides with a Planet
            Planet planet = other as Planet;
            if (planet != null)
            {
                this.TakeDamage(planet.Damage);
                return true;
            }

            return base.CollideWith(other);
        }

        /// <summary>
        /// Thrust with the given engine, causes force in the direction of the engine
        /// </summary>
        /// <param name="engine"></param>
        protected virtual bool Thrust(Engine engine, float amount)
        {
            if (engine != null)
            {
                Vector2 force = engine.Thrust(amount);
                if (Velocity < MaxLinearVelocity)
                {
                    entityBody.LinearDamping = 0.0f;
                    entityBody.ApplyForce(force);
                }
                else
                {
                    entityBody.LinearDamping = 0.1f;
                }
                return true;
            }
            return false;
        }
    }
}
