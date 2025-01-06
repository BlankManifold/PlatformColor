using System.Linq;
using Godot;

namespace PlatFormColor.scripts.Components.PlatformDependent
{
    [GlobalClass]
    public partial class CFrictionOnPlatform : Generic.CFriction
    {
        [Export]
        private CReportPlatformCollision _CReportCollision = null;
        private Platform.Platform _lastColliderBody = null;
        [Signal]
        public delegate void ChangeFrictionFactorEventHandler(float frictionFactor);

        public override string[] _GetConfigurationWarnings()
        {
            string[] warnings = base._GetConfigurationWarnings();

            if (_CReportCollision == null)
                _ = warnings.Append<string>("Must assign a CReportPlaformCollision in order to listen to Collided signal.");

            return warnings;
        }
        public override void _Ready()
        {
            base._Ready();
            _CReportCollision.Collided += _OnCollided;
        }

        private void _OnCollided(Platform.Platform platform)
        {
            if (!_active)
                return;

            if (_lastColliderBody == platform)
                return;
            if (platform == null)
            {
                _frictionFactor = 0.0f;
                return;
            }

            Variant? frictionFactor = platform.GetProperty(Globals.Property.FrictionFactor);
            _frictionFactor = (frictionFactor != null) ? (float)frictionFactor : 1.0f;
        }
        public override void Reset()
        {
            _lastColliderBody = null;
        }
    }
}