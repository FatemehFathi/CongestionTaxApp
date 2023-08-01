using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculatorApp.Models
{
    public class Tariff
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public TimeSpan From { get; private set; }
        public TimeSpan To { get; private set; }
        public double Amount { get; private set; }

        public Tariff(TimeSpan from, TimeSpan to, double amount)
        {
            From = from;
            To = to;
            Amount = amount;
        }
    }
}