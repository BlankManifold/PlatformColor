using System.ComponentModel;
using Godot;
using GCs = Godot.Collections;
using PIC = PlatFormColor.scripts.Platform.PlatformInteractionComponent;

namespace PlatFormColor.scripts.Managers
{
    [GlobalClass]
    public partial class PlatformsManager : Node
    {
        private GCs::Array<PIC> _activeComponents = new();

        public override void _Ready()
        {
            base._Ready();
            _ConnectPlatforms();
        }
        public void OnHandlingRequest(Player.Player player, Platform.Platform platform)
        {
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

        }

        private void _OnActivationRequest(PIC component, bool deactivate)
        {
            if (!deactivate)
            {
                _AddToActive(component);
                return;
            }
            if (deactivate)
            {
                _RemoveFromActive(component);
            }
        }
        private void _AddToActive(PIC component)
        {

            if (_activeComponents.Contains(component))
                return;

            _activeComponents.Add(component);
        }
        private void _RemoveFromActive(PIC component)
        {
            if (!_activeComponents.Contains(component))
                return;

            _activeComponents.Remove(component);
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