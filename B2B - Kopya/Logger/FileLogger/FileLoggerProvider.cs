namespace B2B.Logger.FileLogger
{
    public class FileLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(this);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
