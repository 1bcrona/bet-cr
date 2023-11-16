using System.Collections.Generic;
using BetCR.Repository.Entity;

namespace BetCR.Web.Controllers.API.Model.Response
{
    public class GetUserTournamentResponseModel
    {
        #region Public Fields

        public List<Tournament> All;

        public Tournament Current;

        #endregion Public Fields
    }
}