using BuildingBlocks.Abstractions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.EventStore
{
    public class AppDbContext : IAppDbContext
    {
        private readonly IEventStore _eventStore;
        private readonly IAggregateSet _aggregateSet;
        public AppDbContext(IEventStore eventStore, IAggregateSet aggregateSet)
        {
            _eventStore = eventStore;
            _aggregateSet = aggregateSet;
        }
        public async Task<TAggregateRoot> FindAsync<TAggregateRoot>(Guid id) where TAggregateRoot : AggregateRoot
        {
            return await _aggregateSet.FindAsync<TAggregateRoot>(id);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _eventStore.SaveChangesAsync(cancellationToken);
        }

        public IQueryable<T> Set<T>() where T : AggregateRoot
        {
            return _aggregateSet.Set<T>();
        }

        public void Store(AggregateRoot aggregateRoot)
        {
            _eventStore.Store(aggregateRoot);
        }
    }
}
