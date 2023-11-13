using ProjectTarnished.Capabilities;

namespace ProjectTarnished.Interfaces
{
    public interface ICommandable
    {
        public void AddCommands(Commandable commandable);
    }
}