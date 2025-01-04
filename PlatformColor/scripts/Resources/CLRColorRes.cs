using Godot;

namespace PlatFormColor.scripts.Resources
{
    [GlobalClass]
    public partial class CLRColorRes : CTwoNodeInteractionRes
    {
        [Export]
        public Color Color;
        protected override string _cScenePath { get; } = Globals.ScenePath.CLRColor;
    }


}