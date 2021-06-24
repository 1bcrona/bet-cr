namespace BetCR.Repository.ValueObject
{
    public class TeamStanding
    {
        public string ExternalId { get; set; }
        public string TeamName { get; set; }
        public string BadgeURL { get; set; }
        public int Position { get; set; }
    }
}