using MediatR;
using System.Collections.Generic;

namespace BetCR.Web.Handlers.Query.Prediction
{
    public class PredictionDetailQuery : IRequest<IEnumerable<Repository.Entity.Match>>
    {
        #region Public Properties

        public string Week { get; set; }

        #endregion Public Properties
    }
}