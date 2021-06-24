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
}