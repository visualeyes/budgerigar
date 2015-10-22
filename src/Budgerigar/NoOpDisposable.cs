using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgerigar {
    internal class NoOpDisposable : IDisposable {
        public void Dispose() {}
    }
}
