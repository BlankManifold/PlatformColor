namespace PlatFormColor.scripts.Interfaces
{
    public interface IState
    {
        public void Enter();
        public void Exit();
        public void Update(float delta);
        public void PhysicsUpdate(float delta);
    }
}