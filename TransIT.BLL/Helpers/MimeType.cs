using System;
using System.IO;
using System.Linq;

namespace TransIT.BLL.Helpers
{
    public class MimeType
    {
        // this is the binary value that every PDF file starts with
        private static readonly byte[] PDFHeader = { 37, 80, 68, 70, 45, 49, 46 };

        public static string GetMimeType(Stream stream)
        {
            string mime = "application/octet-stream"; //DEFAULT UNKNOWN MIME TYPE
            byte[] file;
            int blockSize = 256;

            using (stream)
            {
                int minimumNeededSize = (int)((blockSize < stream.Length) ? blockSize : stream.Length);
                file = new byte[minimumNeededSize];
                stream.Read(file, 0, minimumNeededSize);
            }

            if (stream.Length >= PDFHeader.Length && file.Take(PDFHeader.Length).SequenceEqual(PDFHeader))
            {
                mime = "application/pdf";
            }
            
            return mime;
        }


    }
}
