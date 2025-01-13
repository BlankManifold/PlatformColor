using Godot;

namespace PlatFormColor.scripts.PropertyModifiers
{
    public abstract partial class MBase : Node2D
    {
        [Export]
        protected Shape2D _shape;

        public override void _Ready()
        {
            base._Ready();
            Area2D area2D = GetNode<Area2D>("%Area2D");
            area2D.GetNode<CollisionShape2D>("CollisionShape2D").Shape = _shape;

            area2D.BodyEntered += _OnBodyEntered;
            area2D.BodyExited += _OnBodyExited;
        }

        protected abstract void _OnBodyEntered(Node2D body);
        protected abstract void _OnBodyExited(Node2D body);
    }

}