using BuildingBlocks.Abstractions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BuildingBlocks.EventStore
{
    public interface IAggregateSet
    {
        IQueryable<TAggregateRoot> Set<TAggregateRoot>()
            where TAggregateRoot : AggregateRoot;

        Task<TAggregateRoot> FindAsync<TAggregateRoot>(Guid id)
            where TAggregateRoot : AggregateRoot;
    }

    public class AggregateSet : IAggregateSet
    {
        private readonly IEventStoreDbContext _context;
        private readonly IDateTime _dateTime;
        public AggregateSet(IEventStoreDbContext context, IDateTime dateTime)
        {
            _context = context;
            _dateTime = dateTime;
        }

        private IQueryable<StoredEvent> StoredEvents(string aggregateName, Guid[] streamIds = null, DateTime? createdSince = null)
        {
            createdSince ??= _dateTime.UtcNow;

            return from storedEvent in _context.StoredEvents

                   let ids = streamIds ?? _context.StoredEvents.Where(x => x.Aggregate == aggregateName).Select(x => x.StreamId).AsEnumerable()

                   where ids.Contains(storedEvent.StreamId) && storedEvent.CreatedOn <= createdSince
                   
                   select storedEvent;
        }

        public IQueryable<TAggregateRoot> Set<TAggregateRoot>()
            where TAggregateRoot : AggregateRoot
        {
            var aggregateName = typeof(TAggregateRoot).Name;

            return (from storedEvent in StoredEvents(aggregateName).ToList()
                                      group storedEvent by storedEvent.StreamId into storedEventsGroup
                                      orderby storedEventsGroup.Key
                                      select storedEventsGroup).Aggregate(new List<TAggregateRoot>(), Reduce).AsQueryable();

            static List<TAggregateRoot> Reduce(List<TAggregateRoot> aggregates, IGrouping<Guid,StoredEvent> group)
            {
                var aggregate = (TAggregateRoot)FormatterServices.GetUninitializedObject(typeof(TAggregateRoot));

                foreach (var storedEvent in group.OrderBy(x => x.CreatedOn))
                {
                    aggregate.Apply(JsonConvert.DeserializeObject(storedEvent.Data, Type.GetType(storedEvent.DotNetType)));
                }

                aggregate.ClearChanges();

                aggregates.Add(aggregate);

                return aggregates;
            }
        }

        public async Task<TAggregateRoot> FindAsync<TAggregateRoot>(Guid streamId)
            where TAggregateRoot : AggregateRoot
        {
            return StoredEvents(typeof(TAggregateRoot).Name, new Guid[1] { streamId }).OrderBy(x => x.CreatedOn).Aggregate((TAggregateRoot)FormatterServices.GetUninitializedObject(typeof(TAggregateRoot)), Reduce);

            static TAggregateRoot Reduce(TAggregateRoot aggregateRoot, StoredEvent storedEvent)
            {
                aggregateRoot.Apply(JsonConvert.DeserializeObject(storedEvent.Data, Type.GetType(storedEvent.DotNetType)));

                aggregateRoot.ClearChanges();

                return aggregateRoot;
            }
        }
    }
}
