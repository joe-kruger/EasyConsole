using System;
using System.Threading.Tasks;
using EasyConsole;
using Microsoft.Extensions.DependencyInjection;
using MyProgram.Services;

namespace MyProgram
{
    class Program 
    {
        static Task Main(string[] args) => new ConsoleApp(ConfigureOptions, ConfigureServices).Run(args);
        

        public static void ConfigureOptions(ConsoleOptions options)
        {
            options.ClearScreenBeforeRun = true;
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IWaitService, WaitService>();
        }
    }
}
