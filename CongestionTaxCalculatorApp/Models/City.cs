using System;
using System.Collections.Generic;
using CongestionTaxCalculatorApp.Types;

namespace CongestionTaxCalculatorApp.Models
{
    public class City
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public Calendar CityCalendar { get; set; }
        public Rule CityRule { get; set; }
        public List<Tariff> CityTariffs { get; set; }
        public List<VehicleEnum> TollFreeVehicles { get; set; }

        public City(string cityName, Calendar cityCalendar, Rule cityRule,
            List<Tariff> cityTariffs, List<VehicleEnum> tollFreeVehicles)
        {
            CityName = cityName;
            CityCalendar = cityCalendar;
            CityRule = cityRule;
            CityTariffs = cityTariffs;
            TollFreeVehicles = tollFreeVehicles;
        }

        public double GetApplicableTariffAmount(DateTime tollDate)
        {
            Tariff? applicableTariff = CityTariffs.FirstOrDefault(
                t => tollDate.TimeOfDay >= t.From && tollDate.TimeOfDay <= t.To);

            if (applicableTariff != null) return applicableTariff.Amount;
            return 0;
        }

        public bool CheckTariffOverlap(Tariff newTariff)
        {
            if (CityTariffs.Count == 0)
            {
                // No tariffs yet, so no overlap
                return false;
            }

            // Sort existing tariffs by the starting time
            CityTariffs.Sort((t1, t2) => t1.From.CompareTo(t2.From));

            var highestEndTime = CityTariffs[0].To;

            for (int i = 1; i < CityTariffs.Count; i++)
            {
                var currentTariff = CityTariffs[i];

                if (currentTariff.From <= highestEndTime && newTariff.From <= currentTariff.To)
                {
                    // Tariffs overlap
                    return true;
                }

                if (currentTariff.To > highestEndTime)
                {
                    highestEndTime = currentTariff.To;
                }
            }

            // No overlapping tariffs found
            return false;
        }

        public void AddTariff(Tariff tariff)
        {
            bool isOverlap = CheckTariffOverlap(tariff);
            if (!isOverlap)
            {
                CityTariffs.Add(tariff);
            }
            else
            {
                // TODO: Exception Handling
                Console.WriteLine("Cannot add overlapping tariff.");
            }
        }

        public void RemoveTariff(Tariff tariff)
        {
            CityTariffs.Remove(tariff);
        }

        public void AddTollFreeVehicle(VehicleEnum vehicle)
        {
            if (!TollFreeVehicles.Contains(vehicle))
            {
                TollFreeVehicles.Add(vehicle);
            }
        }

        public void RemoveTollFreeVehicle(VehicleEnum vehicle)
        {
            TollFreeVehicles.Remove(vehicle);
        }
    }
}