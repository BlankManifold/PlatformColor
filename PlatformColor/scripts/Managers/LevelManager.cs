using Godot;

namespace PlatFormColor.scripts.Managers
{
    [GlobalClass]
    public partial class LevelManager : Node
    {
        [Export]
        public Player.Player _player = null;
        [Export]
        public PlatformsManager _platformsManager = null;

        public override void _Ready()
        {
            _player.RequestPlatformHandling += _platformsManager.OnHandlingRequest;
        }
    }

}