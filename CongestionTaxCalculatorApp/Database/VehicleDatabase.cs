using System;
using System.Collections.Generic;
using System.Data.SQLite;
using CongestionTaxCalculatorApp.Models;
using CongestionTaxCalculatorApp.Types;

namespace CongestionTaxCalculatorApp.Database
{
    public class VehicleDatabase
    {
        private readonly SQLiteConnection connection;

        public VehicleDatabase()
        {
            connection = SqliteDatabase.GetConnection();
        }

        // CRUD functions
    }
}
