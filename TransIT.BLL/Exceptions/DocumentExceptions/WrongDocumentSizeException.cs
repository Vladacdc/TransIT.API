using System;

namespace TransIT.BLL.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a document size is not in right range
    /// Exception generated with Visual Studio code snippet
    /// </summary>
    [Serializable]
    public class WrongDocumentSizeException : DocumentException
    {
        public WrongDocumentSizeException()
        {
        }

        public WrongDocumentSizeException(string message) : base(message)
        {
        }

        public WrongDocumentSizeException(string message, Exception inner) : base(message, inner)
        {
        }

        protected WrongDocumentSizeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
