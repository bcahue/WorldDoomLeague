using System;

namespace WorldDoomLeague.Application.Common.Exceptions
{
    public class NotZippedException : Exception
    {
        public NotZippedException()
            : base()
        {
        }

        public NotZippedException(string message)
            : base(message)
        {
        }

        public NotZippedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public NotZippedException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not zipped prior to uploading.")
        {
        }
    }
}
