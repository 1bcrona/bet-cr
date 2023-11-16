namespace BetCR.Web.Controllers.API.Model.Request
{
    public class CreateUserMatchBetModel
    {
        #region Public Properties

        public int AwayTeamScore { get; set; }
        public int HomeTeamScore { get; set; }
        public int Leverage { get; set; }
        public string MatchId { get; set; }

        #endregion Public Properties
    }
}