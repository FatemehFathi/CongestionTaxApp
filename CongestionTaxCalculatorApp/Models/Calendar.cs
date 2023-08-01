using System;
using System.Collections.Generic;
using CongestionTaxCalculatorApp.Types;

namespace CongestionTaxCalculatorApp.Models
{
    public class Calendar
    {
        public int Id { get; set; }
        public int PublicHolidayIds { get; set; }
        public int WeekendIds { get; set; }
        public List<DateTime> PublicHolidays { get; private set; }
        public List<WeekDayEnum> Weekends { get; private set; }

        public Calendar(List<DateTime> publicHolidays, List<WeekDayEnum> weekends)
        {
            PublicHolidays = publicHolidays;
            Weekends = weekends;
        }

        public bool IsPublicHoliday(DateTime date)
        {
            return PublicHolidays.Contains(date.Date);
        }

        public bool IsBeforePublicHoliday(DateTime date, int duration = 1)
        {
            DateTime targetDate = date.AddDays(-duration);

            // Check if any public holiday falls between the target date and the original date
            foreach (DateTime publicHoliday in PublicHolidays)
            {
                if (publicHoliday > targetDate && publicHoliday <= date)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsWeekend(DateTime date)
        {
            return Weekends.Contains((WeekDayEnum)date.DayOfWeek);
        }
    }
}