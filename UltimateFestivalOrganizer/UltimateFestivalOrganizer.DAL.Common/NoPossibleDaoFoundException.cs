using System;
using System.Runtime.Serialization;

namespace DAL.Common
{
    [Serializable]
    internal class NoPossibleDaoFoundException : Exception
    {
        public NoPossibleDaoFoundException()
        {
        }

        public NoPossibleDaoFoundException(string message) : base(message)
        {
        }

        public NoPossibleDaoFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoPossibleDaoFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}