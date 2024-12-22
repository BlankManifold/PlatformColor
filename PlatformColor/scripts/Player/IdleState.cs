using Godot;

namespace PlatFormColor.scripts.Player
{
    public partial class IdleState : Node, Interfaces.IState
    {
        public event Interfaces.Notify RequestTransition;

        [Export(PropertyHint.NodePathToEditedNode)]
        public CharacterBody2D _controlledNode = null;
        [Export]
        public string StateName { get; set; }


        public void Enter(string prevStateName = null)
        {
            return;
        }
        public void Exit(string nextStateName)
        {
            return;
        }
        public void PhysicsProcess(double delta)
        {
            ProcessInput();
            return;
        }
        public void Process(double delta)
        {
            return;
        }

        private void ProcessInput()
        {
            if (_JumpPressed())
            {
                RequestTransition?.Invoke("Jump");
                return;
            }

            if (_MovePressed())
            {
                RequestTransition?.Invoke("Move");
                return;
            }
        }
        private bool _JumpPressed() => Input.IsActionJustPressed("player_jump") && _controlledNode.IsOnFloor();
        private bool _MovePressed() => Input.IsActionJustPressed("player_move_right") || Input.IsActionJustPressed("player_move_left");
    }
}