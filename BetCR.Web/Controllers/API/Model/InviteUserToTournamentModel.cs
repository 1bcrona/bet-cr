namespace BetCR.Web.Controllers.API.Model
{
    public class InviteUserToTournamentModel
    {
        public string InviterUserId { get; set; }
        public string InviteeUserId { get; set; }
        public string TournamentId { get; set; }
    }
}