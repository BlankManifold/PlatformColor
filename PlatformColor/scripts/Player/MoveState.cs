using Godot;

namespace PlatFormColor.scripts.Player
{
    [GlobalClass]
    public partial class MoveState : Node, Interfaces.IState
    {
        public event Interfaces.Notify RequestTransition;

        [Export(PropertyHint.NodePathToEditedNode)]
        public CharacterBody2D _controlledNode = null;

        [Export]
        public string StateName { get; set; }

        [Export(PropertyHint.Range, "0, 1000, 50")]
        public float GroundAccelaration { get; set; }
        [Export(PropertyHint.Range, "0, 1000, 50")]
        public float AirealAccelaration { get; set; }
        [Export(PropertyHint.Range, "0, 1000, 50")]
        public float MaxGroundSpeed { get; set; }
        [Export(PropertyHint.Range, "0, 1000, 50")]
        public float MaxAirealSpeed { get; set; }


        private float _acceleration = 0;
        private float _maxSpeed = 0;
        private int _direction = 0;

        public void Enter(string prevStateName = null)
        {
            _UpdateDirection();
        }

        public void Exit(string nextStateName)
        {

            return;
        }

        public void PhysicsProcess(double delta)
        {
            _ProcessInput();
            _UpdateTypeOfMovement();

            Vector2 velocity = _controlledNode.Velocity;
            velocity.X += _direction * _acceleration * (float)delta;

            //TODO non è corretto così non posso cambiare direzione se...
            if (Mathf.Abs(velocity.X) > _maxSpeed && _direction * velocity.X > 0)
                return;

            _controlledNode.Velocity = velocity;
        }

        public void Process(double delta)
        {
            return;
        }

        private void _ProcessInput()
        {
            if (Globals.InputChecker.JumpPressed(_controlledNode))
            {
                RequestTransition?.Invoke("Jump");
                return;
            }
            if (Globals.InputChecker.WallJumpPressed(_controlledNode))
            {
                RequestTransition?.Invoke("WallJump");
                return;
            }
            if (Globals.InputChecker.MoveNotPressed())
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

            int direction = (int)Input.GetAxis("player_move_left", "player_move_right");
            if (direction != 0)
            {
                _direction = direction;
                return;
            }

            return;
        }
        private void _UpdateTypeOfMovement()
        {
            if (_controlledNode.IsOnFloor())
            {
                _acceleration = GroundAccelaration;
                _maxSpeed = MaxGroundSpeed;
            }
            else
            {
                _acceleration = AirealAccelaration;
                _maxSpeed = MaxAirealSpeed;
            }
        }

    }
}