using CongestionTaxCalculatorApp.Models;
using CongestionTaxCalculatorApp.Types;
using System;
using System.Collections.Generic;

namespace CongestionTaxCalculatorApp.Services
{
    public class CongestionTaxCalculator
    {
        public CongestionTaxCalculator() { }

        public double CalculateCongestionTax(City city, Vehicle vehicle)
        {
            // Check if the vehicle is toll-free
            if (IsTollFreeVehicle(city, vehicle)) return 0;

            var tollDates = vehicle.TollDates.OrderBy(date => date).ToList();

            double totalTax = 0;

            double intervalHighest = 0;
            DateTime? intervalStart = null;

            double dayTax = 0;
            DateTime? prevDate = null;

            // First toll date
            intervalHighest = city.GetApplicableTariffAmount(tollDates[0]);
            intervalStart = tollDates[0];

            foreach (DateTime tollDate in tollDates)
            {
                // Check if the tollDate is toll-free
                if (IsTollFreeDate(tollDate, city)) continue;

                // Get tariff amount if there is an applicable tariff for the tollDate
                double applicableTax = city.GetApplicableTariffAmount(tollDate);

                // Update dayTax
                // Check for SingleChargeInterval
                bool isWithinInterval = IsSingleCharge(city.CityRule.SingleChargeInterval, tollDate, intervalStart);

                if (isWithinInterval)
                {
                    // Do not add it to the dayTax yet. Just update intervalHighest
                    intervalHighest = Math.Max(applicableTax, intervalHighest);
                }
                else
                {
                    // Update singleChargeInterval variables
                    intervalHighest = applicableTax;
                    intervalStart = tollDate;

                    // Update dayTax
                    dayTax = Math.Min(dayTax + applicableTax, city.CityRule.MaxChargeAmountPerDay);
                }

                // Check if the tollDate is from the next day (to reset the daily charge)
                if (prevDate != null && prevDate.Value.Date != tollDate.Date)
                {
                    totalTax += dayTax;
                    dayTax = 0;
                }

                prevDate = tollDate;
            }

            // Last day (in case it is the same day as prev day)
            totalTax += dayTax;
            return totalTax;
        }

        public bool IsTollFreeVehicle(City city, Vehicle vehicle)
        {
            if (city.TollFreeVehicles.Contains(vehicle.VehicleType)) return true;
            return false;
        }

        public bool IsTollFreeDate(DateTime date, City city)
        {
            // Check for free month
            bool isFreeMonth = (city.CityRule.TollFreeMonth == date.Month);
            if (isFreeMonth) return true;

            // Check for public weekend
            bool isWeekend = city.CityCalendar.IsWeekend(date);
            if (city.CityRule.HasTollFreeWeekend && isWeekend) return true;

            // Check for public holiday
            bool isPublicHoliday = city.CityCalendar.IsPublicHoliday(date);
            if (city.CityRule.HasTollFreeHoliday && isPublicHoliday) return true;

            // Check for before public holiday
            bool isBeforePublicHoliday = city.CityCalendar.IsBeforePublicHoliday(date, city.CityRule.DaysBeforeHolidayTollFree);
            if (isPublicHoliday) return true;

            return false;
        }

        public bool IsSingleCharge(int interval, DateTime tollDate, DateTime? intervalStart)
        {
            if (intervalStart == null) return false;

            TimeSpan? distance = tollDate - intervalStart;
            double? distanceMinutes = distance?.TotalMinutes;
            return distanceMinutes <= interval;
        }
    }
}
