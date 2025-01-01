using Godot;
using PCI = PlatFormColor.scripts.Platform.PlatformInteractionComponent;

namespace PlatFormColor.scripts.Platform
{
    [GlobalClass]
    public partial class PlatformInteractionComponentRes : Resource
    {
        protected virtual string _componentScenePath { get; } = null;
        public virtual PCI CreateComponent()
        {
            PackedScene componentScene = ResourceLoader.Load<PackedScene>(_componentScenePath);
            PCI componentNode = componentScene.Instantiate<PCI>();
            componentNode.Init(this);
            return componentNode;
        }
    }
}