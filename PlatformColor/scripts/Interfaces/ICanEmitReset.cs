namespace PlatFormColor.scripts.Interfaces
{
    public delegate void NotifyAction();
    public interface ICanEmitReset
    {
        public event NotifyAction Reset;
    }

}