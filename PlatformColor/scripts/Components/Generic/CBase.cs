using Godot;

namespace PlatFormColor.scripts.Components
{
    public abstract partial class CBase : Node
    {
        [Export]
        protected bool _active = true;
        public void Activate(bool active = true)
        {
            _active = active;
        }
        public virtual void Reset()
        {
            return;
        }
    }
}