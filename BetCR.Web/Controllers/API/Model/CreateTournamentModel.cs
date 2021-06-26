using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetCR.Web.Controllers.API.Model
{
    public class CreateTournamentModel
    {
        #region Public Properties

        public string TournamentName { get; set; }

        public string Password { get; set; }
        public DateTime EndDate => DateTime.Parse((EndDateString)).ToUniversalTime();

        public long EndDateEpoch
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return Convert.ToInt64((EndDate - epoch).TotalSeconds);
            }
        }

        public string EndDateString { get; set; }

        public DateTime StartDate => DateTime.Parse(StartDateString).ToUniversalTime();

        public long StartDateEpoch
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return Convert.ToInt64((StartDate - epoch).TotalSeconds);
            }
        }

        public string StartDateString { get; set; }


        #endregion Public Properties
    }
}
