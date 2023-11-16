using BetCR.Web.Controllers.API.Model.Request;
using FluentValidation;

namespace BetCR.Web.Controllers.API.Validator
{
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
}