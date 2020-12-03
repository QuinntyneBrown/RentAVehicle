using BuildingBlocks.Abstractions;
using System;

namespace GoalSetter.Core.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IAppDbContext context)
        {

        }

        public static T CreateAndStore<T>(Func<T> factory, IAppDbContext context)
            where T : AggregateRoot
        {
            var aggregate = factory();
            context.Store(aggregate);
            return aggregate;
        }
    }
}
