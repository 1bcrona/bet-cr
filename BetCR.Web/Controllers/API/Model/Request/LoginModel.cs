namespace BetCR.Web.Controllers.API.Model.Request
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