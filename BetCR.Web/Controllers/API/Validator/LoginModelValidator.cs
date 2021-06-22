using BetCR.Web.Controllers.API.Model;
using BetCR.Web.Models;
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


    public class CreateUserMatchBetModelValidator : AbstractValidator<CreateUserMatchBetModel>
    {
        #region Public Constructors

        public CreateUserMatchBetModelValidator()
        {
            RuleFor(x => x.MatchId).NotEmpty().NotNull();
            RuleFor(x => x.HomeTeamScore).GreaterThan(-1);
            RuleFor(x => x.AwayTeamScore).GreaterThan(-1);
            RuleFor(x => x.AwayTeamScore).GreaterThan(-1);
            RuleFor(x => x.Leverage).GreaterThan(-1);
        }

        #endregion Public Constructors
    }

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