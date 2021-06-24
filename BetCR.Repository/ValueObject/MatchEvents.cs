using System.Collections.Generic;

namespace BetCR.Repository.ValueObject
{
    public class MatchEvents
    {
        #region Public Properties

        public List<Event> AwayTeamEvents { get; set; }
        public List<Event> HomeTeamEvents { get; set; }

        #endregion Public Properties
    }
}