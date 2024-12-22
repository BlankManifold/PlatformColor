using Godot;

namespace PlatFormColor.scripts.Player
{
    public class MoveState : Interfaces.IState
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
            return;
        }

        public void Process(double delta)
        {
            return;
        }
    }
}