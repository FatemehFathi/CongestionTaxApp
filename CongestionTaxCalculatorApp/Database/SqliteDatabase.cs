using System;
using System.Collections.Generic;
using System.Data.SQLite;
using CongestionTaxCalculatorApp.Types;
using CongestionTaxCalculatorApp.Models;

namespace CongestionTaxCalculatorApp.Database
{
    public sealed class SqliteDatabase
    {
        private static readonly string? connectionString = "Data Source=CongestionTaxApp.db;Version=3;";
        private static SQLiteConnection? sqliteConnection;

        private SqliteDatabase() { }

        public static SQLiteConnection GetConnection()
        {
            if (sqliteConnection == null && connectionString != null)
            {
                sqliteConnection = new SQLiteConnection(connectionString);
            }

            return sqliteConnection!;
        }

        public static void CreateTables()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    // Create Weekends table
                    command.CommandText = @"CREATE TABLE IF NOT EXISTS Weekends (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            WeekDay INTEGER
                                        )";
                    command.ExecuteNonQuery();

                    // Create PublicHolidays table
                    command.CommandText = @"CREATE TABLE IF NOT EXISTS PublicHolidays (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            HolidayDate TEXT
                                        )";
                    command.ExecuteNonQuery();

                    // Create Calendars table
                    command.CommandText = @"CREATE TABLE IF NOT EXISTS Calendars (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            WeekendIds INTEGER,
                                            PublicHolidayIds INTEGER,
                                            FOREIGN KEY (WeekendIds) REFERENCES Weekends (Id),
                                            FOREIGN KEY (PublicHolidayIds) REFERENCES PublicHolidays (Id)
                                        )";
                    command.ExecuteNonQuery();

                    // Create Rules table
                    command.CommandText = @"CREATE TABLE IF NOT EXISTS Rules (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            HasTollFreeWeekend INTEGER,
                                            HasTollFreeHoliday INTEGER,
                                            DaysBeforeHolidayTollFree INTEGER,
                                            TollFreeMonth INTEGER,
                                            SingleChargeInterval INTEGER,
                                            MaxChargeAmountPerDay REAL
                                        )";
                    command.ExecuteNonQuery();

                    // Create Tariffs table
                    command.CommandText = @"CREATE TABLE IF NOT EXISTS Tariffs (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            StartTime TEXT,
                                            EndTime TEXT,
                                            Amount REAL,
                                            CityId INTEGER,
                                            FOREIGN KEY (CityId) REFERENCES Cities (Id)
                                        )";
                    command.ExecuteNonQuery();

                    // Create Vehicles table
                    command.CommandText = @"CREATE TABLE IF NOT EXISTS Vehicles (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            VehicleType INTEGER
                                        )";
                    command.ExecuteNonQuery();

                    // Create VehicleTollDates table
                    command.CommandText = @"CREATE TABLE IF NOT EXISTS VehicleTollDates (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            VehicleId INTEGER,
                                            TollDate TEXT,
                                            FOREIGN KEY (VehicleId) REFERENCES Vehicles (Id)
                                        )";
                    command.ExecuteNonQuery();

                    // Create TollFreeVehicles table
                    command.CommandText = @"CREATE TABLE IF NOT EXISTS TollFreeVehicles (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            VehicleType TEXT,
                                            CityId INTEGER,
                                            FOREIGN KEY (CityId) REFERENCES Cities (Id)
                                        )";
                    command.ExecuteNonQuery();

                    // Create Cities table
                    command.CommandText = @"CREATE TABLE IF NOT EXISTS Cities (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            CityName TEXT,
                                            CalendarId INTEGER,
                                            RuleId INTEGER,
                                            FOREIGN KEY (CalendarId) REFERENCES Calendars (Id),
                                            FOREIGN KEY (RuleId) REFERENCES Rules (Id)
                                        )";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
