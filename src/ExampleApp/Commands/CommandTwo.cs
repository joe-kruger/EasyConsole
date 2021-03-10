using System;
using System.Threading.Tasks;
using EasyConsole;
using MyProgram.Services;

namespace MyProgram.Commands
{
    public class CommandTwo : CommandBase
    {
        private readonly IWaitService _waitService;

        public CommandTwo(IWaitService waitService)
        {
            _waitService = waitService;
        }

        public override async Task ExecuteAsync(IServiceProvider provider)
        {
            Console.WriteLine($"{nameof(CommandTwo)} : Waiting 3 seconds using {nameof(IWaitService)}");
            await _waitService.Wait();
        }
    }
}