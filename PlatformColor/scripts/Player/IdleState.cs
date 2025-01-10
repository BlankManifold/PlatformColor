using Godot;

namespace PlatFormColor.scripts.Player
{
    [GlobalClass]
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

            if (Globals.InputChecker.MovePressed())
            {
                RequestTransition?.Invoke("Move");
                return;
            }
        }

    }
}