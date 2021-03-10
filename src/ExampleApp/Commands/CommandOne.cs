using System;
using System.ComponentModel;
using System.Threading.Tasks;
using EasyConsole;

namespace MyProgram.Commands
{
    [Description("Run command one")]
    public class CommandOne : CommandBase
    {
        public override async Task ExecuteAsync(IServiceProvider provider)
        {
            Console.WriteLine($"{nameof(CommandOne)} : Waiting 3 seconds");
            await Task.Delay(3000);
        }
    }
}
