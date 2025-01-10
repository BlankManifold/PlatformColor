using System.Linq;
using Godot;

namespace PlatFormColor.scripts.Components.Generic
{
    [GlobalClass]
    public partial class CGravity : CDynamicBase
    {
        protected float _weight = 0f;
        protected double _alphaReduced = 0f;
        protected bool _hasDrag = false;

        [Export(PropertyHint.Range, "0, 4000, 50")]
        protected float _terminalVelocity;
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
            if (_terminalVelocity != 0)
            {
                _hasDrag = true;
                _alphaReduced = _weight / Mathf.Pow(_terminalVelocity, 2);
            }
        }

        public override void Apply(double delta)
        {
            if (!_active)
                return;

            Vector2 velocity = _controlledNode.Velocity;
            Vector2 gravity = _controlledNode.GetGravity();

            velocity += gravity * _weight * (float)delta;

            if (!_hasDrag)
            {
                _controlledNode.Velocity = velocity;
                return;
            }

            velocity += DragForce(gravity) * (float)delta;
            _controlledNode.Velocity = velocity;
        }

        private Vector2 DragForce(Vector2 gravity)
        {
            float velocityComponent = _controlledNode.Velocity.Dot(gravity.Normalized());
            if (velocityComponent <= 0f)
            {
                return new Vector2(0, 0);
            }

            double alpha = gravity.Length() * _alphaReduced;

            return -(float)alpha * Mathf.Pow(velocityComponent, 2) * gravity.Normalized();
        }
    }
}