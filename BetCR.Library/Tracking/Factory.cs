using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetCR.Library.Tracking.Infrastructure;

namespace BetCR.Library.Tracking
{
    public static class PublisherFactory
    {
        public static ConcurrentDictionary<string, IPublisher> s_Publishers =
            new();


        public static IPublisher GetPublisher(string name)
        {
            s_Publishers.TryGetValue(name, out var publisher);

            if (publisher == null)
            {
                s_Publishers[name] = new Publisher(name);
            }

            return s_Publishers[name];

        }
    }
}
