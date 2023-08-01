using CongestionTaxCalculatorApp.Database;
using CongestionTaxCalculatorApp.Models;
using CongestionTaxCalculatorApp.Services;
using CongestionTaxCalculatorApp.Types;
using System;

namespace CongestionTaxCalculatorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create and initialize the database
            SqliteDatabase.CreateTables();

            CongestionTaxCalculator calculator = new CongestionTaxCalculator();

            // Add data to the database
            // AddGothenburgDataToDB();
            // AddVehicleDataToDB();

            // Read data from the database
            // City gothenburgCity = ReadGothenburgDataFromDB();
            // Vehicle vehicle = ReadGothenburgDataFromDB();

            // Temporary
            City gothenburgCity = GetGothenburgData();
            Vehicle vehicle = GetVehicleData();

            // Calculate tax for vehicle1 in gothenburgCity
            double tax = calculator.CalculateCongestionTax(gothenburgCity, vehicle);

            Console.WriteLine("Total Congestion Tax for the vehicle in " + gothenburgCity.CityName + " is: " + tax);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static City GetGothenburgData()
        {
            // Add Calendar for Gothenburg
            var gothenburgPublicHolidays2013 = new List<DateTime>
            {
                new DateTime(2013, 1, 1),
                new DateTime(2013, 3, 28),
                new DateTime(2013, 3, 29),
                new DateTime(2013, 4, 1),
                new DateTime(2013, 4, 30),
                new DateTime(2013, 5, 1),
                new DateTime(2013, 5, 8),
                new DateTime(2013, 5, 9),
                new DateTime(2013, 6, 5),
                new DateTime(2013, 6, 6),
                new DateTime(2013, 6, 21),
                new DateTime(2013, 11, 1),
                new DateTime(2013, 12, 24),
                new DateTime(2013, 12, 25),
                new DateTime(2013, 12, 26),
                new DateTime(2013, 12, 31)
            };
            var gothenburgWeekends2013 = new List<WeekDayEnum>
            {
                WeekDayEnum.Saturday,
                WeekDayEnum.Sunday
            };
            var gothenburgCalendar2013 = new Calendar(gothenburgPublicHolidays2013, gothenburgWeekends2013);

            // Rule for Gothenburg
            Rule gothenburgRule = new Rule(true, true, 60, 60, 1, 7);

            // List of Tariffs for Gothenburg
            List<Tariff> gothenburgTariffs = new List<Tariff>
            {
                new Tariff(new TimeSpan(6, 0, 0), new TimeSpan(6, 29, 59), 8),
                new Tariff(new TimeSpan(6, 30, 0), new TimeSpan(6, 59, 59), 13),
                new Tariff(new TimeSpan(7, 0, 0), new TimeSpan(7, 59, 59), 18),
                new Tariff(new TimeSpan(8, 0, 0), new TimeSpan(8, 29, 59), 13),
                new Tariff(new TimeSpan(8, 30, 0), new TimeSpan(14, 59, 59), 8),
                new Tariff(new TimeSpan(15, 0, 0), new TimeSpan(15, 29, 59), 13),
                new Tariff(new TimeSpan(15, 30, 0), new TimeSpan(16, 59, 59), 18),
                new Tariff(new TimeSpan(17, 0, 0), new TimeSpan(17, 59, 59), 13),
                new Tariff(new TimeSpan(18, 0, 0), new TimeSpan(18, 29, 59), 8),
                new Tariff(new TimeSpan(18, 30, 0), new TimeSpan(23, 59, 59), 0),
                new Tariff(new TimeSpan(0, 0, 0), new TimeSpan(5, 59, 59), 0)
            };

            // List of toll-free vehicles in Gothenburg
            List<VehicleEnum> gothenburgTollFreeVehicles = new List<VehicleEnum>
            {
                VehicleEnum.Emergency,
                VehicleEnum.Bus,
                VehicleEnum.Diplomat,
                VehicleEnum.Motorcycle,
                VehicleEnum.Military,
                VehicleEnum.Foreign
            };

            // Create CityDatabase instance
            // CityDatabase cityDatabase = new CityDatabase();

            // City for Gothenburg
            City gothenburg = new City("Gothenburg", gothenburgCalendar2013, gothenburgRule, gothenburgTariffs, gothenburgTollFreeVehicles);

            // Add Gothenburg city to the database
            // cityDatabase.CreateCity(gothenburg);

            return gothenburg;
        }

        static Vehicle GetVehicleData()
        {
            // Add toll dates for the vehicle
            List<DateTime> tollDates = new List<DateTime>
            {
                new DateTime(2013, 01, 14, 21, 0, 0),
                new DateTime(2013, 01, 15, 21, 0, 0),
                new DateTime(2013, 02, 07, 06, 23, 27),
                new DateTime(2013, 02, 07, 15, 27, 0),
                new DateTime(2013, 02, 08, 06, 27, 0),
                new DateTime(2013, 02, 08, 06, 20, 27),
                new DateTime(2013, 02, 08, 14, 35, 0),
                new DateTime(2013, 02, 08, 15, 29, 0),
                new DateTime(2013, 02, 08, 15, 47, 0),
                new DateTime(2013, 02, 08, 16, 01, 0),
                new DateTime(2013, 02, 08, 16, 48, 0),
                new DateTime(2013, 02, 08, 17, 49, 0),
                new DateTime(2013, 02, 08, 18, 29, 0),
                new DateTime(2013, 02, 08, 18, 35, 0),
                new DateTime(2013, 03, 26, 14, 25, 0),
                new DateTime(2013, 03, 28, 14, 07, 27)
            };

            // Create VehicleDatabase instance
            // VehicleDatabase vehicleDatabase = new VehicleDatabase();

            // Vehicle
            Vehicle vehicle = new Vehicle(VehicleEnum.Car, tollDates);

            // Add vehicle data to the database
            // vehicleDatabase.CreateVehicle(vehicle);

            return vehicle;
        }
    }
}
