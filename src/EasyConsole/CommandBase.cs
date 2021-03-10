using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasyConsole
{
    public abstract class CommandBase
    {
        public abstract Task ExecuteAsync(IServiceProvider provider);
    }
}
