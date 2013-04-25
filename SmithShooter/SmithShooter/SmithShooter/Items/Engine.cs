using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Codesmith.SmithShooter.Gameplay;
using Codesmith.SmithNgine.Particles;
using Codesmith.SmithShooter.Entities;
using System.Diagnostics;

namespace Codesmith.SmithShooter.Items
{
    [Flags]
    public enum EngineFeatures : int
    {
        None = 0
    }

    public enum EngineDirection : int
    {
        Rear = 0,
        RearLeft = 1,
        Left = 2,
        FrontLeft = 3,
        Front = 4,
        FrontRight = 5,
        Right = 6,
        RearRight = 7
    }

    public class Engine : EquippableItem
    {
        private float thrustPower;
        private float directionAngle;
        private EngineDirection direction;

        public ParticleEffect Effect { get; set; }
        public float Power
        {
            get { return thrustPower; }
            set { thrustPower = value; }
        }

        public EngineDirection ThrustDirection
        {
            get { return direction; }
            set 
            { 
                direction = value;
                directionAngle = (float)((float)((int)direction * Math.PI / 4));
            }
        }

        public EngineFeatures Features
        {
            get;
            set;
        }

        public Engine(String name, EngineDirection direction = EngineDirection.Rear)
            : base(name, null)
        {
            Features = EngineFeatures.None;
            ThrustDirection = direction;
            Power = WorldConversions.BaseEnginePower;
            int i = (int)direction;
        }

        /// <summary>
        /// Generate force
        /// </summary>
        /// <param name="amount">1.0 = full thrust, 0.0 = minimum thrust (none)</param>
        /// <returns>Force vector</returns>
        public Vector2 Thrust(float amount = 1.0f)
        {
            Debug.Assert(Owner != null, "Owner entity did not set Engine.Owner!");

            Vector2 force = new Vector2(
                (float)Math.Sin(directionAngle + Owner.Body.Rotation) * thrustPower * amount,
                (float)-Math.Cos(directionAngle + Owner.Body.Rotation) * thrustPower * amount);
            return force;
        }

        public override bool UpdateItem(GameTime gameTime)
        {
            return base.UpdateItem(gameTime);
        }
    }
}
