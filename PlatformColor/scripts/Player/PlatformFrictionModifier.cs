using Godot;
namespace PlatFormColor.scripts.Player
{
    class PlatformFrictionModifier : Interfaces.IPlatformModifier
    {
        public void Apply(Player player, Platform.Platform platform, double delta)
        {
            if (
                platform is not Interfaces.IHasFriction ||
                player is not Interfaces.IFrictionChangeable
                )
                return;

            if (player.IsOnFloor())
            {
                Vector2 velocity = player.Velocity;
                velocity.X /= platform.GetFriction();

                player.Velocity = velocity;
            }
        }
    }
}