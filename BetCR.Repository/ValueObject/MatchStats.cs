using System.Collections.Generic;

namespace BetCR.Repository.ValueObject
{
    public class MatchStats
    {
        #region Public Properties

        public List<Stat> AwayTeamStats { get; set; }
        public List<Stat> HomeTeamStats { get; set; }

        #endregion Public Properties
    }
}