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

        private int _direction = 0;

        public void Enter(string prevStateName = null)
        {
            _UpdateDirection();
            return;
        }

        public void Exit(string nextStateName)
        {
            return;
        }

        public void PhysicsProcess(double delta)
        {
            Vector2 velocity = _controlledNode.Velocity;
            velocity.X = _direction * Speed;
            _controlledNode.Velocity = velocity;

            _ProcessInput();
            return;
        }

        public void Process(double delta)
        {
            return;
        }

        private void _ProcessInput()
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

            _UpdateDirection();

            return;
        }
        private void _UpdateDirection()
        {
            if (Input.IsActionJustPressed("player_move_right"))
            {
                _direction = 1;
                return;
            }

            if (Input.IsActionJustPressed("player_move_left"))
            {
                _direction = -1;
                return;
            }

            if (Input.IsActionJustReleased("player_move_right") && Input.IsActionPressed("player_move_left"))
            {
                _direction = -1;
                return;
            }
            if (Input.IsActionJustReleased("player_move_left") && Input.IsActionPressed("player_move_right"))
            {
                _direction = 1;
                return;
            }



            return;
        }

        private bool _JumpPressed() => Input.IsActionJustPressed("player_jump") && _controlledNode.IsOnFloor();
        private bool _MoveNotPressed() => !(Input.IsActionPressed("player_move_right") || Input.IsActionPressed("player_move_left"));
    }
}