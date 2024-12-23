using Godot;
using PlatFormColor.scripts.Resources;

namespace PlatFormColor.scripts.Player
{
    public partial class SquarePlayer : Player
    {
        // [Export(PropertyHint.ResourceType)]
        // protected Resources.SquarePlayerRes _res = null;
        public override void _Ready()
        {
            base._Ready();

            SquarePlayerRes res = (SquarePlayerRes)_res;

            ColorRect rect = GetNode<ColorRect>("ColorRect");
            rect.Size = res.Size;
            rect.Color = res.LandingColor;
            rect.Position -= rect.Size / 2.0f;

            RectangleShape2D shape = (RectangleShape2D)GetNode<CollisionShape2D>("CollisionShape2D").Shape;
            shape.Size = res.Size;

        }
        public override void _PhysicsProcess(double delta)
        {
            base._PhysicsProcess(delta);
        }
    }
}