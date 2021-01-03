#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Debug.Commands;

namespace Debug
{
    class Program
    {
        private static IContainer Container { get; set; }
        private static readonly DbgCmdFactory Factory = new();
        const string CMD_SYMBOLS = "$ ";
        const string EXIT_CODE = "q";

        
        static async Task Main(string[] args)
        {
            RegisterCommands();

            while (true)
            {
                Console.Write(CMD_SYMBOLS);
                var cmdName = Console.ReadLine();
                if (cmdName == EXIT_CODE)
                    break;
                
                if (string.IsNullOrEmpty(cmdName)) 
                    continue;
                
                var command = Factory.Get(cmdName!);
                if (command == null)
                {
                    Console.WriteLine("Command not found");
                    continue;
                }

                Console.WriteLine("Cmd started");
                await command.Execute();
                Console.WriteLine("Cmd finished");
            }
        }

        private static void RegisterCommands()
        {
            var builder = new ContainerBuilder();

            var commandTypes =
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(x => x.DefinedTypes)
                    .Select(x => x.AsType())
                    .Where(x => !x.IsAbstract && x.IsSubclassOf(typeof(DebugCommand)))
                    .ToList();

            foreach (var dbgCType in commandTypes)
            {
                builder
                    .RegisterType(dbgCType)
                    .AsSelf()
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
            }
            
            Container = builder.Build();

            Factory.MakeCommandsFactory(commandTypes, Container);
        }
    }

    internal class DbgCmdFactory
    {
        private readonly Dictionary<string, DebugCommand> _instances = new();

        public  DebugCommand? Get(string cmdName)
        {
            return _instances.ContainsKey(cmdName!) ? _instances[cmdName] : null;
        }
        
        public void MakeCommandsFactory(IEnumerable<Type> commandTypes, IContainer container)
        {
            foreach (var cmdInstance in commandTypes.Select(x => (DebugCommand) container.Resolve(x)))
            {
                _instances[cmdInstance.Name] = cmdInstance;
            }
        }
    }
}