using System.Linq;
using Godot;

namespace PlatFormColor.scripts.Components.Generic
{
    [GlobalClass]
    public partial class CFriction2 : CBase
    {
        [Export]
        protected float _friction = 0f;
        protected float _frictionFactor = 1f;

        [Export]
        protected CharacterBody2D _controlledNode = null;

        public override string[] _GetConfigurationWarnings()
        {
            string[] warnings = null;
            warnings = base._GetConfigurationWarnings();

            if (_controlledNode == null)
                _ = warnings.Append<string>("Must assign a CharacterBody2D to apply friction on it.");

            return warnings;
        }

        public override void Apply(double delta)
        {
            bool movePressed = Input.IsActionPressed("player_move_left") || Input.IsActionPressed("player_move_right");

            if (movePressed)
                return;

            Vector2 velocity = _controlledNode.Velocity;
            velocity.X = Mathf.MoveToward(_controlledNode.Velocity.X, 0, _friction * _frictionFactor);

            _controlledNode.Velocity = velocity;
        }
    }
}