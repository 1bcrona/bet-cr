using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetCR.Repository.Entity;

namespace BetCR.Web.Models
{
    public class TournamentStandingsModel
    {

        public User User { get; set; }

        public int WinCount { get; set; }
        public int LossCount { get; set; }
        public int NotPredictedCount { get; set; }

        public int WinMatchCount { get; set; }
        public int WinDifferenceCount { get; set; }

        public int WinMatchScoreCount { get; set; }
        public int? TotalPoints { get; set; }
        public int LossMatchCount { get; set; }
    }
}
