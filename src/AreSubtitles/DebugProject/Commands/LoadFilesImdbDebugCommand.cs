using System;
using System.Threading.Tasks;
using Debug.Commands;

namespace Debug
{
    public class LoadFilesImdbDebugCommand : DebugCommand
    {
        public override string Name => "parse";
        public async override Task Execute()
        {
            Console.WriteLine("parse");
        }
    }
}