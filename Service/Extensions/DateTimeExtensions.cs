using System;
using System.Globalization;
namespace Services.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Get persian passed time
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetPersianPastTimeString(this DateTime dateTime)
        {
            int Second = 1;
            int Minute = 60 * Second;
            int Hour = 60 * Minute;
            int Day = 24 * Hour;
            int Month = 30 * Day;

            var timeSpan = new TimeSpan(DateTime.Now.Ticks - dateTime.Ticks);
            var delta = Math.Abs(timeSpan.TotalSeconds);
            if (delta < 1 * Minute)
            {
                return "لحظاتی قبل";
            }
            if (delta < 2 * Minute)
            {
                return "یک دقیقه قبل";
            }
            if (delta < Hour)
            {
                return timeSpan.Minutes + " دقیقه قبل";
            }
            if (delta < 2 * Hour)
            {
                return "یک ساعت قبل";
            }
            if (delta < 24 * Hour)
            {
                return timeSpan.Hours + " ساعت قبل";
            }
            if (delta < 2 * Day)
            {
                return "دیروز";
            }
            if (delta < 30 * Day)
            {
                return timeSpan.Days + " روز پیش";
            }
            if (delta < 12 * Month)
            {
                var months = Convert.ToInt32(Math.Floor((double)timeSpan.Days / 30));
                return months <= 1 ? "یک ماه پیش" : months + " ماه پیش";
            }
            var years = Convert.ToInt32(Math.Floor((double)timeSpan.Days / 365));
            return years <= 1 ? "یک سال پیش" : years + " سال پیش";
        }

        public static int GetPersianWeekOfYear(this DateTime time)
        {
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Saturday);
        }

        /// <summary>
        /// نمایش کامل تاریخ به شمسی
        /// </summary>
        /// <param name="dt">تاریخ</param>
        /// <param name="longstring">نمایش ساعت ؟</param>
        /// <returns></returns>
        public static string ToPersianDateString(this DateTime dateTime, string separator = "/",
            bool includeHourMinute = false, bool includeSecond = false)
        {
            var year = dateTime.Year;
            var month = dateTime.Month;
            var day = dateTime.Day;
            var persianCalendar = new PersianCalendar();
            var pYear = persianCalendar.GetYear(new DateTime(year, month, day, new GregorianCalendar()));
            var pMonth = persianCalendar.GetMonth(new DateTime(year, month, day, new GregorianCalendar()));
            var pDay = persianCalendar.GetDayOfMonth(new DateTime(year, month, day, new GregorianCalendar()));

            var dateTimeString = $"{pYear}{separator}{pMonth.ToString("00", CultureInfo.InvariantCulture)}{separator}{pDay.ToString("00", CultureInfo.InvariantCulture)}";

            if (includeHourMinute)
            {
                var time = $"{dateTime.Hour.ToString("00")}:{dateTime.Minute.ToString("00")}";

                if (includeSecond)
                {
                    time += $":{dateTime.Second.ToString("00")}";
                }

                dateTimeString = dateTimeString + " " + time;
            }

            return dateTimeString;
        }
    }
}
