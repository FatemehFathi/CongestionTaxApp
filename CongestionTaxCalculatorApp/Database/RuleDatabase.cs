using System;
using System.Data.SQLite;
using CongestionTaxCalculatorApp.Models;

namespace CongestionTaxCalculatorApp.Database
{
    public class RuleDatabase
    {
        private readonly SQLiteConnection connection;

        public RuleDatabase()
        {
            connection = SqliteDatabase.GetConnection();
        }

        // CRUD functions
    }
}
