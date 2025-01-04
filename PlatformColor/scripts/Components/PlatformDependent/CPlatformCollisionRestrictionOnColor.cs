using System.Linq;
using Godot;

namespace PlatFormColor.scripts.Components.PlatformDependent
{
    [GlobalClass]
    public partial class CPlatformCollisionRestrictionOnColor : Generic.CCollisionRestriction<Platform.Platform>
    {
        [Export]
        private Color _allowedColor;
        [Export]
        protected CReportPlatformCollision _CReportCollision = null;
        public override string[] _GetConfigurationWarnings()
        {
            string[] warnings = base._GetConfigurationWarnings();

            if (_CReportCollision == null)
                _ = warnings.Append<string>("Must assign a CReportPlatformCollision in order to listen to Collided signal.");

            return warnings;
        }

        protected override bool _IsCollisionAllowed(Platform.Platform platform)
        {
            if (platform == null)
                return true;

            Variant? colliderColor = platform.GetProperty(Globals.Property.Color);
            if (colliderColor == null)
                return true;
            if (_allowedColor == (Color)colliderColor)
                return true;

            return false;
        }
        protected override void _ConnectReportCollsion()
        {
            _CReportCollision.Collided += _OnCollided;
        }

    }
}