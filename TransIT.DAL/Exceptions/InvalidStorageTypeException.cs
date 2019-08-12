using System;
using System.Collections.Generic;
using System.Text;

namespace TransIT.DAL.Exceptions
{

    [Serializable]
    public class InvalidStorageTypeException : ConfigurationException
    {
        public InvalidStorageTypeException() { }
        public InvalidStorageTypeException(string message) : base(message) { }
        public InvalidStorageTypeException(string message, Exception inner) : base(message, inner) { }
        protected InvalidStorageTypeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
