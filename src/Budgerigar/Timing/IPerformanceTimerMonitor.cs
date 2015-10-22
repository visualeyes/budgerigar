using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgerigar.Timing {
    public interface IPerformanceTimerMonitor {
        string Name { get; }
        IDisposable Step(string name);
        PerformanceTimerResult Stop();
    }
}
