using System.Globalization;

namespace Crud.DDD.Core.Shared.Extensions
{
    public static class DateConverter
    {
        public static bool IsBetween(string time1, string time2, string time3)
        {
            // Convert the Persian dates to Gregorian dates
            DateTime gregorianTime1 = Shamsi2MiladiDate(time1);
            DateTime gregorianTime2 = Shamsi2MiladiDate(time2);
            DateTime gregorianTime3 = Shamsi2MiladiDate(time3); ;

            // Check if time1 is between time2 and time3
            return gregorianTime1 >= gregorianTime2 && gregorianTime1 <= gregorianTime3;
        }

        public static string Miladi2ShamsiString(this DateTime date)
        {
            PersianCalendar calendar = new PersianCalendar();

            var shamsiDate = calendar.GetYear(date) + "/" + (calendar.GetMonth(date).ToString().Length == 1 ? "0" + calendar.GetMonth(date).ToString() : calendar.GetMonth(date)) + "/" + (calendar.GetDayOfMonth(date).ToString().Length == 1 ? "0" + calendar.GetDayOfMonth(date) : calendar.GetDayOfMonth(date));

            return shamsiDate;
        }

        public static DateTime Miladi2ShamsiDate(this DateTime date)
        {
            PersianCalendar calendar = new PersianCalendar();

            int year = calendar.GetYear(date);
            int month = calendar.GetMonth(date);
            int day = calendar.GetDayOfMonth(date);

            return new DateTime(year, month, day, calendar);
        }

        public static string Shamsi2MiladiString(this string date)
        {
            PersianCalendar calendar = new PersianCalendar();

            string[] temp = date.Split('/');

            DateTime miladiDate = calendar.ToDateTime(Convert.ToInt32(temp[0]), Convert.ToInt32(temp[1]), Convert.ToInt32(temp), 0, 0, 0, 0);

            return miladiDate.ToString();
        }

        public static DateTime Shamsi2MiladiDate(this string date)
        {
            PersianCalendar calendar = new PersianCalendar();
            string[] temp = new string[3];

            if (date.Contains('-'))
                temp = date.Split('-');

            if (date.Contains('/'))
                temp = date.Split('/');

            int year = Convert.ToInt32(temp[0]);
            int month = Convert.ToInt32(temp[1]);
            int day = Convert.ToInt32(temp[2]);

            return calendar.ToDateTime(year, month, day, 0, 0, 0, 0);
        }
    }
}
