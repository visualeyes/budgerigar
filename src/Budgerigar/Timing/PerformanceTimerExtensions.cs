using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgerigar.Timing {
    public static class PerformanceTimerExtensions {
        
        public static IPerformanceTimerMonitor Start(this IPerformanceTimerProviderFactory timer, string name) {
            var provider = timer?.GetProvider();

            if (provider != null) {
                return provider.Start(name);
            } else {
                return null;
            }
        }
    }
}
