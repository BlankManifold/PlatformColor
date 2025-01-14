using Godot;
using SCs = System.Collections.Generic;


namespace PlatFormColor.scripts.Resources
{
    [GlobalClass]
    public partial class PlatformRes : EntityWithPropertiesRes
    {
        public Vector2 GlobalPosition;

        public PlatformRes(Vector2? globalPosition = null, SCs::Dictionary<Globals.Property, (Variant Value, bool Changeable)> dict = null) : base(dict)
        {
            GlobalPosition = (globalPosition == null) ? new Vector2(0, 0) : (Vector2)globalPosition;
        }
    }
}