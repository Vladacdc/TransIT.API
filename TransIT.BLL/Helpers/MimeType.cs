using System;
using System.IO;
using System.Linq;

namespace TransIT.BLL.Helpers
{
    public class MimeType
    {
        private static readonly byte[] PDF = { 37, 80, 68, 70, 45, 49, 46 };

        public static string GetMimeType(Stream stream)
        {
            string mime = "application/octet-stream"; //DEFAULT UNKNOWN MIME TYPE
            byte[] file;

            using (stream)
            {
                int minimumNeededSize = (int)((256 < stream.Length) ? 256 : stream.Length);
                file = new byte[minimumNeededSize];
                stream.Read(file, 0, minimumNeededSize);
            }

            if (stream.Length >= 7 && file.Take(7).SequenceEqual(PDF))
            {
                mime = "application/pdf";
            }
            
            return mime;
        }


    }
}
