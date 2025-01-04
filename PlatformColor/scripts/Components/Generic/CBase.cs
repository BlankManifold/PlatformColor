using Godot;

namespace PlatFormColor.scripts.Components
{
    public abstract partial class CBase : Node
    {
        public abstract void Apply(double delta);
    }
}