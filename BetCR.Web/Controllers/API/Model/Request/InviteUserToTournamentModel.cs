namespace BetCR.Web.Controllers.API.Model.Request
{
    public class InviteUserToTournamentModel
    {
        #region Public Properties

        public string InviteeUserId { get; set; }
        public string InviterUserId { get; set; }
        public string TournamentId { get; set; }

        #endregion Public Properties
    }
}