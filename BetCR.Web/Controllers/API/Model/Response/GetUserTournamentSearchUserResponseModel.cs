using BetCR.Repository.Entity;

namespace BetCR.Web.Controllers.API.Model.Response
{
    public class GetUserTournamentSearchUserResponseModel
    {
        #region Public Properties

        public bool IsRegisteredToTournament { get; set; }
        public string TournamentId { get; set; }
        public User User { get; set; }

        #endregion Public Properties
    }
}