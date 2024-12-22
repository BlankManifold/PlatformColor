namespace PlatFormColor.scripts.Interfaces
{
    public delegate void Notify(string nextStateName);

    public interface IState
    {
        public event Notify RequestTransition;
        public string StateName { get; set; }
        public void Enter(string prevStateName = null);
        public void Exit(string nextStateName = null);
        public void Process(double delta);
        public void PhysicsProcess(double delta);
    }
}