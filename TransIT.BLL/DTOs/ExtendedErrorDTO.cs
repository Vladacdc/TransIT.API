using System;
using System.Diagnostics;
using System.Text;

namespace TransIT.BLL.DTOs
{
    public class ExtendedErrorDTO
    {
        private readonly Exception _exception;

        public ExtendedErrorDTO(Exception exception)
        {
            _exception = exception;
        }

        /// <summary>
        /// Prints all method calls with file names and line numbers.
        /// I took it from https://github.com/AndreyWD/EasySharp/blob/master/NHelpers/ExceptionsDealing/Extensions/ExceptionExtensions.cs
        /// </summary>
        public string StackTrace
        {
            get
            {
                StackTrace stackTrace = new StackTrace(_exception, true);
                StackFrame[] frames = stackTrace.GetFrames();

                if (frames is null)
                {
                    return string.Empty;
                }

                var traceStringBuilder = new StringBuilder();

                for (var i = 0; i < frames.Length; i++)
                {
                    StackFrame frame = frames[i];

                    if (frame.GetFileLineNumber() < 1)
                        continue;

                    traceStringBuilder.AppendLine($"File: {frame.GetFileName()}");
                    traceStringBuilder.AppendLine($"Method: {frame.GetMethod().Name}");
                    traceStringBuilder.AppendLine($"LineNumber: {frame.GetFileLineNumber()}")
                                      .Append("\r\n");

                    if (i == frames.Length - 1)
                        break;

                    traceStringBuilder.AppendLine(" ---> ");
                }

                string stackTraceFootprints = traceStringBuilder.ToString();

                if (string.IsNullOrWhiteSpace(stackTraceFootprints))
                    return "Stack trace empty";

                return stackTraceFootprints;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _exception.Message;
            }
        }

        public ExtendedErrorDTO InnerException
        {
            get
            {
                return _exception.InnerException == null
                    ? null 
                    : new ExtendedErrorDTO(_exception.InnerException);
            }
        }
    }
}
