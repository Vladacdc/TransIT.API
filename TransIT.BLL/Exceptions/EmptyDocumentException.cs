using System;
using System.Collections.Generic;
using System.Text;

namespace TransIT.BLL.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a document is empty
    /// Exception generated with Visual Studio code snippet
    /// </summary>
    [Serializable]
    public class EmptyDocumentException : Exception
    {
        public EmptyDocumentException()
        {
        }

        public EmptyDocumentException(string message) : base(message)
        {
        }

        public EmptyDocumentException(string message, Exception inner) : base(message, inner)
        {
        }

        protected EmptyDocumentException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
