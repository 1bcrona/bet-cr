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

        public static string GetColorsOfLogo(string uri)
        {
            if (string.IsNullOrEmpty(uri)) return String.Join(";", "blue", "white");
            var colorThief = new ColorThief();
            var image = GetImage(uri);
            var color = colorThief.GetPalette(image, 8);
            return string.Join(";", color.Take(2).Select(s => s.Color.ToHexString()));
        }

        public static string GetColorsOfTeam(TeamResult team)
        {
            return team.BadgeURL == null ? String.Join(";", "blue", "white") : GetColorsOfLogo((team.BadgeURL));
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