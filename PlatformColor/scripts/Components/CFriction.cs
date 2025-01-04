using Godot;
using CTNIRes = PlatFormColor.scripts.Resources.CTwoNodeInteractionRes;

namespace PlatFormColor.scripts.Components
{
    public partial class CFriction : CTwoNodeInteraction// Interfaces.IReactiveComponent
    {
        private float _frictionFactor = 0f;
        public override void Init(CTNIRes res)
        {
            Resources.CFrictionRes promotedRes = res as Resources.CFrictionRes;
            _frictionFactor = promotedRes.FrictionFactor;
        }
        public override void Apply(PhysicsBody2D interactinBody)
        {
            // if (!interactinBody.IsOnFloor())
            //     return;
            if (_active)
                return;

            if (interactinBody is Interfaces.IFrictionChangeable bodyFrictionChangable)
            {
                bodyFrictionChangable.ChangeFriction(_frictionFactor);
                EmitSignal(SignalName.RequestActivation, this, false);
            }
        }
        public override void React(PhysicsBody2D body1, PhysicsBody2D body2)
        {
            if (!_active)
                return;
            if (body1 == _parentNode)
                return;

            if (body1 == null)
            {
                ((Interfaces.IFrictionChangeable)body2).ChangeFriction(1.0f);
            }

            EmitSignal(SignalName.RequestActivation, this, true);
        }
    }
}