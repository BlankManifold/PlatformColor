using Godot;
using PCIRes = PlatFormColor.scripts.Platform.PlatformInteractionComponentRes;
using PIC = PlatFormColor.scripts.Platform.PlatformInteractionComponent;


namespace PlatFormColor.scripts.Platform
{
    public delegate void RequestActivation(bool Deactivate = false);

    [GlobalClass]
    public abstract partial class PlatformInteractionComponent : Node
    {
        protected Platform _parentPlatform = null;
        protected bool _active = false;
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }
        [Signal]
        public delegate void RequestActivationEventHandler(PIC component, bool deactivate = false);
        public abstract void Init(PCIRes res);
        public abstract void Apply(Player.Player player);
        public void AssignParent(Platform platform)
        {
            _parentPlatform = platform;
        }
    }
}