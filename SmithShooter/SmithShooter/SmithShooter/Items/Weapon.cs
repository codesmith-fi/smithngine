using Codesmith.SmithShooter.Entities;
using Codesmith.SmithShooter.Gameplay;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Codesmith.SmithShooter.Items
{
    public class Weapon : EquippableItem
    {
        private SoundEffect shootSound;
        private Texture2D bulletTexture;
        private float cooldownTimer;
        private Random random;
        private float heat;

        public int BulletsPerSecond
        {
            get;
            set;
        }

        public TimeSpan TimeToNextShot
        {
            get;
            set;
        }

        public TimeSpan TTL
        {
            get;
            set;
        }

        public float DamagePerShot
        {
            get;
            set;
        }

        public float HeatPerShot
        {
            get;
            set;
        }

        public float BulletForce
        {
            get;
            set;
        }

        public float Heat
        {
            get { return heat; }
            set 
            {
                heat = MathHelper.Clamp(value, 0f, MaxHeat*1.2f);
            }
        }

        public bool CanShoot
        {
            get
            {
                if ((TimeToNextShot <= TimeSpan.Zero) && (Heat < MaxHeat))
                {
                    return true;
                }
                return false;
            }
        }

        public float MaxHeat
        {
            get;
            set;
        }

        public float Cooldown
        {
            get;
            set;
        }

        public Weapon(String name, SoundEffect sound, Texture2D bulletTexture) 
            : base(name)
        {
            random = new Random();
            shootSound = sound;
            this.bulletTexture = bulletTexture;
            MaxHeat = 1.0f;
            HeatPerShot = 0.05f;
            Heat = 0f;
            TimeToNextShot = TimeSpan.Zero;
            BulletsPerSecond = 5;
            BulletForce = 20f;
            DamagePerShot = WorldConversions.BulletBaseDamage;
            TTL = TimeSpan.FromSeconds(2.0f);
            Cooldown = 0.08f;
            cooldownTimer = 0f;
        }

        public override bool UpdateItem(GameTime time)
        {
            if (TimeToNextShot > TimeSpan.Zero)
            {
                TimeToNextShot -= time.ElapsedGameTime;
            }

            if (Heat > 0)
            {
                cooldownTimer += time.ElapsedGameTime.Milliseconds;
                float delta = time.ElapsedGameTime.Milliseconds / 1000f;
                if (cooldownTimer >= 1000) cooldownTimer = 0;
                Heat -= delta * Cooldown;
            }
            return true;
        }

        public virtual Bullet Shoot()
        {
            if (CanShoot)
            {
                Heat += HeatPerShot;
                TimeToNextShot = TimeSpan.FromSeconds(1.0f / BulletsPerSecond);
                shootSound.Play(0.50f, MathHelper.Clamp(Heat, 0f, 1f), 0.0f);

                Bullet bullet = new Bullet(bulletTexture, Owner.LevelBounds, Owner.World, TTL, Owner);
                bullet.Body.IgnoreCollisionWith(Owner.Body);
                Owner.Body.IgnoreCollisionWith(bullet.Body);

                bullet.BodyPosition = Owner.Body.Position;
                bullet.Rotation = Owner.Body.Rotation;
                bullet.Scale = 0.2f + (float)random.NextDouble() * 0.8f;
                bullet.Color = Color.Lerp(Color.DarkRed, Color.Red, (float)random.NextDouble());
                bullet.Damage = DamagePerShot;
                // We don't want this bullet to report collision with the source ship

                Vector2 directionVector = Owner.Body.GetWorldVector(new Vector2(0, -1));
                
                Vector2 forceVector = directionVector * BulletForce;
                bullet.Body.ApplyLinearImpulse(forceVector);
                return bullet;
            }
            else
            {
                return null;
            }
        }
    }
}
