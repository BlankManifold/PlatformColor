using Godot;
using SCs = System.Collections.Generic;

namespace PlatFormColor.scripts.Resources
{
    [GlobalClass]
    public partial class PlayerRes : EntityWithPropertiesRes
    {
        public Vector2 GlobalPosition;

        public PlayerRes(Vector2? globalPosition = null, SCs::Dictionary<Globals.Property, (Variant Value, bool Changeable)> dict = null) : base(dict)
        {
            GlobalPosition = (globalPosition == null) ? new Vector2(0, 0) : (Vector2)globalPosition;
        }
    }
}