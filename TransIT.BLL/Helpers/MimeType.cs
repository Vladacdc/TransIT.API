using System;
using System.IO;
using System.Linq;

namespace TransIT.BLL.Helpers
{
    public class MimeType
    {
        private static readonly byte[] PDF = { 37, 80, 68, 70, 45, 49, 46 };

        public static string GetMimeType(byte[] file)
        {
            string mime = "application/octet-stream"; //DEFAULT UNKNOWN MIME TYPE

            if (file.Take(7).SequenceEqual(PDF))
            {
                mime = "application/pdf";
            }
            
            return mime;
        }


    }
}
