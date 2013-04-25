/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
using System;
using Microsoft.Xna.Framework;

namespace Codesmith.SmithNgine.MathUtil
{
    /// <summary>
    /// Contains helper methods for interpolations
    /// </summary>
    public static class Interpolations
    {
        /// <summary>
        /// Clamp the given value to range between 0.0f to 1.0f
        /// </summary>
        /// <param name="value">Value to clamp</param>
        /// <returns>0.0f if value is smaller than 0.0f, 1.0f if value is larger than 1.0f</returns>
        public static float ClampZeroOne(float value)
        {
            if (value < 0.0f) 
                return 0.0f;
            else if (value > 1.0f) 
                return 1.0f;
            else 
                return value;
        }

        /// <summary>
        /// Linearly interpolate in a given range
        /// </summary>
        /// <param name="start">Start of the range</param>
        /// <param name="end">End of the range</param>
        /// <param name="amount">Amount to interpolate from 0.0f to 1.0f</param>
        /// <returns></returns>
        public static float LinearInterpolate(float start, float end, float amount)
        {
            amount = Interpolations.ClampZeroOne(amount);
            return start + ((end - start) * amount);
        }

        /// <summary>
        /// Get a random point within a given rectangle
        /// </summary>
        /// <param name="rect">Rectangle</param>
        /// <param name="r">Random generator</param>
        /// <returns>Vector2 with x and y coordinate within the given rectangle</returns>
        public static Vector2 GetRandomRectanglePoint(Rectangle rect, Random r)
        {
            float x = (float)(r.NextDouble() * rect.Width);
            float y = (float)(r.NextDouble() * rect.Height);
            return new Vector2(rect.Left+x, rect.Top+y);
        }

        /// <summary>
        /// Linearly interpolate a range in a given time period. Useful in e.g.
        /// game loop if you have a certain time interval and want to e.g.
        /// fade in or out (transition a value from 0.0 to 1.0 for example).
        /// </summary>
        /// <example>
        /// Transition from 0.0f to 1.0f within two seconds in 12ms increments.
        /// 
        /// float value = 0.0f;
        /// LinearTransition2(TimeSpan.FromMilliseconds(12), TimeSpan.FromSeconds(2),
        ///    1, ref value, 0.0f, 1.0f);
        /// </example>
        /// <param name="elapsed">Elapsed time</param>
        /// <param name="interval">Total time interval of a full transition</param>
        /// <param name="direction">1 is Up (e.g. 0..1), -1 is down (e.g. 1..0)</param>
        /// <param name="transitionValue">The transition value to be updated</param>
        /// <param name="minLimit">Minimum limit of the transition value</param>
        /// <param name="maxLimit">Maximum limit of the transition value</param>
        /// <returns>true if transition is not complete, false if limit reached</returns>
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
            // Still transitioning
            return true;
        }

        /// <summary>
        /// Linearly interpolate between 0.0f and 1.0f in a given time period. 
        /// Useful in e.g.
        /// game loop if you have a certain time interval and want to e.g.
        /// fade in or out (transition a value from 0.0 to 1.0 for example).
        /// </summary>
        /// <example>
        /// Transition from 0.0f to 1.0f within two seconds in 12ms increments.
        /// 
        /// float value = 0.0f;
        /// LinearTransition2(TimeSpan.FromMilliseconds(12), TimeSpan.FromSeconds(2),
        ///    1, ref value, 0.0f, 1.0f);
        /// </example>
        /// <param name="elapsed">Elapsed time</param>
        /// <param name="interval">Total time interval of a full transition</param>
        /// <param name="direction">1 is Up (e.g. 0..1), -1 is down (e.g. 1..0)</param>
        /// <param name="transitionValue">The transition value to be updated</param>
        /// <returns>true if transition is not complete, false if limit reached</returns>
        public static bool LinearTransition(TimeSpan elapsed, TimeSpan interval, 
            int direction, ref float transitionValue)
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
            // Still transitioning
            return true;
        }
    }
}
