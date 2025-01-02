using Godot;

namespace PlatFormColor.scripts.Components
{
    public delegate void RequestActivation(bool Deactivate = false);

    [GlobalClass]
    public abstract partial class CTwoNodeInteraction : Node
    {
        protected bool _active = false;
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }
        protected Node _parentNode = null;

        [Signal]
        public delegate void RequestActivationEventHandler(CTwoNodeInteraction component, bool deactivate = false);
        public abstract void Init(Resources.CTwoNodeInteractionRes res);
        public abstract void Apply(PhysicsBody2D interactingBody);
        public abstract void React(PhysicsBody2D body1, PhysicsBody2D body2);
        public void AssignParent(Node parentNode)
        {
            _parentNode = parentNode;
        }
    }
}