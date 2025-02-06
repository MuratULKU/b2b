using Microsoft.Extensions.Logging;

namespace B2B.Logger.FileLogger
{
    public static class FileLoggerExtensions
    {
        public static ILoggingBuilder AddFileLoger(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<ILoggerProvider, FileLoggerProvider>();
            //builder.Services.Configure(); şimdilik boş kalsın dosya adıalabiliriz.
            return builder;
        }
    }
}
