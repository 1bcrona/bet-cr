using System;

namespace BetCR.Library
{
    public static class FixtureHelper
    {
        #region Public Methods

        public static string GetWeek(DateTime date)
        {
            var diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            var startOfWeek = date.AddDays(-1 * diff).Date;
            var endOfWeek = startOfWeek.AddDays(6);
            return $"{startOfWeek.Date:dd.MM.yyyy}_{endOfWeek.Date:dd.MM.yyyy}";
        }

        #endregion Public Methods
    }
}