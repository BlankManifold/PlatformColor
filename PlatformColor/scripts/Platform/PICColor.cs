using Godot;
using PCIRes = PlatFormColor.scripts.Platform.PlatformInteractionComponentRes;


namespace PlatFormColor.scripts.Platform
{
    public partial class PICColor : PlatformInteractionComponent, Interfaces.IReactiveComponent
    {
        private Color _color;
        private ColorRect _colorRect;
        private bool _done = false;

        public override void Init(PCIRes res)
        {
            PICColorRes promotedRes = res as PICColorRes;
            _color = promotedRes.Color;
        }
        public override void _Ready()
        {
            base._Ready();

            _colorRect = GetNode<ColorRect>("ColorRect");
            _colorRect.Color = _color;
            _colorRect.Size = _parentPlatform.Size;
            _colorRect.GlobalPosition = _parentPlatform.GlobalPosition;
            _colorRect.Position -= _parentPlatform.Size / 2.0f;
        }

        public override void Apply(Player.Player player)
        {
            if (_active)
                return;
            if (player is not Interfaces.IHasColor)
                return;

            EmitSignal(SignalName.RequestActivation, this, false);
        }

        public void React(Platform platform, Player.Player player)
        {
            if (!_active)
                return;
            if (platform == null)
                return;
            if (platform == _parentPlatform)
                return;

            ChangeColor(((Interfaces.IHasColor)player).GetColor());
            EmitSignal(SignalName.RequestActivation, this, true);
        }
        public void ChangeColor(Color color)
        {
            _color = color;
            _colorRect.Color = color;
        }



    }

}
