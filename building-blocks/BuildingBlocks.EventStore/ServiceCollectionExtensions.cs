using BuildingBlocks.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BuildingBlocks.EventStore
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEventStore(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
        {
            services.AddTransient<IEventStoreDbContext, EventStoreDbContext>();
            services.AddTransient<IAggregateSet, AggregateSet>();
            services.AddTransient<IEventStore, EventStore>();
            services.AddSingleton<IDateTime, MachineDateTime>();
            services.AddTransient<IAppDbContext, AppDbContext>();
            services.AddDbContext<EventStoreDbContext>(optionsAction);
        }
    }

    public class MachineDateTime : IDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
