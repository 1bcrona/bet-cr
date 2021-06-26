using System;

namespace BetCR.Web.Controllers.API.Model
{
    public class ApiException : Exception
    {
        #region Public Properties

        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public int StatusCode { get; set; }

        #endregion Public Properties
    }
}