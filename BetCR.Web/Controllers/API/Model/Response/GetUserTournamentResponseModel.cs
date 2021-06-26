using BetCR.Repository.Entity;
using System.Collections.Generic;

namespace BetCR.Web.Controllers.API.Model
{
    public class GetUserTournamentResponseModel
    {
        #region Public Fields

        public List<Tournament> All;

        public Tournament Current;

        #endregion Public Fields
    }
}