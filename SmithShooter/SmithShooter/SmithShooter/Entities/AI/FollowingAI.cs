using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Codesmith.SmithNgine.General;
using Codesmith.SmithShooter.Gameplay;

namespace Codesmith.SmithShooter.Entities.AI
{
    public class FollowingAI : AIBase
    {
        enum AIState
        {
            Idle,
            Roam
        }

        private AIState state;
        private EventTrigger decisionTrigger;
        private EventTrigger actionTrigger;
        private float angleDifference;
        private Entity lastTarget;

        public FollowingAI()
        {
            decisionTrigger = new EventTrigger(TimeSpan.FromSeconds(1.0f), true);
            decisionTrigger.EventTriggered += decisionTrigger_EventTriggered;
            decisionTrigger.Start();

            actionTrigger = new EventTrigger(TimeSpan.FromSeconds(0.2f), true);
            actionTrigger.EventTriggered += actionTrigger_EventTriggered;
            actionTrigger.Start();

            state = AIState.Idle;
        }

        public override void UpdateAI(GameTime gameTime)
        {
            decisionTrigger.Update(gameTime);
            actionTrigger.Update(gameTime);
            if (lastTarget != null)
            {
                Vector2 distanceVector = lastTarget.Body.Position - Owner.Body.Position;
                Vector2 desired_velocity = Vector2.Normalize(distanceVector) * Owner.RearThruster.Power;
                Vector2 steering = desired_velocity - Owner.Body.LinearVelocity;
                steering.Normalize();
                float angleDiff = AngleToTarget(lastTarget.Body.Position, Owner.Body.Rotation);
                //                if (distanceVector.Length() > WorldConversions.ConvertFromPixels(300))
                //                {
                Owner.Steer(angleDiff);
                //                }
                if (Math.Abs(angleDiff) < Math.PI / 5)
                {
                    Owner.Accelerate(steering.Length());
                }
            }
            else
            {

            }


            base.UpdateAI(gameTime);
        }

        private void actionTrigger_EventTriggered(object sender, EventArgs e)
        {
        }

        void decisionTrigger_EventTriggered(object sender, EventArgs e)
        {
            Entity mostWanted = MostHatedEntity();

            if (mostWanted != null)
            {
                lastTarget = mostWanted;
            }
            else 
            {
                lastTarget = null;
                beginRoam();
            }
        }

        private void beginRoam()
        {
            state = AIState.Roam;
            lastTarget = null;
        }

        private float AngleToTarget(Vector2 targetPosition, float currentRotation)
        {
            float x = targetPosition.X - Owner.Body.Position.X;
            float y = targetPosition.Y - Owner.Body.Position.Y;
            float desiredAngle = (float)(Math.Atan2(y, x) + Math.PI/2);
            float difference = MathHelper.WrapAngle(desiredAngle - currentRotation);

            float test = AngleBetween(targetPosition, Owner.Body.Position);

//            difference = MathHelper.Clamp(difference, -0.50f, 0.50f);
//            return MathHelper.WrapAngle(currentRotation + difference);
            return difference;
        }

        private float AngleBetween(Vector2 a, Vector2 b)
        {
            float dot = Vector2.Dot(a,b);
            float angle;
            if (dot == 0.0f)
            {
                angle = (float)Math.PI;
            }
            else
            {
                angle = (float)Math.Acos(dot / (a.Length() * b.Length()));
            }
            return angle;        
        }
    }
}
