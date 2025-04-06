using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeConversion
{
    /// <summary>
    /// Provides utility methods for converting time between hours, minutes, and seconds.
    /// </summary>
    public static class TimeConverter
    {
        /// <summary>
        /// Converts the given time to float seconds.
        /// </summary>
        /// <param name="hours">The number of hours.</param>
        /// <param name="minutes">The number of minutes.</param>
        /// <param name="seconds">The number of seconds.</param>
        /// <param name="floor">By default true, the result is floored to the nearest whole number.</param>
        /// <returns>The total time in seconds.</returns>
        public static float ConvertToSeconds(float hours, float minutes, float seconds, bool floor = true)
        {
            float calculation = hours * 3600 + minutes * 60 + seconds;
            if (floor) return Mathf.Floor(calculation);
            return calculation;
        }

        /// <summary>
        /// Converts the given time to float minutes.
        /// </summary>
        /// <param name="hours">The number of hours.</param>
        /// <param name="minutes">The number of minutes.</param>
        /// <param name="seconds">The number of seconds.</param>
        /// <param name="floor">By default true, the result is floored to the nearest whole number.</param>
        /// <returns>The total time in minutes.</returns>
        public static float ConvertToMinutes(float hours, float minutes, float seconds, bool floor = true)
        {
            float calculation = hours * 60 + minutes + seconds / 60;
            if (floor) return Mathf.Floor(calculation);
            return calculation;
        }

        /// <summary>
        /// Converts the given time to float hours.
        /// </summary>
        /// <param name="hours">The number of hours.</param>
        /// <param name="minutes">The number of minutes.</param>
        /// <param name="seconds">The number of seconds.</param>
        /// <param name="floor">By default true, the result is floored to the nearest whole number.</param>
        /// <returns>The total time in hours.</returns>
        public static float ConvertToHours(float hours, float minutes, float seconds, bool floor = true)
        {
            float calculation = hours + minutes / 60 + seconds / 3600;
            if (floor) return Mathf.Floor(calculation);
            return calculation;
        }
    }
}