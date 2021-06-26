namespace BetCR.Web.Controllers.API.Model
{
    public class RespondUserTournamentModel
    {
        #region Public Properties

        public string InvitationId { get; set; }
        public bool Response { get; set; }
        public string TournamentId { get; set; }

        #endregion Public Properties
    }
}