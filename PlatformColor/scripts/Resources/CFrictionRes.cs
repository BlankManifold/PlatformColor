using Godot;

namespace PlatFormColor.scripts.Resources
{
    [GlobalClass]
    public partial class CFrictionRes : CTwoNodeInteractionRes
    {
        [Export]
        public float FrictionFactor = 0f;
        protected override string _cScenePath { get; } = Globals.ScenePath.CFriction;
    }


}