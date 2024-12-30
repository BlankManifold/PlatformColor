
namespace PlatFormColor.scripts.Player
{
    class PlatformColorModifier : Interfaces.IPlatformModifier
    {
        private Platform.Platform _previousPlatform = null;
        public void Apply(Player player, Platform.Platform platform, double delta)
        {
            if (platform == _previousPlatform)
                return;

            if (
                platform is Interfaces.IColorChangeable platformColorChangeable &&
                player is Interfaces.IHasColor playerWithColor
                )
            {
                platformColorChangeable.ChangeColor(playerWithColor.GetColor());
                _previousPlatform = platform;
            }

            return;
        }
    }
}