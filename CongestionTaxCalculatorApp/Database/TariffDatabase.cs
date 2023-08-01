using System;
using System.Collections.Generic;
using System.Data.SQLite;
using CongestionTaxCalculatorApp.Models;
using CongestionTaxCalculatorApp.Types;

namespace CongestionTaxCalculatorApp.Database
{
    public class TariffDatabase
    {
        private readonly SQLiteConnection connection;

        public TariffDatabase()
        {
            connection = SqliteDatabase.GetConnection();
        }

        // CRUD functions
    }
}
