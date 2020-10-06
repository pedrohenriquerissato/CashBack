using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CashBack.Api.Logging
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        private readonly CustomerLoggerProviderConfiguration _loggerConfiguration;
        readonly ConcurrentDictionary<string, CustomLogger> loggers = new ConcurrentDictionary<string, CustomLogger>();

        public CustomLoggerProvider(CustomerLoggerProviderConfiguration loggerConfiguration)
        {
            _loggerConfiguration = loggerConfiguration;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return loggers.GetOrAdd(categoryName, name => new CustomLogger(name, _loggerConfiguration));
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
