using System.Linq;
using Godot;

namespace PlatFormColor.scripts.Components.PlatformDependent
{
    [GlobalClass]
    public partial class CLeaveColorOnPlatform : CBase
    {
        [Export]
        protected Node _controlledNode = null;
        [Export]
        protected Generic.CColor _CColor = null;
        [Export]
        protected CReportPlatformCollision _CReportCollision = null;
        private Platform.Platform _lastCollidedPlatform = null;
        private Color _color;

        public override string[] _GetConfigurationWarnings()
        {
            string[] warnings = null;
            warnings = base._GetConfigurationWarnings();

            if (_controlledNode == null)
                _ = warnings.Append<string>("Must assign a Node that implements IEntityWithProperties interface add weight to it.");
            if (_CColor == null)
                _ = warnings.Append<string>("Must assign a CColor component to get color from it.");
            if (_CReportCollision == null)
                _ = warnings.Append<string>("Must assign a CReportPlaformCollision in order to listen to Collided signal.");

            return warnings;
        }
        public override void _Ready()
        {
            base._Ready();
            _color = _CColor.GetColor();
            _CReportCollision.Collided += _OnCollided;
        }
        public void _OnCollided(Platform.Platform platform)
        {
            if (!_active)
                return;

            if (platform == null)
                return;
            if (platform == _lastCollidedPlatform)
                return;

            if (_lastCollidedPlatform == null)
            {
                _lastCollidedPlatform = platform;
                return;
            }

            _lastCollidedPlatform.UpdateRes();
            _lastCollidedPlatform.SetProperty(Globals.Property.Color, _color);
            _lastCollidedPlatform = platform;
        }
        public override void Reset()
        {
            _lastCollidedPlatform = null;
        }

    }
}