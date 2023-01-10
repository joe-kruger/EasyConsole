using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasyConsole
{
    public abstract class CommandBase : IDisposable
    {
        public abstract Task ExecuteAsync(IServiceProvider provider);
        public virtual void Dispose()
        {
            //nop
        }
    }
}
