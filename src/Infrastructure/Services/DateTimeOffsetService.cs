using WorldDoomLeague.Application.Common.Interfaces;
using System;

namespace WorldDoomLeague.Infrastructure.Services
{
    public class DateTimeOffsetService : IDateTimeOffset
    {
        public DateTimeOffset LocalNow => DateTimeOffset.Now;
        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    }
}
