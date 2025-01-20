using System.Linq;
using Godot;

namespace PlatFormColor.scripts.Components.PlatformDependent
{
    [GlobalClass]
    public partial class CLeaveColorOnPlatform : CBase
    {

        [Export]
        protected bool _onlyOnFloor = true;
        [Export]
        protected Generic.CColor _CColor = null;
        [Export]
        protected int colorIndex = 0;
        [Export]
        protected CReportPlatformCollision _CReportCollision = null;
        private Platform.Platform _lastCollidedPlatform = null;
        private Platform.Platform _platformThatHasToChangeColor = null;
        private Color _color;

        public override string[] _GetConfigurationWarnings()
        {
            string[] warnings = null;
            warnings = base._GetConfigurationWarnings();

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
            _CColor.ChangedColor += _OnChangedColor;
        }
        public void _OnCollided(Platform.Platform platform)
        {
            if (!_active)
                return;
            if (_onlyOnFloor && !_CReportCollision.GetControlledNode().IsOnFloor())
                return;
            if (platform == null)
                return;
            if (_platformThatHasToChangeColor == platform)
                return;

            if (_lastCollidedPlatform != platform)
                _lastCollidedPlatform = platform;

            if (_lastCollidedPlatform == _platformThatHasToChangeColor)
                return;
            if (_platformThatHasToChangeColor != null)
            {
                _platformThatHasToChangeColor.UpdateRes();
                _platformThatHasToChangeColor.SetProperty(Globals.Property.Color, _CColor.GetColor(colorIndex));
                _platformThatHasToChangeColor = null;
            }

            if (platform.IsPropertyChangeable(Globals.Property.Color))
                _platformThatHasToChangeColor = platform;
        }
        public override void Reset()
        {
            _lastCollidedPlatform = null;
        }
        private void _OnChangedColor(Color color)
        {
            _color = color;
        }

    }
}