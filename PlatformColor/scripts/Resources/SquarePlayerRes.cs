using Godot;

namespace PlatFormColor.scripts.Resources
{
    [GlobalClass]
    public partial class SquarePlayerRes : PlayerRes
    {
        [Export]
        public Color LandingColor;
        [Export]
        public Color DroppedColor;
        [Export]
        public Vector2 Size;
    }
}