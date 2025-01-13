using Godot;

namespace PlatFormColor.scripts.PropertyModifiers
{
    public partial class MColor : MBase
    {
        [Export]
        private Color _color = new(1, 1, 1, 1);

        public override void _Ready()
        {
            base._Ready();
        }

        protected override void _OnBodyEntered(Node2D body)
        {
            if (body is Interfaces.IEntityWithProperties entity)
            {
                entity.SetProperty(Globals.Property.Color, _color);
            }
        }
        protected override void _OnBodyExited(Node2D body)
        {
        }
    }

}