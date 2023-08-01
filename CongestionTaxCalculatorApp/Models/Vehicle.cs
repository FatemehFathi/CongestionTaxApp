using System;
using System.Collections.Generic;
using CongestionTaxCalculatorApp.Types;

namespace CongestionTaxCalculatorApp.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public VehicleEnum VehicleType { get; private set; }
        public List<DateTime> TollDates { get; private set; }

        public Vehicle(VehicleEnum vehicleType)
        {
            VehicleType = vehicleType;
            TollDates = new List<DateTime>();
        }

        public Vehicle(VehicleEnum vehicleType, List<DateTime> tollDates)
        {
            VehicleType = vehicleType;
            TollDates = tollDates;
        }

        public void AddTollDate(DateTime tollDate)
        {
            TollDates.Add(tollDate);
        }

        public void RemoveTollDate(DateTime tollDate)
        {
            TollDates.Remove(tollDate);
        }
    }
}