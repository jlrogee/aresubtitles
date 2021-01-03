using System;
using System.Threading.Tasks;

namespace Debug.Commands
{
    public class TestCommand : DebugCommand
    {
        public override string Name => "t";

        public async override Task Execute()
        {
            Console.WriteLine("hi");
        }
    }
}