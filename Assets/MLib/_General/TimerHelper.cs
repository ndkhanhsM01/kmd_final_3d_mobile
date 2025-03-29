

using System;

namespace MLib
{
    public static class TimeHelper
    {
        public static DateTime beginEpoch = new DateTime(1970, 1, 1, 0, 0, 0);
        public static double UnixTimeNow => DateTimeToUnixTimeStamp(DateTime.Now);
        public static double DateTimeToUnixTimeStamp(DateTime dateTime)
        {
            var timeSpan = (dateTime - beginEpoch);
            return (double)timeSpan.TotalSeconds;
        }
        public static DateTime UnixTimeStampToDateTime(double unix)
        {
            return beginEpoch.AddSeconds(unix);
        }
    }
}
