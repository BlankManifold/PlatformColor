using Godot;

namespace PlatFormColor.scripts.Platform
{
    [GlobalClass]
    public partial class PICColorRes : PlatformInteractionComponentRes
    {
        [Export]
        public Color Color;
        protected override string _componentScenePath { get; } = Globals.ScenePath.PICColor;
    }

}
