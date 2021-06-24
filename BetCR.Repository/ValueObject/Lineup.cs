namespace BetCR.Repository.ValueObject
{
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
}