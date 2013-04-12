using System;

using Microsoft.Xna.Framework;

namespace Codesmith.SmithNgine.MathUtil
{
    public static class TransitionMath
    {
        public static Vector2 GetRandomRectanglePoint(Rectangle rect, Random r)
        {
            float x = (float)(r.NextDouble() * rect.Width) - rect.Width / 2;
            float y = (float)(r.NextDouble() * rect.Height) - rect.Height / 2;
            return new Vector2(x, y);
        }

        public static bool LinearTransition2(TimeSpan elapsed, TimeSpan interval, int direction, 
            ref float transitionValue, float minLimit, float maxLimit )
        {
            direction = (direction > 0) ? 1 : -1;
            double elapsedMs = elapsed.TotalMilliseconds;
            double transitionMs = interval.TotalMilliseconds;

            float delta = (float)(elapsedMs / transitionMs);
            transitionValue += delta * direction;

            // Ensure that the transition value is kept in: 0.0f <= transitionValue <= 1.0f
            if (((direction > 0) && (transitionValue > maxLimit)) ||
                ((direction < 0) && (transitionValue < minLimit)))
            {
                // We are done transitioning in or out, clamp the value and end transition
                transitionValue = MathHelper.Clamp(transitionValue, minLimit, maxLimit);
                return false;
            }
            // Still animating
            return true;
        }

        public static bool LinearTransition(TimeSpan elapsed, TimeSpan interval, int direction, ref float transitionValue)
        {
            direction = (direction > 0) ? 1 : -1;
            double elapsedMs = elapsed.TotalMilliseconds;
            double transitionMs = interval.TotalMilliseconds;

            float delta = (float)(elapsedMs / transitionMs);
            transitionValue += delta * direction;

            // Ensure that the transition value is kept in: 0.0f <= transitionValue <= 1.0f
            if (((direction > 0) && (transitionValue > 1.0f)) ||
                ((direction < 0) && (transitionValue < 0.0f)))
            {
                // We are done transitioning in or out, clamp the value and end transition
                transitionValue = MathHelper.Clamp(transitionValue, 0.0f, 1.0f);
                return false;
            }
            // Still animating
            return true;
        }

        public static float SmoothTransition(float[] p, float[] c, float time)
        {
            float res = 1.0f;
            if (time >= 0.0f && time < c[1])
            {
                float amount = (1.0f / ( c[1] - c[0] )) * time;
                res = MathHelper.SmoothStep(p[0], p[1], amount);
            }
            else if (time >= c[1] && time < c[2])
            {
                float amount = (1.0f / ( c[2] - c[1] )) * ( time - c[1]);
                res = MathHelper.SmoothStep(p[1], p[2], amount);
            }
            if (time >= c[2] && time < c[3])
            {
                float amount = (1.0f / (c[3] - c[2])) * (time - c[2]);
                res = MathHelper.SmoothStep(p[2], p[3], amount);
            }
            return res;
        }
    }
}
