using Godot;
using CTNIRes = PlatFormColor.scripts.Resources.CTwoNodeInteractionRes;

namespace PlatFormColor.scripts.Components
{
    public partial class CLeaveColor : CTwoNodeInteraction
    {
        private Color _color;
        public override void Init(CTNIRes res)
        {
            Resources.CLeaveColorRes promotedRes = res as Resources.CLeaveColorRes;
            _color = promotedRes.Color;
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

            ChangeColor(((Interfaces.IHasColor)body2).GetColor());
            EmitSignal(SignalName.RequestActivation, this, true);
        }
        public void ChangeColor(Color color)
        {
            _color = color;
        }



    }

}
