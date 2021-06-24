using BetCR.Web.Models;
using FluentValidation;

namespace BetCR.Web.Controllers.API.Validator
{
    public class RegisterUserModelValidator : AbstractValidator<RegisterUserModel>
    {
        #region Public Constructors

        public RegisterUserModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.Password).Length(1, 100);
            RuleFor(x => x.Firstname).NotEmpty().NotNull();
            RuleFor(x => x.Surname).NotEmpty().NotNull();
            RuleFor(x => x.DateOfBirth).NotEmpty().NotNull();
        }

        #endregion Public Constructors
    }
}