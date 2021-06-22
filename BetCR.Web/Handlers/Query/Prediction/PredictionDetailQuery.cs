using System.Collections.Generic;
using MediatR;

namespace BetCR.Web.Handlers.Query.Prediction
{
    public class PredictionDetailQuery : IRequest<IEnumerable<Repository.Entity.Match>>
    {
        public string Week { get; set; }
    }
}
