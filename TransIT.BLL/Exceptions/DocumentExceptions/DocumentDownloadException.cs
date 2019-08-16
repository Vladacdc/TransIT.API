using System;
using System.Collections.Generic;
using System.Text;

namespace TransIT.BLL.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a document cannot be accessed and downloaded
    /// Exception generated with Visual Studio code snippet
    /// </summary>
    [Serializable]
    public class DocumentDownloadException : DocumentException
    {
        public DocumentDownloadException()
        {
        }

        public DocumentDownloadException(string message) : base(message)
        {
        }

        public DocumentDownloadException(string message, Exception inner) : base(message, inner)
        {
        }

        protected DocumentDownloadException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
