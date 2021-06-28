using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetCR.Repository.Entity.Base;

namespace BetCR.Repository.Entity
{
    public class ChangeEvent : EntityBase<string>
    {
        public string StreamId { get; set; }

        public string EventType { get; set; }
        public string EntityId { get; set; }

        public string Data { get; set; }

        public string DataType { get; set; }

        public long EventEpoch => DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}
