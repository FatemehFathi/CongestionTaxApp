using System;
using System.Collections.Generic;
using System.Data.SQLite;
using CongestionTaxCalculatorApp.Models;
using CongestionTaxCalculatorApp.Types;

namespace CongestionTaxCalculatorApp.Database
{
    public class TollFreeVehicleDatabase
    {
        private readonly SQLiteConnection connection;

        public TollFreeVehicleDatabase()
        {
            connection = SqliteDatabase.GetConnection();
        }

        // CRUD functions
    }
}
