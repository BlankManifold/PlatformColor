using Godot;

namespace PlatFormColor.scripts.Components.PlatformDependent
{
    [GlobalClass]
    public partial class CReportPlatformCollision : Generic.CReportCollision<Platform.Platform>
    {
        public override void _Ready()
        {
            base._Ready();

            foreach (var item in GetChildren())
            {
                if (item is CollisionRestrictions.CollisionRestriction<CharacterBody2D, Platform.Platform> restriction)
                    _genericRestrictions.Add(restriction);
            }

        }
    }
}