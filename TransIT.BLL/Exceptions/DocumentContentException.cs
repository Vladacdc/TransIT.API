using System;
using System.Collections.Generic;
using System.Text;

namespace TransIT.BLL.Exceptions
{

    [Serializable]
    public class DocumentContentException : DocumentException
    {
        public DocumentContentException() { }
        public DocumentContentException(string message) : base(message) { }
        public DocumentContentException(string message, Exception inner) : base(message, inner) { }
        protected DocumentContentException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
