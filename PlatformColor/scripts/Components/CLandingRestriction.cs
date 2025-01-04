using Godot;
using CTNIRes = PlatFormColor.scripts.Resources.CTwoNodeInteractionRes;

namespace PlatFormColor.scripts.Components
{
    public partial class CLandingRestriction : CTwoNodeInteraction
    {
        private Vector2? _lastValidPosition = null;
        public override void Init(CTNIRes res)
        { }
        public override void _Ready()
        {
            base._Ready();
            CallDeferred(MethodName.EmitSignal, SignalName.RequestActivation, this, false);
        }

        public override void Apply(PhysicsBody2D interactingBody)
        {
            if (!_active)
                return;
            if (_lastValidPosition == null)
                return;
            if (_IsLandingAllowed(interactingBody))
                return;

            interactingBody.GlobalPosition = (Vector2)_lastValidPosition;
            if (interactingBody is CharacterBody2D interactingCharacterBody)
                interactingCharacterBody.Velocity = new(0, 0);
        }

        public override void React(PhysicsBody2D body1, PhysicsBody2D body2)
        {
            if (body1 == null)
                return;
            if (body1 == _parentNode)
                return;

            _lastValidPosition = body2.GlobalPosition;
        }
        protected virtual bool _IsLandingAllowed(PhysicsBody2D interactingBody)
        {
            return false;
        }
    }

}
