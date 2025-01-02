using Godot;

namespace PlatFormColor.scripts.Resources
{
    [GlobalClass]
    public partial class CLeaveColorRes : CTwoNodeInteractionRes
    {
        [Export]
        public Color Color;
        protected override string _cScenePath { get; } = Globals.ScenePath.CLeaveColor;
    }

}
