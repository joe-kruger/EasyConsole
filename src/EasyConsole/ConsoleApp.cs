using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;


namespace EasyConsole
{
    public class ConsoleApp
    {
        private readonly ServiceProvider _provider;
        private readonly ConsoleOptions _options;

        #region Constructor Overrides
        public ConsoleApp()
        {
            _options = BuildConsoleOptions();
            _provider = BuildServiceProvider();
        }

        public ConsoleApp(Action<ConsoleOptions> optionsAction)
        {
            _options = BuildConsoleOptions(optionsAction);
            _provider = BuildServiceProvider(null);
        }

        public ConsoleApp(Action<IServiceCollection> serviceAction)
        {
            _options = BuildConsoleOptions(null);
            _provider = BuildServiceProvider(serviceAction);
        }

        public ConsoleApp(Action<ConsoleOptions> optionsAction, Action<IServiceCollection> serviceAction)
        {
            _options = BuildConsoleOptions(optionsAction);
            _provider = BuildServiceProvider(serviceAction);
        } 
        #endregion

        public async Task Run(string[] args)
        {
            var commands = GetCommands();
            if(!DisplayMenu(commands)) return;

            while (true)
            {
                var input = Console.ReadLine();
                if (input?.ToLower().Trim().StartsWith("x") ?? false) break;
                if (!int.TryParse(input, out var choice) || choice < 1 || choice > commands.Length)
                {
                    await Console.Error.WriteLineAsync($"Please enter a number between 1 and {commands.Length}");
                    Console.Write("Select number of command you wish to run : ");
                    continue;
                }

                var stopwatch = Stopwatch.StartNew();
                var cmdType = commands[choice - 1];
                var command = (CommandBase )ActivatorUtilities.CreateInstance(_provider, cmdType);
                
                if(_options.ClearScreenBeforeRun) Console.Clear();

                try
                {
                    await command.ExecuteAsync(_provider);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.GetType().Name} : {ex.Message}");
                    Console.WriteLine("StackTrace:");
                    Console.WriteLine(ex.StackTrace);
                }

                stopwatch.Stop();
                Console.WriteLine($"\n\nCommand finished in {stopwatch.Elapsed:c}\n");

                if (_options.ClearScreenBeforeShowingMenu)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }

                DisplayMenu(commands);
            }

            
            

        }

        private bool DisplayMenu(Type[] types)
        {
            if (!types.Any())
            {
                Console.WriteLine("No Commands!  Please create at least one class that extends CommandBase.");
                return false;
            }
            if(_options.ClearScreenBeforeShowingMenu) Console.Clear();

            Console.WriteLine("Please select an option: \n");

            var count = 0;
            foreach (var type in types)
            {
                var descriptionAttr = type.GetCustomAttributes(typeof(DescriptionAttribute), false).Cast<DescriptionAttribute>().ToArray();
                var desc = descriptionAttr.Any() ? descriptionAttr.First().Description : type.Name;
                Console.WriteLine($"  {++count}\t{desc}");
            }
            Console.WriteLine($"  x\tExit App");

            Console.Write("\nSelect number of command you wish to run : ");

            return true;

        }


        private ConsoleOptions BuildConsoleOptions(Action<ConsoleOptions> optionsAction = null)
        {
            var options = new ConsoleOptions();
            optionsAction?.Invoke(options);
            return options;
        }

        private ServiceProvider BuildServiceProvider(Action<IServiceCollection> serviceAction = null)
        {
            var services = new ServiceCollection();
            serviceAction?.Invoke(services);
            return services.BuildServiceProvider();
            
        }

        private Type[] GetCommands()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(asm => asm.GetTypes().Where(t => typeof(CommandBase).IsAssignableFrom(t) && !t.IsAbstract));

            return types.ToArray();
        }
    }
}
