using System;

namespace KillemAll
{
    public class InvalidCommandException : Exception
    {
        public InvalidCommandException(string message) : base(message) { }
    }

    public class ProcessNotFoundException : Exception
    {
        public ProcessNotFoundException(string message) : base(message){ }
    }

    public class HistoryException : Exception
    {
        public HistoryException(string message) : base(message) { }
    }
}