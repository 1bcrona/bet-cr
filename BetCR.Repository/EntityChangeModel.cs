using Microsoft.EntityFrameworkCore;

namespace BetCR.Repository
{
    public class EntityChangeModel
    {
        public string EntityId { get; set; }

        public string EventType { get; set; }

        public object Entity;

        public DbContext Context;
    }
}
