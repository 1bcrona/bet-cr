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
            var userRepository = _unitOfWork.GetRepository<Repository.Entity.UserTournameRel, string>();

            var userTournamentRels = await userRepository.FindAsync(f => f.Active == 1 && (f.User.Id.Contains(request.Id) || f.User.Email.Contains(request.Email)));

            var groupedUser = userTournamentRels.GroupBy(g => g.User.Id).ToList();

            var items = groupedUser.Select(s =>
                new GetUserTournamentSearchUserResponseModel()
                {
                    User = s.First().User,
                    IsRegisteredToTournament = s.Any(s => s.Tournament.Id == request.TournamentId),
                    TournamentId = request.TournamentId
                });


            return items.ToList();
        }
    }
}
