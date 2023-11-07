namespace ProjectTarnished.Interfaces
{
    public interface ICommand
    {
        public void Execute();
        public bool IsFinished();
    }
}