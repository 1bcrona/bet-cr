using BetCR.Web.Controllers.API.Model.Request;
using FluentValidation;

namespace BetCR.Web.Controllers.API.Validator
{
    public class SearchUserTournamentModelValidator : AbstractValidator<SearchUserTournamentModel>
    {
        #region Public Constructors

        public SearchUserTournamentModelValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.Email).NotEmpty().NotNull();
        }

        #endregion Public Constructors
    }
}