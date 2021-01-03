using System.Threading.Tasks;

namespace Debug.Commands
{
    public abstract class DebugCommand
    {
        public abstract string Name { get; }
        public abstract Task Execute();
    }
}