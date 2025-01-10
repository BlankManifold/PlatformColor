using Godot;

namespace PlatFormColor.scripts.Player
{
    [GlobalClass]
    public partial class WallJumpState : Node, Interfaces.IState
    {
        public event Interfaces.Notify RequestTransition;

        [Export(PropertyHint.NodePathToEditedNode)]
        public CharacterBody2D _controlledNode = null;

        [Export]
        public string StateName { get; set; }

        [Export(PropertyHint.Range, "0, 2000, 50")]
        public float JumpAcceleration { get; set; }


        public void Enter(string prevStateName = null)
        {
            Vector2 jumpDirection;

            if (_controlledNode.IsOnWall())
                jumpDirection = _controlledNode.GetWallNormal();
            else if (_controlledNode.IsOnCeiling())
                jumpDirection = -_controlledNode.UpDirection;
            else
                return;

            _controlledNode.Velocity += JumpAcceleration * jumpDirection;
            return;
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
                return;
            }

            ProcessInput();
        }

        public void Process(double delta)
        {
            return;
        }

        private void ProcessInput()
        {
            if (Globals.InputChecker.MovePressed())
            {
                RequestTransition?.Invoke("Move");
                return;
            }
        }
    }
}