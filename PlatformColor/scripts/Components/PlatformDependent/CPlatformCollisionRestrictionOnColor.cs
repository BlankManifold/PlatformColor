using System.Linq;
using Godot;

namespace PlatFormColor.scripts.Components.PlatformDependent
{
    [GlobalClass]
    public partial class CPlatformCollisionRestrictionOnColor : Generic.CCollisionRestriction<Platform.Platform>, Interfaces.ICanEmitReset
    {
        [Export]
        private Color _allowedColor;
        [Export]
        protected CReportPlatformCollision _CReportCollision = null;
        private Platform.Platform _lastCollidedPlatform = null;

        public event Interfaces.NotifyAction Reset;
        public override string[] _GetConfigurationWarnings()
        {
            string[] warnings = base._GetConfigurationWarnings();

            if (_CReportCollision == null)
                _ = warnings.Append<string>("Must assign a CReportPlatformCollision in order to listen to Collided signal.");

            return warnings;
        }
        public override void _Ready()
        {
            base._Ready();
            AddToGroup("NodeWithResetSignal");
        }


        protected override bool _IsCollisionAllowed(Platform.Platform platform)
        {
            if (platform == null)
                return true;
            if (platform == _lastCollidedPlatform)
            {
                if (_controlledNode is Interfaces.IPropAndResEntity controlledNodeWithProperties)
                    controlledNodeWithProperties.UpdateRes();
                return true;
            }

            _lastCollidedPlatform = platform;

            Variant? colliderColor = platform.GetProperty(Globals.Property.Color);
            if (colliderColor == null || _allowedColor == (Color)colliderColor)
            {
                if (_controlledNode is Interfaces.IPropAndResEntity controlledNodeWithProperties)
                    controlledNodeWithProperties.UpdateRes();
                return true;
            }

            return false;
        }
        protected override void _ConnectReportCollsion()
        {
            _CReportCollision.Collided += _OnCollided;
        }
        protected override void _OnCollided(Platform.Platform platform)
        {
            if (!_active)
                return;

            if (_IsCollisionAllowed(platform))
                return;

            Reset?.Invoke();
            platform.Reset();

            _controlledNode.Velocity = new(0, 0);
            if (_controlledNode is Interfaces.IPropAndResEntity controlledNodeWithProperties)
                controlledNodeWithProperties.Reset();
        }

    }
}