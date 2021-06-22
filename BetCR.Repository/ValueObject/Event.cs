using System.Collections.Generic;

namespace BetCR.Repository.ValueObject
{
    public class Event
    {
        #region Public Properties

        public int Elapsed { get; set; }
        public int ElapsedPlus { get; set; }
        public string EventType { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public string TeamId { get; set; }

        #endregion Public Properties
    }

    public class Lineup
    {
        #region Public Properties

        public int Number { get; set; }
        public Player Player { get; set; }
        public string Position { get; set; }
        public bool StartingXI { get; set; }

        public string TeamId { get; set; }

        #endregion Public Properties
    }

    public class MatchEvents
    {
        #region Public Properties

        public List<Event> AwayTeamEvents { get; set; }
        public List<Event> HomeTeamEvents { get; set; }

        #endregion Public Properties
    }

    public class MatchLineups
    {
        #region Public Properties

        public List<Lineup> AwayTeamLineup { get; set; }
        public List<Lineup> HomeTeamLineup { get; set; }

        #endregion Public Properties
    }

    public class MatchStats
    {
        #region Public Properties

        public List<Stat> AwayTeamStats { get; set; }
        public List<Stat> HomeTeamStats { get; set; }

        #endregion Public Properties
    }

    public class Player
    {
        #region Public Properties

        public string FullName { get; set; }
        public string Name { get; set; }

        #endregion Public Properties
    }

    public class Standing
    {
        #region Public Properties

        public int Draw { get; set; }
        public int GoalAgainst { get; set; }
        public int GoalDifference { get; set; }
        public int GoalFor { get; set; }
        public int Lost { get; set; }
        public int Played { get; set; }

        public int Point { get; set; }
        public int Win { get; set; }
        public TeamStanding TeamStanding { get; set; }
        #endregion Public Properties
    }

    public class TeamStanding
    {
        public string ExternalId { get; set; }
        public string TeamName { get; set; }
        public string BadgeURL { get; set; }
        public int Position { get; set; }
    }
    public class Stat
    {
        #region Public Properties

        public string Key { get; set; }
        public string TeamId { get; set; }
        public int Value { get; set; }

        #endregion Public Properties
    }
}