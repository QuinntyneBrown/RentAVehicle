using System;

namespace BuildingBlocks.Abstractions
{
    public interface IDateTime
    {
        System.DateTime UtcNow { get; }
    }
}
