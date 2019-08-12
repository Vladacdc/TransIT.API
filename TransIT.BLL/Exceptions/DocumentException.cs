using System;
using System.Collections.Generic;
using System.Text;

namespace TransIT.BLL.Exceptions
{

    [Serializable]
    public class DocumentException : Exception
    {
        public DocumentException() { }
        public DocumentException(string message) : base(message) { }
        public DocumentException(string message, Exception inner) : base(message, inner) { }
        protected DocumentException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
