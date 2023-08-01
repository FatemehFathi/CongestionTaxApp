using System;
using System.Collections.Generic;
using System.Data.SQLite;
using CongestionTaxCalculatorApp.Models;
using CongestionTaxCalculatorApp.Types;

namespace CongestionTaxCalculatorApp.Database
{
    public class CalendarDatabase
    {
        private readonly SQLiteConnection connection;

        public CalendarDatabase()
        {
            connection = SqliteDatabase.GetConnection();
        }

        // CRUD functions
    }
}
