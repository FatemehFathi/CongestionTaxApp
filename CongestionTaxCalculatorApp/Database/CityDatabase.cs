using System;
using System.Collections.Generic;
using System.Data.SQLite;
using CongestionTaxCalculatorApp.Models;
using CongestionTaxCalculatorApp.Types;

namespace CongestionTaxCalculatorApp.Database
{
    public class CityDatabase
    {
        private readonly SQLiteConnection connection;
        private readonly CalendarDatabase calendarDatabase;
        private readonly RuleDatabase ruleDatabase;
        private readonly TariffDatabase tariffDatabase;
        private readonly TollFreeVehicleDatabase tollFreeVehicleDatabase;

        public CityDatabase()
        {
            connection = SqliteDatabase.GetConnection();
            calendarDatabase = new CalendarDatabase();
            ruleDatabase = new RuleDatabase();
            tariffDatabase = new TariffDatabase();
            tollFreeVehicleDatabase = new TollFreeVehicleDatabase();
        }

        // CRUD functions
    }
}
