using System;
using System.Collections.Generic;
using System.Text;

namespace TransIT.BLL.Exceptions
{

    [Serializable]
    public class WrongDocumentSizeException : DocumentException
    {
        public WrongDocumentSizeException() { }
        public WrongDocumentSizeException(string message) : base(message) { }
        public WrongDocumentSizeException(string message, Exception inner) : base(message, inner) { }
        protected WrongDocumentSizeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
