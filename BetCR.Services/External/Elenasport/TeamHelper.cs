using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetCR.Repository.Entity;
using BetCR.Services.External.Elenasport.Model;
using ColorThiefDotNet;

namespace BetCR.Services.External.Elenasport
{
    public class TeamHelper
    {
        public static string GetColorsOfTeam(TeamResult team)
        {
            if (team.BadgeURL == null) return String.Join(";", "blue", "yellow");
            var colorThief = new ColorThief();
            var image = GetImage(team.BadgeURL);
            var color = colorThief.GetPalette(image, 8);
            return string.Join(";", color.Take(2).Select(s => s.Color.ToHexString()));
        }

        private static Bitmap GetImage(string uri)
        {

            var request = System.Net.WebRequest.Create(uri);
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();
            return new Bitmap(responseStream);
        }

    }
}
