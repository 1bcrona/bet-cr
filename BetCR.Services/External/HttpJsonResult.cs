using Newtonsoft.Json.Linq;
using System.Net;

namespace BetCR.Services.External
{
    public class HttpJsonResult
    {
        #region Public Properties

        public JToken Data { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        #endregion Public Properties
    }
}