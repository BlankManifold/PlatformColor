using Godot;

namespace PlatFormColor.scripts.Platform
{
    [GlobalClass]
    public partial class PICFrictionRes : PlatformInteractionComponentRes
    {
        [Export]
        public float FrictionFactor = 0f;
        protected override string _componentScenePath { get; } = Globals.ScenePath.PICFriction;
    }


}