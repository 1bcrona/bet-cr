using BetCR.Web.Controllers.API.Model.Request;
using FluentValidation;

namespace BetCR.Web.Controllers.API.Validator
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        #region Public Constructors

        public LoginModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull();
            RuleFor(x => x.Password).Length(1, 100);
            RuleFor(x => x.Email).EmailAddress();
        }

        #endregion Public Constructors
    }
}