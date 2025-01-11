using Godot;

namespace PlatFormColor.scripts.Components.Generic
{
    [GlobalClass]
    public partial class CGravityPointField : CGravityField
    {
        public override void _Ready()
        {
            base._Ready();

            _fieldArea.GravityPoint = true;
            _fieldArea.GravitySpaceOverride = Area2D.SpaceOverride.Replace;
            _fieldArea.GlobalPosition = _sourceNode.GlobalPosition;
            _fieldArea.GravityPointCenter = new Vector2(0, 0);
            _fieldArea.Gravity = _intensity;
            _fieldArea.GetNode<CollisionShape2D>("CollisionShape2D").Shape = _fieldShape;
            //TODO così è forza costante (come anche world gravity) se voglio 1/r^2 cambiare UnitDistance 
        }
    }
}