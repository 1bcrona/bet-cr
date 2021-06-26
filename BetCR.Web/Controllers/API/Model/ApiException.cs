using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetCR.Web.Controllers.API.Model
{
    public class ApiException : Exception
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public int StatusCode { get; set; }
    }
}
