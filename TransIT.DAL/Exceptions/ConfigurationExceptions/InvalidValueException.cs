using System;

namespace TransIT.DAL.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a configurable variable has an invalid value
    /// Exception generated with Visual Studio code snippet
    /// </summary>
    [Serializable]
    public class InvalidValueException : Exception
    {
        public InvalidValueException()
        {
        }

        public InvalidValueException(string message) : base(message)
        {
        }

        public InvalidValueException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidValueException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
