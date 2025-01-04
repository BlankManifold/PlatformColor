using Godot;

namespace PlatFormColor.scripts.Player
{
    public partial class SquarePlayer : Player
    {
        private Resources.SquarePlayerRes _promotedRes = null;
        public override void _Ready()
        {
            base._Ready();

            _promotedRes = (Resources.SquarePlayerRes)_res;

            ColorRect rect = GetNode<ColorRect>("ColorRect");
            rect.Size = _promotedRes.Size;
            rect.Position -= rect.Size / 2.0f;

            RectangleShape2D shape = (RectangleShape2D)GetNode<CollisionShape2D>("CollisionShape2D").Shape;
            shape.Size = _promotedRes.Size;

        }
        public override void _PhysicsProcess(double delta)
        {
            base._PhysicsProcess(delta);
        }
        public override void AddProperty(Globals.Property property, Variant value)
        {
            base.AddProperty(property, value);

            if (property is Globals.Property.Color)
                GetNode<ColorRect>("ColorRect").Color = (Color)value;
        }
        public override void SetProperty(Globals.Property property, Variant value)
        {
            base.SetProperty(property, value);

            if (property is Globals.Property.Color)
                GetNode<ColorRect>("ColorRect").Color = (Color)value;
        }
    }
}