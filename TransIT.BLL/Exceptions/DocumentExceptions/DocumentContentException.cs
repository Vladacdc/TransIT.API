using System;
using System.Collections.Generic;
using System.Text;

namespace TransIT.BLL.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a document content type isn't supported by this application
    /// Exception generated with Visual Studio code snippet
    /// </summary>
    [Serializable]
    public class DocumentContentException : DocumentException
    {
        public DocumentContentException()
        {
        }

        public DocumentContentException(string message) : base(message)
        {
        }

        public DocumentContentException(string message, Exception inner) : base(message, inner)
        {
        }

        protected DocumentContentException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
