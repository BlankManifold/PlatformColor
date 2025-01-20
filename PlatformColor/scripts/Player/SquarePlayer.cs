using Godot;
using GCs = Godot.Collections;

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
        public override void AddProperty(Globals.Property property, Variant value, bool changeable = true)
        {
            base.AddProperty(property, value, changeable);

            switch (property)
            {
                case Globals.Property.Color:
                    GetNode<ColorRect>("ColorRect2").Color = (Color)value;
                    break;
                case Globals.Property.Colors:
                    GCs::Array<Color> colors = (GCs::Array<Color>)value;
                    GetNode<ColorRect>("ColorRect2").Color = colors[0];
                    GetNode<ColorRect>("ColorRect").Color = colors[1];
                    break;
                default:
                    break;
            }
        }
        public override void SetProperty(Globals.Property property, Variant value)
        {
            base.SetProperty(property, value);

            switch (property)
            {
                case Globals.Property.Color:
                    GetNode<ColorRect>("ColorRect2").Color = (Color)value;
                    break;
                case Globals.Property.Colors:
                    GCs::Array<Color> colors = (GCs::Array<Color>)value;
                    GetNode<ColorRect>("ColorRect2").Color = colors[0];
                    GetNode<ColorRect>("ColorRect").Color = colors[1];
                    break;
                default:
                    break;
            }
        }
    }
}