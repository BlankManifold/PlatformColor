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
            rect.Color = _promotedRes.LandingColor;
            rect.Position -= rect.Size / 2.0f;

            RectangleShape2D shape = (RectangleShape2D)GetNode<CollisionShape2D>("CollisionShape2D").Shape;
            shape.Size = _promotedRes.Size;

        }
        public override void _PhysicsProcess(double delta)
        {
            base._PhysicsProcess(delta);
        }

        public override void ChangeColor(Color color)
        {
            GetNode<ColorRect>("ColorRect").Color = color;
        }
        public override Color GetColor()
        {
            return _promotedRes.DroppedColor;
        }
    }
}