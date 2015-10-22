using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgerigar.Timing {
    public class PerformanceTimerProviderFactory : IPerformanceTimerProviderFactory {
        private IPerformanceTimerProvider provider;

        public PerformanceTimerProviderFactory(IPerformanceTimerProvider provider) {
            this.provider = provider;
        }

        public IPerformanceTimerProvider GetProvider() {
            return provider;
        }
    }
}
