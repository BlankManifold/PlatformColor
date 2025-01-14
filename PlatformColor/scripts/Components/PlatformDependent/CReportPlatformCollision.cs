using Godot;
using GCs = Godot.Collections;

namespace PlatFormColor.scripts.Components.PlatformDependent
{
    [GlobalClass]
    public partial class CReportPlatformCollision : Generic.CReportCollision<Platform.Platform>
    {
        [Export]
        public GCs::Array<Resources.PlatformCollisionRestrictionRes> Restrictions;
        public override void _Ready()
        {
            base._Ready();

            foreach (Resources.PlatformCollisionRestrictionRes restriction in Restrictions)
            {
                _genericRestrictions.Add(restriction);
            }

        }
    }
}