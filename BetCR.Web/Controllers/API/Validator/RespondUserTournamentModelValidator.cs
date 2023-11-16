using BetCR.Web.Controllers.API.Model.Request;
using FluentValidation;

namespace BetCR.Web.Controllers.API.Validator
{
    public class RespondUserTournamentModelValidator : AbstractValidator<RespondUserTournamentModel>
    {
        #region Public Constructors

        public RespondUserTournamentModelValidator()
        {
            RuleFor(x => x.TournamentId).NotEmpty().NotNull();
        }

        #endregion Public Constructors
    }
}