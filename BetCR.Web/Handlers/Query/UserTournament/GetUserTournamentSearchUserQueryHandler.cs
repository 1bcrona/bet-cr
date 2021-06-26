using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetCR.Repository.Repository.Base.Interfaces;
using BetCR.Web.Controllers.API.Model;
using MediatR;

namespace BetCR.Web.Handlers.Query.UserTournament
{
    public class GetUserTournamentSearchUserQueryHandler : IRequestHandler<GetUserTournamentSearchUserQuery, List<GetUserTournamentSearchUserResponseModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserTournamentSearchUserQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<GetUserTournamentSearchUserResponseModel>> Handle(GetUserTournamentSearchUserQuery request, CancellationToken cancellationToken)
        {
            var userRepository = _unitOfWork.GetRepository<Repository.Entity.User, string>();
            var users = await userRepository.FindAsync(f => f.Active == 1 && (f.Id.Contains(request.Id) || f.Email.Contains(request.Email)));
            var items = users.Select(s =>
                new GetUserTournamentSearchUserResponseModel()
                {
                    User = s,
                    IsRegisteredToTournament = s.UserTournameRels.Any(a => a.Tournament.Id == request.TournamentId && s.Active == 1),
                    TournamentId = request.TournamentId
                });


            return items.ToList();
        }
    }
}
