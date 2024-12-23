using Godot;
using GCs = Godot.Collections;

namespace PlatFormColor.scripts.Resources
{
    [GlobalClass]
    public partial class PlayerRes : Resource
    {
        [Export]
        GCs::Dictionary<string, float> GlobalPhysicsProperties { get; set; } = new();

        public float GetGlobalPhysicsProperty(string propertyName) => GlobalPhysicsProperties[propertyName.ToLower()];
    }
}