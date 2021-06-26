using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetCR.Repository.ValueObject
{
    public class UserActionStatus
    {
        public const string NEW = "NEW";
        public const string WAITING_FOR_REPLY = "WAITING_FOR_REPLY";
        public const string RESPOND = "RESPOND";
    }

    public class UserActionType
    {
        public const string TOURNAMENT_INVITE = "TOURNAMENT_INVITE";
    }
}
