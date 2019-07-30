namespace TransIT.BLL.Helpers.Configuration
{
    enum Logger
    {
        Azure,
        FileSystem
    }
    static class LoggerConfig
    {
        public static readonly Logger logger = Logger.Azure;
    }
}
