using WorldDoomLeague.Application.Common.Interfaces;
using System;

namespace WorldDoomLeague.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
