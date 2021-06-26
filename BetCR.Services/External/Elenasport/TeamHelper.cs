using BetCR.Services.External.Elenasport.Model;
using ColorThiefDotNet;
using System;
using System.Drawing;
using System.Linq;

namespace BetCR.Services.External.Elenasport
{
    public class TeamHelper
    {
        #region Public Methods

        public static string GetColorsOfTeam(TeamResult team)
        {
            if (team.BadgeURL == null) return String.Join(";", "blue", "yellow");
            var colorThief = new ColorThief();
            var image = GetImage(team.BadgeURL);
            var color = colorThief.GetPalette(image, 8);
            return string.Join(";", color.Take(2).Select(s => s.Color.ToHexString()));
        }

        #endregion Public Methods

        #region Private Methods

        private static Bitmap GetImage(string uri)
        {
            var request = System.Net.WebRequest.Create(uri);
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();
            return new Bitmap(responseStream);
        }

        #endregion Private Methods
    }
}