﻿using Budgerigar.Timing;
using Budgerigar.Timing.StopWatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Budgerigar.Tests.Timing {
    public class PerformanceTimerExtensionsFacts {

        [Fact]
        public void Returns_Null_If_Timer_Null() {
            PerformanceTimerProviderFactory timer = null;
            var monitor = timer.Start("test");
            Assert.Null(monitor);
        }

        [Fact]
        public void Returns_Null_If_Provider_Null() {
            var timer = new PerformanceTimerProviderFactory(null);
            var monitor = timer.Start("test");
            Assert.Null(monitor);
        }

        [Fact]
        public void Returns_If_Timer_NotNull() {
            var stopwatchProvider = new StopwatchTimerProvider();
            var timer = new PerformanceTimerProviderFactory(stopwatchProvider);
            var monitor = timer.Start("test");
            Assert.NotNull(monitor);
        }
    }
}
