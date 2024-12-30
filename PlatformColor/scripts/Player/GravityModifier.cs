using Godot;

namespace PlatFormColor.scripts.Player
{
    public class GravityModifier : Interfaces.IPhysicsModifier
    {
        private CharacterBody2D _controlledNode = null;
        private float _weight = 0f;

        public GravityModifier(CharacterBody2D controlledNode)
        {
            _controlledNode = controlledNode;
            if (controlledNode is Interfaces.IHasWeight controlledNodeWithGravity)
            {
                _controlledNode = controlledNode;
                _weight = controlledNodeWithGravity.GetWeight();
            }
            else
            {
                throw new System.Exception("CharacterBody2D is not a IHasWeight");
            }
        }
        public void Apply(double delta)
        {
            if (_controlledNode.IsOnFloor())
                return;

            Vector2 velocity = _controlledNode.Velocity;
            velocity += _controlledNode.GetGravity() * _weight * (float)delta;

            _controlledNode.Velocity = velocity;
        }
    }

}