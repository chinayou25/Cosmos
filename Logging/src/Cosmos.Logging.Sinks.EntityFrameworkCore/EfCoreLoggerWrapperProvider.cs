﻿using System;
using Cosmos.Logging.Events;
using Cosmos.Logging.RunsOn.AspNetCore.Core;
using Microsoft.Extensions.Logging;

namespace Cosmos.Logging.Sinks.EntityFrameworkCore {
    internal class EfCoreLoggerWrapperProvider : ILoggerProvider {
        private readonly ILoggingServiceProvider _loggingServiceProvider;
        private readonly Func<string, LogEventLevel, bool> _filter;
        private static readonly Func<string, LogEventLevel, bool> trueFilter = (cat, level) => true;
        private static readonly Func<string, LogEventLevel, bool> falseFilter = (cat, level) => false;

        public EfCoreLoggerWrapperProvider(ILoggingServiceProvider loggingServiceProvider) {
            _loggingServiceProvider = loggingServiceProvider ?? throw new ArgumentNullException(nameof(loggingServiceProvider));
            _filter = trueFilter;
        }

        public EfCoreLoggerWrapperProvider(ILoggingServiceProvider loggingServiceProvider, Func<string, LogEventLevel, bool> filter) {
            _loggingServiceProvider = loggingServiceProvider ?? throw new ArgumentNullException(nameof(loggingServiceProvider));
            _filter = filter ?? trueFilter;
        }

        public EfCoreLoggerWrapperProvider(ILoggingServiceProvider loggingServiceProvider, Func<string, LogLevel, bool> filter) {
            _loggingServiceProvider = loggingServiceProvider ?? throw new ArgumentNullException(nameof(loggingServiceProvider));
            _filter = As(filter) ?? trueFilter;
        }

        public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName) {
            return new EfCoreLoggerWrapper(_loggingServiceProvider.GetLogger(categoryName, _filter, LogEventSendMode.Automatic));
        }

        public void Dispose() { }

        private static Func<string, LogEventLevel, bool> As(Func<string, LogLevel, bool> originFilter) {
            if (originFilter == null) return null;
            return (s, l) => originFilter(s, LogLevelSwitcher.Switch(l));
        }
    }

}