using Godot;
using CTNIRes = PlatFormColor.scripts.Resources.CTwoNodeInteractionRes;

namespace PlatFormColor.scripts.Components
{
    public partial class CLRColor : CLandingRestriction
    {
        private Color _allowedColor;
        public override void Init(CTNIRes res)
        {
            base.Init(res);
            Resources.CLRColorRes promotedRes = res as Resources.CLRColorRes;
            _allowedColor = promotedRes.Color;
        }
        protected override bool _IsLandingAllowed(PhysicsBody2D landingBody)
        {
            if (landingBody is Interfaces.IHasColor landingBodyWithColor)
            {
                return landingBodyWithColor.GetColor() == _allowedColor;
            }

            return true;
        }
    }

}
