using Godot;
using GCs = Godot.Collections;

namespace PlatFormColor.scripts.Managers
{
    [GlobalClass]
    public partial class PlatformsManager : Node
    {
        private Platform.Platform _lastHandledPlatform = null;

        public override void _Ready()
        {
            base._Ready();
        }
        public void OnHandlingRequest(Player.Player player, Platform.Platform platform)
        {
            if (_lastHandledPlatform == platform)
                return;


            // if (platform != null)
            // {
            //     foreach (CTNI component in platform.InteractionComponents)
            //         component.Apply(player);
            // }

            _lastHandledPlatform = platform;
        }

    }

}