using PlatFormColor.scripts.Interfaces;
using PCI = PlatFormColor.scripts.Platform.PlatformInteractionComponent;
using PCIRes = PlatFormColor.scripts.Platform.PlatformInteractionComponentRes;

namespace PlatFormColor.scripts.Platform
{
    public partial class PICFriction : PCI, IReactiveComponent
    {
        public float FrictionFactor = 0f;
        public override void Init(PCIRes res)
        {
            PICFrictionRes promotedRes = res as PICFrictionRes;
            FrictionFactor = promotedRes.FrictionFactor;
        }
        public override void Apply(Player.Player player)
        {
            if (!player.IsOnFloor())
                return;
            if (_active)
                return;

            if (player is Interfaces.IFrictionChangeable playerFrictionChangable)
            {
                playerFrictionChangable.ChangeFriction(FrictionFactor);
                EmitSignal(SignalName.RequestActivation, this, false);
            }
        }
        public void React(Platform platform, Player.Player player)
        {
            if (!_active)
                return;
            if (platform == _parentPlatform)
                return;

            if (platform == null)
            {
                ((Interfaces.IFrictionChangeable)player).ChangeFriction(1.0f);
            }

            EmitSignal(SignalName.RequestActivation, this, true);
        }
    }
}