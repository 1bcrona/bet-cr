namespace BetCR.Repository.ValueObject
{
    public class UserActionStatus
    {
        #region Public Fields

        public const string NEW = "NEW";
        public const string RESPOND = "RESPOND";
        public const string WAITING_FOR_REPLY = "WAITING_FOR_REPLY";

        #endregion Public Fields
    }

    public class UserActionType
    {
        #region Public Fields

        public const string TOURNAMENT_INVITE = "TOURNAMENT_INVITE";

        #endregion Public Fields
    }

    public class UserBetPoint
    {
        #region Public Fields

        public const int WinDifference = 2;
        public const int WinMatch = 1;
        public const int LossMatch = -1;
        public const int WinMatchScore = 5;

        #endregion Public Fields
    }
}