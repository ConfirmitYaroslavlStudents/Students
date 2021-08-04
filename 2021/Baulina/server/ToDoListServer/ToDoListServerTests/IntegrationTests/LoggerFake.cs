using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace ToDoListServerTests.IntegrationTests
{
    public class LoggerFake : ILogger
    {
        public List<string> Message { get; } = new();

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Message.Add(formatter(state, exception));
        }

        public bool IsEnabled(LogLevel logLevel) => true;

        public IDisposable? BeginScope<TState>(TState state) => null;
    }
}
