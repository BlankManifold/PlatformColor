using Godot;
using GCs = Godot.Collections;
using PIC = PlatFormColor.scripts.Platform.PlatformInteractionComponent;

namespace PlatFormColor.scripts.Managers
{
    [GlobalClass]
    public partial class PlatformsManager : Node
    {
        private GCs::Array<PIC> _activeComponents = new();
        private Label _label;
        private Platform.Platform _lastHandledPlatform = null;

        public override void _Ready()
        {
            base._Ready();
            _ConnectPlatforms();
            _label = new Label();
            AddChild(_label);
        }
        public override void _Process(double delta)
        {
            _label.Text = "";
            foreach (PIC component in _activeComponents)
            {
                _label.Text += component.Name;
                _label.Text += "\n";
            }
        }
        public void OnHandlingRequest(Player.Player player, Platform.Platform platform)
        {
            if (_lastHandledPlatform == platform)
                return;

            foreach (PIC component in _activeComponents)
            {
                if (component is Interfaces.IReactiveComponent reactiveComponent)
                    reactiveComponent.React(platform, player);
            }

            if (platform != null)
            {
                foreach (PIC component in platform.InteractionComponents)
                    component.Apply(player);
            }

            _lastHandledPlatform = platform;
        }

        private void _OnActivationRequest(PIC component, bool deactivate)
        {
            if (!deactivate)
            {
                CallDeferred(MethodName._AddToActive, component);
                return;
            }
            if (deactivate)
            {
                CallDeferred(MethodName._RemoveFromActive, component);
                return;
            }
        }
        private void _AddToActive(PIC component)
        {
            if (_activeComponents.Contains(component))
                return;

            _activeComponents.Add(component);
            component.Active = true;
        }
        private void _RemoveFromActive(PIC component)
        {
            if (!_activeComponents.Contains(component))
                return;

            _activeComponents.Remove(component);
            component.Active = false;
        }
        private void _ConnectPlatforms()
        {
            foreach (Platform.Platform platform in GetTree().GetNodesInGroup("platform"))
            {
                foreach (PIC component in platform.InteractionComponents)
                {
                    component.RequestActivation += _OnActivationRequest;
                }
            }
        }
    }

}