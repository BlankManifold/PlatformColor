using System.Linq;
using Godot;

namespace PlatFormColor.scripts.Components.Generic
{
    [GlobalClass]
    public partial class CGravity : CBase
    {
        [Export]
        protected float _weight = 0f;

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
            Vector2 velocity = _controlledNode.Velocity;
            velocity += _controlledNode.GetGravity() * _weight * (float)delta;

            _controlledNode.Velocity = velocity;
        }
    }
}