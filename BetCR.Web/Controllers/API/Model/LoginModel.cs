﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetCR.Web.Controllers.API.Model
{
    public class LoginModel
    {
        #region Public Properties

        public string Email { get; set; }
        public bool IsRememberMe { get; set; }
        public string Password { get; set; }

        #endregion Public Properties
    }
}
