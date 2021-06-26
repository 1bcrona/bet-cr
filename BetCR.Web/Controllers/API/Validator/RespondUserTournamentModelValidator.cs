using FluentValidation;

namespace BetCR.Web.Controllers.API.Model
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