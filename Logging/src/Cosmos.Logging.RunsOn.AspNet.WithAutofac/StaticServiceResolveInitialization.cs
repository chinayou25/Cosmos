﻿using System;
using Cosmos.Logging.Core;

namespace Cosmos.Logging.RunsOn.AspNet.WithAutofac {
    internal class StaticServiceResolveInitialization {
        public StaticServiceResolveInitialization(ILoggingServiceProvider loggingServiceProvider) {
            if (loggingServiceProvider == null) throw new ArgumentNullException(nameof(loggingServiceProvider));
            StaticServiceResolver.SetResolver(loggingServiceProvider);
        }
    }
}