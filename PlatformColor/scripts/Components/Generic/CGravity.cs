using System.Linq;
using Godot;

namespace PlatFormColor.scripts.Components.Generic
{
    [GlobalClass]
    public partial class CGravity : CDynamicBase
    {
        protected float _weight = 0f;

        [Export]
        protected CharacterBody2D _controlledNode = null;
        [Export]
        protected CWeight _CWeight = null;

        public override string[] _GetConfigurationWarnings()
        {
            string[] warnings = null;
            warnings = base._GetConfigurationWarnings();

            if (_controlledNode == null)
                _ = warnings.Append<string>("Must assign a CharacterBody2D to apply gravity on it.");
            if (_CWeight == null)
                _ = warnings.Append<string>("Must assign a CWeight component to get weight from it.");

            return warnings;
        }

        public override void _Ready()
        {
            base._Ready();
            _weight = _CWeight.GetWeight();
        }

        public override void Apply(double delta)
        {
            if (!_active)
                return;

            Vector2 velocity = _controlledNode.Velocity;
            velocity += _controlledNode.GetGravity() * _weight * (float)delta;

            _controlledNode.Velocity = velocity;
        }
    }
}