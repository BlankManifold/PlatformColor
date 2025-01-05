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
        private Timer _resetTimer;

        public override void _Ready()
        {
            _player.RequestPlatformHandling += _platformsManager.OnHandlingRequest;
            foreach (Node node in GetTree().GetNodesInGroup("NodeWithResetSignal"))
            {
                Interfaces.ICanEmitReset nodeWithResetSignal = (Interfaces.ICanEmitReset)node;
                nodeWithResetSignal.Reset += _OnReset;
            }

            _resetTimer = GetNode<Timer>("ResetTimer");
            _resetTimer.Timeout += _OnResetTimerTimeout;

        }
        public override void _Process(double delta)
        {
            GetNode<Label>("Label").Text = ((int)_resetTimer.TimeLeft).ToString();
        }
        private void _OnReset()
        {
            _player.ActivateComponents(false);
            _resetTimer.Start();
        }
        private void _OnResetTimerTimeout()
        {
            _player.ActivateComponents(true);
        }
    }

}