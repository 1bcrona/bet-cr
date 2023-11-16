using BetCR.Web.Controllers.API.Model.Request;
using FluentValidation;

namespace BetCR.Web.Controllers.API.Validator
{
    public class InviteUserTournamentModelValidator : AbstractValidator<InviteUserToTournamentModel>
    {
        #region Public Constructors

        public InviteUserTournamentModelValidator()
        {
            RuleFor(x => x.InviterUserId).NotEmpty().NotNull();
            RuleFor(x => x.InviteeUserId).NotEmpty().NotNull();
            RuleFor(x => x.TournamentId).NotEmpty().NotNull();
        }

        #endregion Public Constructors
    }
}