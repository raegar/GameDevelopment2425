using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeConversion
{
    public static class TimeConverter
    {
        public static int ConvertToSeconds(int hours, int minutes, int seconds)
        {
            return hours * 3600 + minutes * 60 + seconds;
        }

        public static int ConvertToMinutes(int hours, int minutes, int seconds)
        {
            return hours * 60 + minutes + seconds / 60;
        }

        public static int ConvertToHours(int hours, int minutes, int seconds)
        {
            return hours + minutes / 60 + seconds / 3600;
        }
    }
}
    
