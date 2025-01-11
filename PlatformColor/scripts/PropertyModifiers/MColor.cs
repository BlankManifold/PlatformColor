using Godot;

namespace PlatFormColor.scripts.PropertyModifiers
{
    public partial class MColor : Node2D
    {
        [Export]
        private Color _color = new Color(1, 1, 1, 1);
        [Export]
        private Shape2D _shape;

        public override void _Ready()
        {
            base._Ready();
            Area2D area2D = GetNode<Area2D>("%Area2D");
            area2D.GetNode<CollisionShape2D>("CollisionShape2D").Shape = _shape;

            area2D.BodyEntered += _OnBodyEntered;
            area2D.BodyExited += _OnBodyExited;
        }

        private void _OnBodyEntered(Node2D body)
        {
            if (body is Interfaces.IEntityWithProperties entity)
            {
                entity.SetProperty(Globals.Property.Color, _color);
            }

        }
        private void _OnBodyExited(Node2D body)
        {
        }
    }

}