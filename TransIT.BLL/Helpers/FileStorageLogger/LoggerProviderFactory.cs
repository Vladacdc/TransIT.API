using System;
using TransIT.BLL.Helpers.Configuration;
using TransIT.BLL.Helpers.FileStorageLogger.FileStorageInterface;

namespace TransIT.BLL.Helpers.FileStorageLogger
{
    public static class LoggerProviderFactory
    {
        public static IFileStorageLogger GetFileStorageLogger()
        {
            if(LoggerConfig.logger == Logger.Azure)
            {
                return new AzureFileStorage();
            }

            if( LoggerConfig.logger ==Logger.FileSystem)
            {
                return new FileSystemStorage();
            }
            throw new Exception("Logger not selected!");
        }

    }
}
