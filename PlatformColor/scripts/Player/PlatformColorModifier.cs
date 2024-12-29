
namespace PlatFormColor.scripts.Player
{
    class PlatformColorModifier : Interfaces.IPlatformModifier
    {
        private Platform.Platform _previousPlatform = null;
        public void Apply(Player player, Platform.Platform platform, double delta)
        {
            if (
                platform is not Interfaces.IColorChangeable ||
                player is not Interfaces.IColorChangeable
                )
                return;

            if (platform == _previousPlatform)
                return;

            platform.ChangeColor(player.GetColor());
            _previousPlatform = platform;
        }
    }
}