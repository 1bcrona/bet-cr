using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetCR.Repository.Entity.Base;
using BetCR.Repository.Repository;

namespace BetCR.Repository.Entity
{
    [NotTracking]
    public class ChangeEvent : BaseEntity<string>
    {
        public string StreamId { get; set; }

        public string EventType { get; set; }
        public string EntityId { get; set; }

        public string Data { get; set; }

        public string DataType { get; set; }

        public long EventEpoch => DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}
