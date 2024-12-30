using System;
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

        [Export(PropertyHint.Range, "0, 2000, 50")]
        public float JumpVelocity { get; set; }


        public void Enter(string prevStateName = null)
        {
            if (prevStateName == "Idle" || prevStateName == "Move")
            {
                Vector2 velocity = _controlledNode.Velocity;
                velocity.Y -= JumpVelocity;

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
            if (_MovePressed())
            {
                RequestTransition?.Invoke("Move");
                return;
            }
        }
        private bool _MovePressed() => Input.IsActionPressed("player_move_right") || Input.IsActionPressed("player_move_left");
    }
}