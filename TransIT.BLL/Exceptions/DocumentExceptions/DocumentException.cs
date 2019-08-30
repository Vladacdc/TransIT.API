using System;

namespace TransIT.BLL.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a document doesn't follow some rule of BL
    /// Exception generated with Visual Studio code snippet
    /// </summary>
    [Serializable]
    public class DocumentException : Exception
    {
        public DocumentException()
        {
        }

        public DocumentException(string message) : base(message)
        {
        }

        public DocumentException(string message, Exception inner) : base(message, inner)
        {
        }

        protected DocumentException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
