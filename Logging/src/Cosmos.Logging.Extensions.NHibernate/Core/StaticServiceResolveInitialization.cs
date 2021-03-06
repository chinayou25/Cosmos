﻿using System;

namespace Cosmos.Logging.Extensions.NHibernate.Core {
    internal class StaticServiceResolveInitialization {
        public StaticServiceResolveInitialization(ILoggingServiceProvider loggingServiceProvider, NhSinkOptions settings) {
            if (loggingServiceProvider == null) throw new ArgumentNullException(nameof(loggingServiceProvider));
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            Logging.Core.StaticServiceResolver.SetResolver(loggingServiceProvider);
            global::NHibernate.LoggerProvider.SetLoggersFactory(
                new NHibernateLoggerWrapperFactory(loggingServiceProvider, settings.GetRenderingOptions(), settings.Filter));
        }
    }
}