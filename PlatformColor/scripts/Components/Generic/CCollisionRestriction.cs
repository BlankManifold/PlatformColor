using System.Linq;
using Godot;

namespace PlatFormColor.scripts.Components.Generic
{
    public abstract partial class CCollisionRestriction<T> : CDynamicBase where T : PhysicsBody2D
    {
        [Export]
        protected CharacterBody2D _controlledNode = null;

        protected Vector2? _lastValidPosition;

        public override string[] _GetConfigurationWarnings()
        {
            string[] warnings = null;
            warnings = base._GetConfigurationWarnings();

            if (_controlledNode == null)
                _ = warnings.Append<string>("Must assign a CharacterBody2D to apply restriction on it.");

            return warnings;
        }
        public override void _Ready()
        {
            base._Ready();
            _ConnectReportCollsion();
        }

        public override void Apply(double delta)
        {
            if (_controlledNode.IsOnFloor())
                _lastValidPosition = _controlledNode.GlobalPosition;
        }

        public void _OnCollided(T collider)
        {
            if (_IsCollisionAllowed(collider))
                return;

            _controlledNode.GlobalPosition = (Vector2)_lastValidPosition;
            _controlledNode.Velocity = new(0, 0);
        }
        protected abstract bool _IsCollisionAllowed(T collider);
        protected abstract void _ConnectReportCollsion();
    }
}