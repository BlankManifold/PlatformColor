using System.Linq;
using Godot;

namespace PlatFormColor.scripts.Components.Generic
{
    [GlobalClass]
    public partial class CFriction : CDynamicBase
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
        public override void _Ready()
        {
            base._Ready();
            if (_controlledNode is Interfaces.IEntityWithProperties _controlledEntity)
            {
                _controlledEntity.AddProperty(Globals.Property.Friction, _friction);
            }
            else
            {
                throw new System.Exception($"Cannot add Fricion component because {_controlledNode.Name} is not a IEntityWithProperties.");
            }
        }

        public override void Apply(double delta)
        {
            if (!_active)
                return;

            if (Globals.InputChecker.MovePressed())
                return;

            Vector2 velocity = _controlledNode.Velocity;
            velocity.X = Mathf.MoveToward(_controlledNode.Velocity.X, 0, _friction * _frictionFactor);

            _controlledNode.Velocity = velocity;
        }
    }
}