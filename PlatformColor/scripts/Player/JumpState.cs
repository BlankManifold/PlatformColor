using Godot;

namespace PlatFormColor.scripts.Player
{
    public partial class JumpState : Node, Interfaces.IState
    {
        public event Interfaces.Notify RequestTransition;

        [Export(PropertyHint.NodePathToEditedNode)]
        public CharacterBody2D _controlledNode = null;

        [Export]
        public string StateName { get; set; }


        public void Enter(string prevStateName = null)
        {
            if (prevStateName == "Idle")
            {
                Vector2 velocity = _controlledNode.Velocity;
                velocity.Y -= 100;

                _controlledNode.Velocity = velocity;
                return;
            }
        }

        public void Exit(string nextStateName)
        {
            return;
        }

        public void PhysicsProcess(double delta)
        {
            if (_controlledNode.IsOnFloor())
            {
                RequestTransition?.Invoke("Idle");
            }
        }

        public void Process(double delta)
        {
            return;
        }
    }
}