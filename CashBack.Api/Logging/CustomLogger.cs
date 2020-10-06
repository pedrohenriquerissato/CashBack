using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CashBack.Api.Logging
{
    public class CustomLogger: ILogger
    {
        private readonly string _loggerName;
        private readonly CustomerLoggerProviderConfiguration _loggerConfig;

        public CustomLogger(string loggerName, CustomerLoggerProviderConfiguration loggerConfig)
        {
            _loggerName = loggerName;
            _loggerConfig = loggerConfig;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var log = $"{logLevel}: {eventId.Id} - {formatter(state, exception)}";

            WriteLogToFile(log);
        }

        private static void WriteLogToFile(string log)
        {
            //TODO Implement database log saving
            //TODO Move implementation to service
            const string filePath = @"C:\Temp\Application_log.txt";
            using var st = new StreamWriter(filePath, true);
            st.WriteLine(log);
            st.Close();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
