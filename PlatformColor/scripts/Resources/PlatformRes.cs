using Godot;
using GCs = Godot.Collections;

namespace PlatFormColor.scripts.Resources
{
    [GlobalClass]
    public partial class PlatformRes : EntityWithPropertiesRes
    {
        public Vector2 GlobalPosition;

        public PlatformRes(Vector2? globalPosition = null, GCs::Dictionary<Globals.Property, Variant> dict = null) : base(dict)
        {
            GlobalPosition = (globalPosition == null) ? new Vector2(0, 0) : (Vector2)globalPosition;
        }
    }
}