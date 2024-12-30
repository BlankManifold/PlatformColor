using Godot;
namespace PlatFormColor.scripts.Player
{
    class PlatformFrictionModifier : Interfaces.IPlatformModifier
    {
        private Platform.Platform _previousPlatform = null;
        public void Apply(Player player, Platform.Platform platform, double delta)
        {
            if (_previousPlatform == platform || !player.IsOnFloor())
                return;

            if
            (
                platform is Interfaces.IHasFriction platformWithFriction &&
                player is Interfaces.IFrictionChangeable playerFrictionChangable
            )
            {
                playerFrictionChangable.ChangeFriction(platformWithFriction.GetFriction());
            }

            _previousPlatform = platform;
        }
    }
}