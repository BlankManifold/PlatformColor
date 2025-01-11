using Godot;

namespace PlatFormColor.scripts.Components.Generic
{
    [GlobalClass]
    public partial class CGravityPlaneField : CGravityField
    {
        [Export]
        private Vector2 _gravityDirection;
        public override void _Ready()
        {
            base._Ready();

            _fieldArea.GravitySpaceOverride = Area2D.SpaceOverride.Replace;
            _fieldArea.GlobalPosition = _sourceNode.GlobalPosition;
            _fieldArea.GravityDirection = _gravityDirection.Normalized();
            _fieldArea.Gravity = _intensity;
            _fieldArea.GetNode<CollisionShape2D>("CollisionShape2D").Shape = _fieldShape;
        }
    }
}