using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonUtils
{
    public class Time
    {
        public static int CurrentEpoch()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            return (int)t.TotalSeconds;
        }
        public static int DateTimeToEpoch(DateTime dateTime,bool isUtc = true)
        {
            if (!isUtc)
            {
                dateTime = dateTime.ToUniversalTime();
            }
            TimeSpan t = dateTime - new DateTime(1970, 1, 1);
            return (int)t.TotalSeconds;
        }

        public static int StringDateTimeToEpoch(string dateTime)
        {
            DateTime dt = DateTime.Parse(dateTime);
            return DateTimeToEpoch(dt);
        }

        public static int CurrentEpoch(int offset)
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            return (int)t.TotalSeconds + offset;
        }
        public static DateTime EpochToLocalDateTime(long epoch)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return dt.AddSeconds(epoch).ToLocalTime();
        }

        public static string FormatShortTimeStamp(DateTime dateTime)
        {
            TimeSpan timeSpan = dateTime.Date.Subtract(DateTime.Now.Date);
            if ((int)timeSpan.TotalDays == 0)
            {
                return dateTime.ToString("HH:mm");
            }
            else if ((int)timeSpan.TotalDays == 1)
            {
                return dateTime.ToString("HH:mm");
            }
            else if ((int)timeSpan.TotalDays > 6)
            {
                return dateTime.ToString("d. MMMM");
            }
            return dateTime.DayOfWeek.ToString() + ", " + dateTime.ToString("HH:mm");
        }
    }

}
