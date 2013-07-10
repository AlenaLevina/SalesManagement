using System;

namespace Data.Exceptions
{
    public class DataException : Exception
    {
        public DataException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public DataException(string message)
            : base(message)
        { }
    }
}
