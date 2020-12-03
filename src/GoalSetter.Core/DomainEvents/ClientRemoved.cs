using System;

namespace GoalSetter.Core.DomainEvents
{
    public class ClientRemoved
    {
        public ClientRemoved(DateTime deleted)
        {
            Deleted = deleted;
        }

        public DateTime Deleted { get; }
    }
}
