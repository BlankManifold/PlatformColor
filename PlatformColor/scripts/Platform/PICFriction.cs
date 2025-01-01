using Godot;
using PCI = PlatFormColor.scripts.Platform.PlatformInteractionComponent;
using PCIRes = PlatFormColor.scripts.Platform.PlatformInteractionComponentRes;

namespace PlatFormColor.scripts.Platform
{
    public partial class PICFriction : PCI
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

            if (player is Interfaces.IFrictionChangeable playerFrictionChangable)
            {
                playerFrictionChangable.ChangeFriction(FrictionFactor);
            }
        }

    }
}