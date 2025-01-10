using System.Linq;
using Godot;

namespace PlatFormColor.scripts.Components.Generic
{
    [GlobalClass]
    public partial class CGravityField : CBase
    {
        [Export]
        protected float _intensity = 100f;
        [Export]
        protected Shape2D _fieldShape = null;
        [Export]
        protected Node2D _sourceNode = null;
        protected Area2D _fieldArea = null;
        protected Label _label = null;
        private CharacterBody2D _interactionBody = null;

        public override string[] _GetConfigurationWarnings()
        {
            string[] warnings = null;
            warnings = base._GetConfigurationWarnings();

            if (_sourceNode == null)
                _ = warnings.Append<string>("Must assign a Node2D representing the source of the field.");
            if (_fieldShape == null)
                _ = warnings.Append<string>("Must assign a Shape2D representing the field area of interaction.");

            return warnings;
        }
        public override void _Ready()
        {
            base._Ready();

            _label = GetNode<Label>("Label");
            _label.GlobalPosition = _sourceNode.GlobalPosition;

            _fieldArea = GetNode<Area2D>("Area2D");
            _fieldArea.GravityPoint = true;
            _fieldArea.GravitySpaceOverride = Area2D.SpaceOverride.Replace;
            _fieldArea.GlobalPosition = _sourceNode.GlobalPosition;
            _fieldArea.GravityPointCenter = new Vector2(0, 0);
            _fieldArea.Gravity = _intensity;
            _fieldArea.GetNode<CollisionShape2D>("CollisionShape2D").Shape = _fieldShape;
            //TODO così è forza costante (come anche world gravity) se voglio 1/r^2 cambiare UnitDistance 

            _fieldArea.BodyEntered += _OnBodyEntered;
            _fieldArea.BodyExited += _OnBodyExited;
        }

        private void _OnBodyEntered(Node2D body)
        {
            _label.Text = body.Name;
            if (body is CharacterBody2D characterBody)
            {
                _interactionBody = characterBody;
            }

        }
        private void _OnBodyExited(Node2D body)
        {
            _label.Text = "";
            _interactionBody = null;
        }
    }
}