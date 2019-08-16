using System;
using System.Collections.Generic;
using System.Text;

namespace TransIT.DAL.Exceptions
{
    /// <summary>
    /// The exception that is thrown when specified in configuration storage type is invalid or unsupported
    /// Exception generated with Visual Studio code snippet
    /// </summary>
    [Serializable]
    public class InvalidStorageTypeException : ConfigurationException
    {
        public InvalidStorageTypeException()
        {
        }

        public InvalidStorageTypeException(string message) : base(message)
        {
        }

        public InvalidStorageTypeException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidStorageTypeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
