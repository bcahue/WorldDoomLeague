using System;

namespace WorldDoomLeague.Application.Common.Interfaces
{
    public interface IDateTimeOffset
    {
        DateTimeOffset LocalNow { get; }
        DateTimeOffset UtcNow { get; }
    }
}
