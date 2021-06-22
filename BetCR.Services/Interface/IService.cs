using BetCR.Repository.Repository.Base.Interfaces;

namespace BetCR.Services.Interface
{
    public interface IService
    {
        #region Public Properties

        public IUnitOfWork UnitOfWork { get; }

        #endregion Public Properties
    }
}