namespace B2B.Logger.FileLogger
{
    public class FileLogger : ILogger
    {

        private readonly FileLoggerProvider _fileLoggerProvider;

        public FileLogger(FileLoggerProvider fileLoggerProvider)
        {
            _fileLoggerProvider = fileLoggerProvider;
        }

        public IDisposable BeginScope<TState>(TState state) => null;


        public bool IsEnabled(LogLevel logLevel) => true;
       

        public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter("b2blog.txt", true))
                {
                    await streamWriter.WriteLineAsync($"Log Level : {logLevel.ToString()} | Event ID : {eventId.Id} | Event Name : {eventId.Name} | Formatter : {formatter(state, exception)}");
                    streamWriter.Close();
                    await streamWriter.DisposeAsync();
                }
            }
            catch (Exception)
            {

                //
            }
            
        }
    }
}
