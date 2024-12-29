using Godot;

namespace PlatFormColor.scripts.Interfaces
{
    public interface IPlatformModifier
    {
        public void Apply(
            Player.Player player, Platform.Platform platform, double delta
            );
    }
}