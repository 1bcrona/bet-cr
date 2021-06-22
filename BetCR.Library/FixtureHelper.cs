using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BetCR.Library
{
    public static class FixtureHelper
    {

        public static string GetWeek(DateTime date)
        {
            var diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            var startOfWeek = date.AddDays(-1 * diff).Date;
            var endOfWeek = startOfWeek.AddDays(6);
            return $"{startOfWeek.Date.ToShortDateString()}_{endOfWeek.Date.ToShortDateString()}";

        }
    }
}
