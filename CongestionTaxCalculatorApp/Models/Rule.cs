using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculatorApp.Models
{
    public class Rule
    {
        public int Id { get; set; }
        public bool HasTollFreeWeekend { get; private set; }
        public bool HasTollFreeHoliday { get; private set; }
        public int DaysBeforeHolidayTollFree { get; private set; }
        public int TollFreeMonth { get; private set; }
        public int SingleChargeInterval { get; private set; }
        public double MaxChargeAmountPerDay { get; private set; }

        public Rule(bool hasTollFreeWeekend, bool hasTollFreeHoliday, int singleChargeInterval, double maxChargeAmountPerDay, int daysBeforeHolidayTollFree = 0, int tollFreeMonth = 0)
        {
            HasTollFreeWeekend = hasTollFreeWeekend;
            HasTollFreeHoliday = hasTollFreeHoliday;
            DaysBeforeHolidayTollFree = daysBeforeHolidayTollFree;
            TollFreeMonth = tollFreeMonth;
            SingleChargeInterval = singleChargeInterval;
            MaxChargeAmountPerDay = maxChargeAmountPerDay;
        }
    }
}