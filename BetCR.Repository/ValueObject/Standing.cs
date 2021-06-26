namespace BetCR.Repository.ValueObject
{
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
        public TeamStanding TeamStanding { get; set; }
        public int Win { get; set; }

        #endregion Public Properties
    }
}