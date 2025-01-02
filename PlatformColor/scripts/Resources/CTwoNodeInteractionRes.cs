using Godot;

namespace PlatFormColor.scripts.Resources
{
    [GlobalClass]
    public partial class CTwoNodeInteractionRes : Resource
    {
        protected virtual string _cScenePath { get; } = null;
        public virtual Components.CTwoNodeInteraction CreateComponent()
        {
            PackedScene cScene = ResourceLoader.Load<PackedScene>(_cScenePath);
            Components.CTwoNodeInteraction cNode = cScene.Instantiate<Components.CTwoNodeInteraction>();
            cNode.Init(this);
            return cNode;
        }
    }
}