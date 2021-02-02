using System;

namespace WorldDoomLeague.Application.Common.Exceptions
{
    public class EmptyFileException : Exception
    {
        public EmptyFileException()
            : base()
        {
        }

        public EmptyFileException(string message)
            : base(message)
        {
        }

        public EmptyFileException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public EmptyFileException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was empty.")
        {
        }
    }
}
