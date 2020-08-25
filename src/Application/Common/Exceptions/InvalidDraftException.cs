using Microsoft.Extensions.Caching.Memory;
using System;

namespace WorldDoomLeague.Application.Common.Exceptions
{
    public class InvalidDraftException : Exception
    {
        public InvalidDraftException()
            : base()
        {
        }

        public InvalidDraftException(string message)
            : base(message)
        {
        }

        public InvalidDraftException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public InvalidDraftException(string name, object key, string reason)
            : base($"Draft for season: \"{name}\" ({key}) did not process successfully. Reason: {reason}")
        {
        }
    }
}
