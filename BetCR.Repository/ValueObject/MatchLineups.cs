using System.Collections.Generic;

namespace BetCR.Repository.ValueObject
{
    public class MatchLineups
    {
        #region Public Properties

        public List<Lineup> AwayTeamLineup { get; set; }
        public List<Lineup> HomeTeamLineup { get; set; }

        #endregion Public Properties
    }
}