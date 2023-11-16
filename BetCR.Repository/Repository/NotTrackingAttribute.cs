using System;

namespace BetCR.Repository.Repository
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NotTrackingAttribute : Attribute
    {
    }
}