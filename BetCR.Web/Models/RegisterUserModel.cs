namespace BetCR.Web.Models
{
    public class RegisterUserModel
    {
        #region Public Properties

        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }

        public string Password { get; set; }
        public string Surname { get; set; }

        #endregion Public Properties
    }
}