using Godot;

namespace PlatFormColor.scripts.Player
{
    public partial class MoveState : Node, Interfaces.IState
    {
        public event Interfaces.Notify RequestTransition;

        [Export(PropertyHint.NodePathToEditedNode)]
        public CharacterBody2D _controlledNode = null;

        [Export]
        public string StateName { get; set; }

        [Export(PropertyHint.Range, "0, 1000, 50")]
        public float Speed { get; set; }

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
            float direction = Mathf.Sign(Input.GetAxis("player_move_left", "player_move_right"));

            if (direction != 0)
            {
                Vector2 velocity = _controlledNode.Velocity;
                velocity.X = direction * Speed;
                _controlledNode.Velocity = velocity;
            }


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
            if (_MoveNotPressed())
            {
                RequestTransition?.Invoke("Idle");
                return;
            }

            return;
        }
        private bool _JumpPressed() => Input.IsActionJustPressed("player_jump") && _controlledNode.IsOnFloor();
        private bool _MoveNotPressed() => !(Input.IsActionPressed("player_move_right") || Input.IsActionPressed("player_move_left"));
    }
}