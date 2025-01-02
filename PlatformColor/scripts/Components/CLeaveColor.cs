using Godot;
using CTNIRes = PlatFormColor.scripts.Resources.CTwoNodeInteractionRes;

namespace PlatFormColor.scripts.Components
{
    public partial class CLeaveColor : CTwoNodeInteraction
    {
        public override void Init(CTNIRes res)
        {
            // Resources.CLeaveColorRes promotedRes = res as Resources.CLeaveColorRes;
        }
        public override void _Ready()
        {
            base._Ready();
        }

        public override void Apply(PhysicsBody2D interactingBody)
        {
            if (_active)
                return;
            if (interactingBody is not Interfaces.IHasColor)
                return;

            EmitSignal(SignalName.RequestActivation, this, false);
        }

        public override void React(PhysicsBody2D body1, PhysicsBody2D body2)
        {
            if (!_active)
                return;
            if (body1 == null)
                return;
            if (body1 == _parentNode)
                return;

            if (
                _parentNode is Interfaces.IColorChangeable parentColorChangeable &&
                body2 is Interfaces.IHasColor bodyWithColor
                )
            {
                parentColorChangeable.ChangeColor(bodyWithColor.GetColor());
                EmitSignal(SignalName.RequestActivation, this, true);
            }
        }
    }

}
