using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetCR.Web.Controllers.API.Model
{
    public class ResponseModel<T>
    {
        #region Public Properties

        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public string Result { get; set; }

        public string ReturnUrl { get; set; }

        #endregion Public Properties
    }

    public class ResponseModel : ResponseModel<object>
    {
    }
}
